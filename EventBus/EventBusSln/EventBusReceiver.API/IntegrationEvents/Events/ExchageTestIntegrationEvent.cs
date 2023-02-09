using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace EventBusReceiver.API.IntegrationEvents.Events
{
    public record ExchageTestIntegrationEvent : IntegrationEvent
    {
        public string Name { get; set; } = "ExchageTest";
    }
}
