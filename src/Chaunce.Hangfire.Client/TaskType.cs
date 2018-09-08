using System;
using System.Collections.Generic;
using System.Text;

namespace Chaunce.Hangfire.Client
{
    /// <summary>
    /// 任务类型
    /// </summary>
    public enum TaskType
    {
        /// <summary>
        /// 循环任务
        /// </summary>
        Recurringjob,

        /// <summary>
        /// 一次任务
        /// </summary>
        Backgroundjob
    }
}
