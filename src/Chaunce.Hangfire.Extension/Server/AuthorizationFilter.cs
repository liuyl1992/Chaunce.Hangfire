using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chaunce.Hangfire.Extension
{
    public class AuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            return true;

            //return httpContext.User.Identity.IsAuthenticated;
        }
    }
}
