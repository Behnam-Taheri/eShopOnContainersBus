using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace EventBusReceiver.API.IntegrationEvents.Events
{
    public record SampleIntegrationEvent : IntegrationEvent
    {
        public string Name { get; set; } = "Behnam";
    }
}
