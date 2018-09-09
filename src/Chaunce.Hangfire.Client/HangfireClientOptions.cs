using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Chaunce.Hangfire.Client
{
    public class HangfireClientOptions
    {
        /// <summary>
        /// Hangfire服务地址(循环)
        /// </summary>
        public string RecurringJobUrl { get; set; }

        /// <summary>
        /// Hangfire服务地址（单次）
        /// </summary>
        public string BackgroundJobUrl { get; set; }

        /// <summary>
        /// Hangfire主机地址
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Hangfire的访问账户
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Hnagfire的访问密码
        /// </summary>
        public string PassWord { get; set; }
    }
}
