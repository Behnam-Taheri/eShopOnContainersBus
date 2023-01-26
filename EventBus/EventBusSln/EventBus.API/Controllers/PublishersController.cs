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
        public void Publis()
        {
            _eventBus.Publish(new SampleIntegrationEvent());
        }
    }
}
