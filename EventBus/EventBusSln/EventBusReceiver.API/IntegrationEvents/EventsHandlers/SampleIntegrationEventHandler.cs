using EventBusReceiver.API.IntegrationEvents.Events;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;

namespace EventBusReceiver.API.IntegrationEvents.EventsHandlers
{
    public class SampleIntegrationEventHandler : IIntegrationEventHandler<SampleIntegrationEvent>
    {
        public async Task Handle(SampleIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
