using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

#pragma warning disable IDE0051
#pragma warning disable IDE0052

namespace mxProject.Tasks
{
    internal class TaskObserverSample
    {

        private readonly TaskObserver TaskObserver = new TaskObserver();
        private IDisposable? TaskListenerUnregister;

        /// <summary>
        /// Performs initial processing related to TaskObserver.
        /// </summary>
        private void InitializeTaskObserver()
        {
            TaskListenerUnregister = TaskObserver.RegisterListener(OnStart, OnCompleted, OnFailed);
        }

        /// <summary>
        /// Executes the process at the start of the task.
        /// </summary>
        /// <param name="taskId"></param>
        private void OnStart(TaskID taskId)
        {
            Debug.WriteLine($"Start the task. ID:{taskId}");
        }

        /// <summary>
        /// Executes the process when the task is completed.
        /// </summary>
        /// <param name="taskId"></param>
        private void OnCompleted(TaskID taskId)
        {
            Debug.WriteLine($"The task is complete. ID:{taskId}");
        }

        /// <summary>
        /// Executes the process when the task is failed.
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="exception"></param>
        private void OnFailed(TaskID taskId, Exception? exception)
        {
            Debug.WriteLine($"The task is failed. {exception?.Message} ID:{taskId}");
        }


        /// <summary>
        /// Perform the task using the explicitly generated cancel token.
        /// </summary>
        /// <returns></returns>
        private Task RunAsyncWithCancellationToken(Guid taskId, CancellationTokenSource cancellation)
        {
            return TaskObserver.Observe(
                taskId
                , ExecuteAsync(cancellation.Token)
                , cancellation
                , disposableCancellation: false
                );
        }

        /// <summary>
        /// Perform the task using the implicitly generated cancel token.
        /// </summary>
        /// <returns></returns>
        private Task RunAsyncImplicitCancellationToken(Guid taskId)
        {
            return TaskObserver.RunWithCancellation(
                taskId
                , ExecuteAsync
                );
        }

        /// <summary>
        /// Requests cancellation for the specified task.
        /// </summary>
        /// <param name="taskId"></param>
        private void Cancel(Guid taskId)
        {
            TaskObserver.RequestCancel(taskId);
        }

        /// <summary>
        /// Requests cancellation for all tasks.
        /// </summary>
        private void CancelAll()
        {
            TaskObserver.RequestCancelAll();
        }

        /// <summary>
        /// Gets whether the specified task is running.
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        private bool IsRunning(Guid taskId)
        {
            return TaskObserver.IsRunning(taskId);
        }

        /// <summary>
        /// async method sample.
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        private Task ExecuteAsync(CancellationToken cancellation = default)
        {
            return Task.Delay(1000);
        }

    }

}

#pragma warning restore IDE0051
#pragma warning restore IDE0052