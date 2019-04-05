using Microsoft.AspNetCore.Mvc;

namespace Token.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new Domain.Entities.User
            {
                Name = HttpContext.User.FindFirst(nameof(Domain.Entities.User.Name)).Value,
                Token = HttpContext.User.FindFirst(nameof(Domain.Entities.User.Token)).Value
            });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
