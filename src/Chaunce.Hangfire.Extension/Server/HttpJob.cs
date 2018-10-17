using Hangfire;
using Hangfire.Console;
using Hangfire.Logging;
using Hangfire.Server;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Chaunce.Hangfire.Extension
{
    internal class HttpJob
    {
        private static readonly ILog Logger = LogProvider.For<HttpJob>();
        public static HangfireHttpJobOptions HangfireHttpJobOptions;


        public static HttpClient GetHttpClient(HttpJobItem item)
        {
            var handler = new HttpClientHandler();
            if (HangfireHttpJobOptions.Proxy == null)
            {
                handler.UseProxy = false;
            }
            else
            {
                handler.Proxy = HangfireHttpJobOptions.Proxy;
            }
            var HttpClient = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromMilliseconds(HangfireHttpJobOptions.GlobalHttpTimeOut),
            };

            if (!string.IsNullOrEmpty(item.BasicUserName) && !string.IsNullOrEmpty(item.BasicPassword))
            {
                var byteArray = Encoding.ASCII.GetBytes(item.BasicUserName + ":" + item.BasicPassword);
                HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            }
            return HttpClient;
        }

        public static HttpRequestMessage PrepareHttpRequestMessage(HttpJobItem item)
        {
            var request = new HttpRequestMessage(new HttpMethod(item.Method), item.Url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(item.ContentType));
            if (!item.Method.ToLower().Equals("get"))
            {
                if (!string.IsNullOrEmpty(item.Data))
                {
                    var bytes = Encoding.UTF8.GetBytes(item.Data);
                    request.Content = new ByteArrayContent(bytes, 0, bytes.Length);
                }
            }
            return request;
        }

        /// <summary>
        /// 执行
        /// 每个JobTask只能执行一个
        /// </summary>
        /// <param name="item"></param>
        /// <param name="jobName"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("HttpJob:{1}")]
        public static async Task Excute(HttpJobItem item, string jobName = null, PerformContext context = null)
        {
            
            var mon = JobStorage.Current.GetMonitoringApi();
            var processingJobs = mon.ProcessingJobs(0, int.MaxValue)
                .Where(o => o.Value.Job.Args[1].ToString() == jobName).ToList();

            //如果任务没执行完，删除新的任务
            if (processingJobs.Count() >= 2)
            {
                var removeId = processingJobs.Max(s => s.Key);
                RecurringJob.RemoveIfExists(removeId);
                BackgroundJob.Delete(removeId);
                return;
            }
            
            try
            {
                context.WriteLine(jobName);
                context.WriteLine(JsonConvert.SerializeObject(item));
                var client = GetHttpClient(item);
                var httpMesage = PrepareHttpRequestMessage(item);
                var httpResponse =await client.SendAsync(httpMesage);
                HttpContent content = httpResponse.Content;
                string result =await content.ReadAsStringAsync();
                context.WriteLine(result);
            }
            catch (Exception ex)
            {
                Logger.ErrorException("HttpJob.Excute", ex);
                context.WriteLine(ex.Message);
            }
        }

    }



}
