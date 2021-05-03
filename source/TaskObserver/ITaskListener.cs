using System;
using System.Collections.Generic;
using System.Text;

namespace mxProject.Tasks
{

    /// <summary>
    /// Provides the functionality needed to listern task completion notifications.
    /// </summary>
    /// <typeparam name="TTaskInfo">The type of task information that including an ID that uniquely identifies the task.</typeparam>
    public interface ITaskListener<TTaskInfo> where TTaskInfo : ITaskInfo
    {
        /// <summary>
        /// Executes the process at the start of the task.
        /// </summary>
        /// <param name="taskInfo">Task information including an ID that uniquely identifies the task.</param>
        void OnStart(TTaskInfo taskInfo);

        /// <summary>
        /// Executes the process when the task is completed.
        /// </summary>
        /// <param name="taskInfo">Task information including an ID that uniquely identifies the task.</param>
        void OnCompleted(TTaskInfo taskInfo);

        /// <summary>
        /// Executes the process when the task is failed.
        /// </summary>
        /// <param name="taskInfo">Task information including an ID that uniquely identifies the task.</param>
        /// <param name="exception">Exception that occurred.</param>
        void OnFailed(TTaskInfo taskInfo, Exception? exception);
    }

    /// <summary>
    /// Provides the functionality needed to listern task completion notifications.
    /// </summary>
    public interface ITaskListener : ITaskListener<TaskID>
    {
    }

}
