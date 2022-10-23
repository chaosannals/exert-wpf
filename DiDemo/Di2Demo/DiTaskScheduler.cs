using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Di2Demo;

// 没有使用
public class DiTaskScheduler : TaskScheduler
{
    protected override IEnumerable<Task>? GetScheduledTasks()
    {
        return null;
    }

    protected override void QueueTask(Task task)
    {
        
    }

    protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
    {
        return false;
    }
}
