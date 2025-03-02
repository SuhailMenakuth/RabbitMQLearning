using EventBus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServiceA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumberController : ControllerBase
    {
        private readonly IRabbitMQEventBus _eventBus;
        public NumberController(IRabbitMQEventBus eventBus) => _eventBus = eventBus;

        [HttpPost]
        public IActionResult Post([FromBody] NumberRequest request)
        {
            int sum = request.Num1 + request.Num2;
            if (sum > 5)
            {
                _eventBus.Publish("helloQueue", "Hello World");
            }
            return Ok("Processed");
        }
    }

    public class NumberRequest
    {
        public int Num1 { get; set; }
        public int Num2 { get; set; }
    }

}
