using EventBus.API.IntegrationEvents.Events;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;

namespace EventBus.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IEventBus _eventBus;

        public PublishersController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [HttpPost]
        public void Publish()
        {
            _eventBus.Publish(new SampleIntegrationEvent());
        }

        [HttpPost("PublishExchageTest")]
        public void PublishExchageTest()
        {
            _eventBus.Publish(new ExchageTestIntegrationEvent());
        }
    }
}
