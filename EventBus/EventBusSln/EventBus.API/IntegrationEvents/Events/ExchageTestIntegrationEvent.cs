using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace EventBus.API.IntegrationEvents.Events
{
    public record ExchageTestIntegrationEvent : IntegrationEvent
    {
        public string Name { get; set; } = "ExchageTest";
    }
}
