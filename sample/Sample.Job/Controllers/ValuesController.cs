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
                Url = "https://www.cnblogs.com/xiaoliangge/",
                JobName = "I'm external Job by restful Api",
            }, TaskType.Recurringjob);
            return new string[] { "Do i succeeded?", $"{result}" };
        }
    }
}
