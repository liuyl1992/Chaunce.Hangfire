using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chaunce.Hangfire.Client
{
    public interface IHangfireClient
    {
        Task<bool> SendTimerJobAsync(HttpJobItem model, TaskType taskType);
    }
}
