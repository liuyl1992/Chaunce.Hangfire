using Hangfire.Dashboard;
using System;
using System.Reflection;
using Hangfire;

namespace Chaunce.Hangfire.Extension
{
    public static class GlobalConfigurationExtension
    {
        public static IGlobalConfiguration UseHangfireHttpJob(this IGlobalConfiguration config, HangfireHttpJobOptions options = null)
        {
            if (options == null) options = new HangfireHttpJobOptions();
            var assembly = typeof(HangfireHttpJobOptions).GetTypeInfo().Assembly;


            //处理http请求
            DashboardRoutes.Routes.Add("/httpjob", new HttpJobDispatcher(options));


            var jsPath = DashboardRoutes.Routes.Contains("/js[0-9]+") ? "/js[0-9]+" : "/js[0-9]{3}";
            DashboardRoutes.Routes.Append(jsPath, new EmbeddedResourceDispatcher(assembly, "Chaunce.Hangfire.Extension.Content.jsoneditor.js"));
            DashboardRoutes.Routes.Append(jsPath, new DynamicJsDispatcher(options));
            DashboardRoutes.Routes.Append(jsPath, new EmbeddedResourceDispatcher(assembly, "Chaunce.Hangfire.Extension.Content.httpjob.js"));


            var cssPath = DashboardRoutes.Routes.Contains("/css[0-9]+") ? "/css[0-9]+" : "/css[0-9]{3}";
            DashboardRoutes.Routes.Append(cssPath, new EmbeddedResourceDispatcher(assembly, "Chaunce.Hangfire.Extension.Content.jsoneditor.css"));
            DashboardRoutes.Routes.Append(cssPath, new DynamicCssDispatcher(options));


            if (options.GlobalHttpTimeOut < 2000) options.GlobalHttpTimeOut = 2000;
            HttpJob.HangfireHttpJobOptions = options;

            return config;
        }
    }
}
