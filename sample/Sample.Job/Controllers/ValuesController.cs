using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chaunce.Hangfire.Client;
using Microsoft.AspNetCore.Mvc;

namespace Sample.Job.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IHangfireClient _hangfireClient;
        public ValuesController(IHangfireClient hangfireClient)
        {
            _hangfireClient = hangfireClient;
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            var result = await _hangfireClient.SendTimerJobAsync(new HttpJobItem
            {
                Corn = Cron.MinuteInterval(10),
                //Corn = "*/1 * * * *",
                Url = "https://blog.csdn.net/u014401141/article/details/71086757",
                JobName = "我是外部任务",
            }, TaskType.Recurringjob);
            return new string[] { "我成功了吗", $"{result}" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
