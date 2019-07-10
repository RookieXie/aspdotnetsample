using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBlog.DBContext;
using MyBlog.Services;

namespace MyBlogWeb.Controllers
{
    [Route("api")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> logger;
        private readonly ITestApiService testApiService;
        public ValuesController(ILogger<ValuesController> logger, ITestApiService testApiService)
        {
            this.logger = logger;
            this.testApiService = testApiService;
        }
        [HttpGet]
        [Route("values/test")]
        public string Test()
        {
            //var api = testApiService.Gets();
            //logger.LogDebug("哈哈哈哈哈");
            return "test";
        }
        /// <summary>
        ///  GET api/values
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("values/get2")]
        public ActionResult<IEnumerable<string>> Get()
        {
           var api= testApiService.Gets();
            //logger.LogDebug("哈哈哈哈哈");
            return new string[] {"", "" };
        }

        // GET api/values/5

        //[HttpGet("{id}")]
        [Route("values/get/{id:int:min(1)}")]
        public int Get(int id)
        {
            var api=testApiService.Get(id);
            return api.Id;
        }

        // POST api/values
        [HttpPost]
        [Route("values/get3")]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [Route("values/ge4t")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Route("values/get5")]
        public void Delete(int id)
        {
        }
    }
}
