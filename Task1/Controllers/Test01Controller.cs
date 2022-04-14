using Microsoft.AspNetCore.Mvc;
using Task1.Models;
using Task1.Services;

namespace Task1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Test01Controller : ControllerBase
    {
        private readonly ITest01Service _service;
        public Test01Controller(ITest01Service service)
        {
            _service = service;
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult Get(int currentPage = 1, int maxPerPage = 5)
        {
            var result = _service.Get(currentPage, maxPerPage);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add(Test01 model)
        {
            _service.Add(model);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(Test01 model, int id)
        {
            _service.Update(model,id);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Remove(Test01 model)
        {
            _service.Remove(model);
            return Ok();
        }

    }
}
