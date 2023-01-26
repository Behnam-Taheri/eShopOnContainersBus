using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace EventBus.API.IntegrationEvents.Events
{
    public record SampleIntegrationEvent : IntegrationEvent
    {
        public string Name { get; set; } = "Behnam";
    }
}
