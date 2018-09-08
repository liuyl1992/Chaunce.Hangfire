using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Chaunce.Hangfire.Client
{
    /// <summary>
    /// 发送任务类
    /// </summary>
    public class HangfireClient : IHangfireClient
    {
        private readonly IHttpClientFactory _clientFactory;
        public HangfireClient(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        /// <summary>
        /// 发送定时任务
        /// </summary>
        /// <param name="model"></param>
        /// <param name="taskType"></param>
        /// <returns></returns>
        public async Task<bool> SendTimerJobAsync(HttpJobItem model, TaskType taskType)
        {
            var client = _clientFactory.CreateClient("hangfire");
            string requestUrl = string.Empty;
            switch (taskType)
            {
                case TaskType.Recurringjob:
                    requestUrl = "hangfire/httpjob?op=recurringjob";
                    break;
                case TaskType.Backgroundjob:
                    requestUrl = "hangfire/httpjob?op=backgroundjob";
                    break;
                default:
                    break;
            };
            var content = new StringContent(JsonConvert.SerializeObject(new
            {
                model.Url,
                model.Data,
                model.Corn,
                model.JobName,
                model.BasicUserName,
                model.BasicPassword,
                Method = "POST",
                ContentType = "application/json",
            }));

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Add("Cookie", ".AspNetCore.Antiforgery.sTiOh67Tj1Y=CfDJ8PAS56rDRhRCqkF3gbzNyisHFL1Ycxw02RZADTY4pRk7H4vimKH7LK5KRnqeXhpdnv8aiuO0mpV-HSu2572KyPrltqjeRa77GojWa8eaS2oaiCcqlGUYhalADkc9lUpLBGachigzzl-sfhTXuWYURbg;");
            var response = await client.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(client.BaseAddress, requestUrl),
                Content = content,
                Version = HttpVersion.Version11,
            });

            if (response.IsSuccessStatusCode)
                return true;
            else return false;
        }
    }
}
