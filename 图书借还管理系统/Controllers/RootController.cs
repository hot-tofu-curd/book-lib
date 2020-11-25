using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

//这个控制器用来管理用户
namespace 图书借还管理系统.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RootController : ControllerBase
    {
        // GET: api/<RootController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RootController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RootController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RootController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RootController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
