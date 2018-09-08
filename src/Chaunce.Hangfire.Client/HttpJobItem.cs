using Newtonsoft.Json;

namespace Chaunce.Hangfire.Client
{
    public class HttpJobItem
    {
        /// <summary>
        /// 请求Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public object Data { get; set; }

        public string Corn { get; set; }
        public string JobName { get; set; }

        public string BasicUserName { get; set; }
        public string BasicPassword { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
