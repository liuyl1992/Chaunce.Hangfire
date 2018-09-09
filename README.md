
# :boom: Chaunce.Hangfire

Chaunce.Hangfire is a free, open source,Chaunce.Hangfire.Extension and Chaunce.Hangfire.Client built on the .NETStandard platform.
Chaunce.Hangfire.Extension fork from [Hangfire.HttpJob](https://github.com/yuzd/Hangfire.HttpJob)!
[![ http://www.cnblogs.com/xiaoliangge/](https://badges.gitter.im/Join%20Chat.svg)](http://www.cnblogs.com/xiaoliangge/)

## About Chauce.Hangfire

#### Please visit our website at http://www.cnblogs.com/xiaoliangge/ for the most current information about this project.

Chaunce.Hangfire is free,open source.
Chaunce.Hangfire can be called by Java and go.
Now only provide C# sdk.

## :boom:  How to use
### Step 1:appsettings.json
```xml
 "HangfireClientOptions": {
    "RecurringJobUrl": "hangfire/httpjob?op=recurringjob",
    "BackgroundJobUrl": "",
    "BaseUrl": "http://localhost:5000"
  }
```
### Step 2 : ConfigureServices

```csharp
public void ConfigureServices(IServiceCollection services)
        {
            var option = Configuration.GetSection(nameof(HangfireClientOptions)).Get<HangfireClientOptions>();

            services.AddHangfireClient(option);
        }
```
###  Step 3 : Write code in you controller 
```csharp

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
                Url = "https://blog.csdn.net/u014401141/article/details/71086757",
                JobName = "我是外部任务",
            }, TaskType.Recurringjob);
            return new string[] { "我成功了吗", $"{result}" };
        }
}

```
### Step 4 :Get ready Chaunce.Hangfire.Server 
First create database,databse name according to the appsettings.json's ConnectionStrings string
there is chauncehangfire

```json
 "ConnectionStrings": {
    "HangfireConnection": "server=.;database=chauncehangfire;uid=sa;pwd=111111"
  },
```
### Test Picture
![](https://github.com/liuyl1992/Chaunce.Hangfire/blob/master/123512.gif)

