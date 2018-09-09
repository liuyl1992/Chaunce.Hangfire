using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Http;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;

namespace Chaunce.Hangfire.Client
{
    public static class HangfireBuilderExtensions
    {
        public static IServiceCollection AddHangfireClient(this IServiceCollection services, HangfireClientOptions options)
        {
            services.TryAddSingleton<IHangfireClient, HangfireClient>();
            services.AddHttpClient("hangfire", x =>
            {
                if (!(string.IsNullOrWhiteSpace(options.UserName)||string .IsNullOrWhiteSpace(options.PassWord)))
                {
                    var byteArray = Encoding.ASCII.GetBytes($"{options.UserName}:{options.PassWord}");
                    x.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                }
               
                x.BaseAddress = new Uri($"{options.BaseUrl}");
                x.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                //x.DefaultRequestHeaders.Add("RequestVerificationToken", "CfDJ8PAS56rDRhRCqkF3gbzNyiuw5W20WcHEdRNIoiDH48mSyiMvwZgO6oVydcWc3giBNn5Ho7nfQ4Akvw2JYUJ5Qdikt_CenYV8WdTljuyqnlBx9OIi_E4Q17WXnaRoelJ8SiDCF4NlAnnRK44mDk-GNCU");
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                UseCookies = true,
            });
            return services;
        }
    }
}
