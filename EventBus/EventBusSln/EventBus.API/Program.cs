using Microsoft.AspNetCore.Connections;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBusRabbitMQ;
using Autofac;
using RabbitMQ.Client;
using Autofac.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(o => new AutofacServiceProviderFactory());
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

var container = new ContainerBuilder();
container.Populate(builder.Services);

var autofacProvider = new AutofacServiceProvider(container.Build());

RegisterRabbitConfiguration(builder);
RegisterEventBus(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
static void RegisterRabbitConfiguration(WebApplicationBuilder builder)
{
    builder.Services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
    {
        var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

        var factory = new ConnectionFactory()
        {
            HostName = builder.Configuration["EventBusConnection"],
            UserName = builder.Configuration["EventBusUserName"],
            Password = builder.Configuration["EventBusPassword"],

            DispatchConsumersAsync = true
        };

        int retryCount = int.Parse(builder.Configuration["EventBusRetryCount"]);

        return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
    });
}

static void RegisterEventBus(WebApplicationBuilder builder)
{
    builder.Services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
    {
        var subscriptionClientName = builder.Configuration["QueueName"];
        var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
        var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
        var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
        var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

        var retryCount = 0;
        if (!string.IsNullOrEmpty(builder.Configuration["EventBusRetryCount"]))
            retryCount = int.Parse(builder.Configuration["EventBusRetryCount"]);

        return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
    });
}