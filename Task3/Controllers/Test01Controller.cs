using Microsoft.AspNetCore.Mvc;
using Task3.Models;
using Task3.RabbitMQ;

namespace Task1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Test01Controller : ControllerBase
    {

        private readonly IBus _bus;
        private  const string QUEUE = "qtest1";
        public Test01Controller(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public IActionResult Add(Test01 model)
        {
            var data = new MqModel { Command = "create", Data = model };
            _bus.SendAsync(QUEUE,data);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(Test01 model)
        {
            var data = new MqModel { Command = "update", Data = model };
            _bus.SendAsync(QUEUE, data);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Remove(Test01 model)
        {
            var data = new MqModel { Command = "delete", Data = model };
            _bus.SendAsync(QUEUE, data);
            return Ok();
        }

    }
}
