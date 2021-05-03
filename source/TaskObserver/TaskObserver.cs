using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace mxProject.Tasks
{


    /// <summary>
    /// Task Observer
    /// </summary>
    public class TaskObserver : TaskObserver<TaskID>
    {

        #region RunWithCancellation

        /// <summary>
        /// Runs the specified work. Use the internally generated cancel token. 
        /// </summary>
        /// <param name="taskId">An ID that uniquely identifies the task.</param>
        /// <param name="function">The asynchronous work.</param>
        /// <returns>A task.</returns>
        /// <exception cref="ArgumentException">
        /// The specified task ID has already been registered.
        /// </exception>
        public Task RunWithCancellation(Guid taskId, Func<CancellationToken, Task> function)
        {
            return RunWithCancellation(new TaskID(taskId), function);
        }

        /// <summary>
        /// Runs the specified work. Use the internally generated cancel token. 
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
        /// <param name="taskId">An ID that uniquely identifies the task.</param>
        /// <param name="function">The asynchronous work.</param>
        /// <returns>A task.</returns>
        /// <exception cref="ArgumentException">
        /// The specified task ID has already been registered.
        /// </exception>
        public Task<TResult> RunWithCancellation<TResult>(Guid taskId, Func<CancellationToken, Task<TResult>> function)
        {
            return RunWithCancellation(new TaskID(taskId), function);
        }

        /// <summary>
        /// Runs the specified work. Use the internally generated cancel token. 
        /// </summary>
        /// <param name="taskId">An ID that uniquely identifies the task.</param>
        /// <param name="function">The asynchronous work.</param>
        /// <returns>A task.</returns>
        /// <exception cref="ArgumentException">
        /// The specified task ID has already been registered.
        /// </exception>
        public ValueTask RunWithCancellation(Guid taskId, Func<CancellationToken, ValueTask> function)
        {
            return RunWithCancellation(new TaskID(taskId), function);
        }

        /// <summary>
        /// Runs the specified work. Use the internally generated cancel token. 
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
        /// <param name="taskId">An ID that uniquely identifies the task.</param>
        /// <param name="function">The asynchronous work.</param>
        /// <returns>A task.</returns>
        /// <exception cref="ArgumentException">
        /// The specified task ID has already been registered.
        /// </exception>
        public ValueTask<TResult> RunWithCancellation<TResult>(Guid taskId, Func<CancellationToken, ValueTask<TResult>> function)
        {
            return RunWithCancellation(new TaskID(taskId), function);
        }

        #endregion

        #region Run

        /// <summary>
        /// Runs the specified work.
        /// </summary>
        /// <param name="taskId">An ID that uniquely identifies the task.</param>
        /// <param name="function">The asynchronous work.</param>
        /// <param name="cancellation">The Cancellation token that can be used to cancel the work.</param>
        /// <param name="disposableCancellation">A value that indicates whether the cancel token can be disposed after the task is completed.</param>
        /// <returns>A task.</returns>
        /// <exception cref="ArgumentException">
        /// The specified task ID has already been registered.
        /// </exception>
        public Task Run(Guid taskId, Func<CancellationToken, Task> function, CancellationTokenSource? cancellation = null, bool disposableCancellation = false)
        {
            return Observe(new TaskID(taskId), function(cancellation?.Token ?? default), cancellation, disposableCancellation);
        }

        /// <summary>
        /// Runs the specified work.
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
        /// <param name="taskId">An ID that uniquely identifies the task.</param>
        /// <param name="function">The asynchronous work.</param>
        /// <param name="cancellation">The Cancellation token that can be used to cancel the work.</param>
        /// <param name="disposableCancellation">A value that indicates whether the cancel token can be disposed after the task is completed.</param>
        /// <returns>A task.</returns>
        /// <exception cref="ArgumentException">
        /// The specified task ID has already been registered.
        /// </exception>
        public Task<TResult> Run<TResult>(Guid taskId, Func<CancellationToken, Task<TResult>> function, CancellationTokenSource? cancellation = null, bool disposableCancellation = false)
        {
            return Observe(new TaskID(taskId), function(cancellation?.Token ?? default), cancellation, disposableCancellation);
        }

        /// <summary>
        /// Runs the specified work.
        /// </summary>
        /// <param name="taskId">An ID that uniquely identifies the task.</param>
        /// <param name="function">The asynchronous work.</param>
        /// <param name="cancellation">The Cancellation token that can be used to cancel the work.</param>
        /// <param name="disposableCancellation">A value that indicates whether the cancel token can be disposed after the task is completed.</param>
        /// <returns>A task.</returns>
        /// <exception cref="ArgumentException">
        /// The specified task ID has already been registered.
        /// </exception>
        public ValueTask Run(Guid taskId, Func<CancellationToken, ValueTask> function, CancellationTokenSource? cancellation = null, bool disposableCancellation = false)
        {
            return Observe(new TaskID(taskId), function(cancellation?.Token ?? default), cancellation, disposableCancellation);
        }

        /// <summary>
        /// Runs the specified work.
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
        /// <param name="taskId">An ID that uniquely identifies the task.</param>
        /// <param name="function">The asynchronous work.</param>
        /// <param name="cancellation">The Cancellation token that can be used to cancel the work.</param>
        /// <param name="disposableCancellation">A value that indicates whether the cancel token can be disposed after the task is completed.</param>
        /// <returns>A task.</returns>
        /// <exception cref="ArgumentException">
        /// The specified task ID has already been registered.
        /// </exception>
        public ValueTask<TResult> Run<TResult>(Guid taskId, Func<CancellationToken, ValueTask<TResult>> function, CancellationTokenSource? cancellation = null, bool disposableCancellation = false)
        {
            return Observe(new TaskID(taskId), function(cancellation?.Token ?? default), cancellation, disposableCancellation);
        }

        #endregion

        #region Observe

        /// <summary>
        /// Observes the specified task.
        /// </summary>
        /// <param name="taskId">An ID that uniquely identifies the task.</param>
        /// <param name="task">The task.</param>
        /// <param name="cancellation">The Cancellation token that can be used to cancel the work.</param>
        /// <param name="disposableCancellation">A value that indicates whether the cancel token can be disposed after the task is completed.</param>
        /// <returns>A task.</returns>
        /// <exception cref="ArgumentException">
        /// The specified task ID has already been registered.
        /// </exception>
        public Task Observe(Guid taskId, Task task, CancellationTokenSource cancellation, bool disposableCancellation = false)
        {
            return Observe(new TaskID(taskId), task, cancellation, disposableCancellation);
        }

        /// <summary>
        /// Observes the specified task.
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
        /// <param name="taskId">An ID that uniquely identifies the task.</param>
        /// <param name="task">The task.</param>
        /// <param name="cancellation">The Cancellation token that can be used to cancel the work.</param>
        /// <param name="disposableCancellation">A value that indicates whether the cancel token can be disposed after the task is completed.</param>
        /// <returns>A task.</returns>
        /// <exception cref="ArgumentException">
        /// The specified task ID has already been registered.
        /// </exception>
        public Task<TResult> Observe<TResult>(Guid taskId, Task<TResult> task, CancellationTokenSource cancellation, bool disposableCancellation = false)
        {
            return Observe(new TaskID(taskId), task, cancellation, disposableCancellation);
        }

        /// <summary>
        /// Observes the specified task.
        /// </summary>
        /// <param name="taskId">An ID that uniquely identifies the task.</param>
        /// <param name="task">The task.</param>
        /// <param name="cancellation">The Cancellation token that can be used to cancel the work.</param>
        /// <param name="disposableCancellation">A value that indicates whether the cancel token can be disposed after the task is completed.</param>
        /// <returns>A task.</returns>
        /// <exception cref="ArgumentException">
        /// The specified task ID has already been registered.
        /// </exception>
        public ValueTask Observe(Guid taskId, ValueTask task, CancellationTokenSource cancellation, bool disposableCancellation = false)
        {
            return Observe(new TaskID(taskId), task, cancellation, disposableCancellation);
        }

        /// <summary>
        /// Observes the specified task.
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
        /// <param name="taskId">An ID that uniquely identifies the task.</param>
        /// <param name="task">The task.</param>
        /// <param name="cancellation">The Cancellation token that can be used to cancel the work.</param>
        /// <param name="disposableCancellation">A value that indicates whether the cancel token can be disposed after the task is completed.</param>
        /// <returns>A task.</returns>
        /// <exception cref="ArgumentException">
        /// The specified task ID has already been registered.
        /// </exception>
        public ValueTask<TResult> Observe<TResult>(Guid taskId, ValueTask<TResult> task, CancellationTokenSource cancellation, bool disposableCancellation = false)
        {
            return Observe(new TaskID(taskId), task, cancellation, disposableCancellation);
        }

        #endregion

    }

    /// <summary>
    /// Task observer.
    /// </summary>
    /// <typeparam name="TTaskInfo">The type of task information that including an ID that uniquely identifies the task.</typeparam>
    public class TaskObserver<TTaskInfo> where TTaskInfo : ITaskInfo
    {

        #region RunWithCancellation

        /// <summary>
        /// Runs the specified work. Use the internally generated cancel token. 
        /// </summary>
        /// <param name="taskInfo">Task information including an ID that uniquely identifies the task.</param>
        /// <param name="function">The asynchronous work.</param>
        /// <returns>A task.</returns>
        /// <exception cref="ArgumentException">
        /// The specified task ID has already been registered.
        /// </exception>
        public Task RunWithCancellation(TTaskInfo taskInfo, Func<CancellationToken, Task> function)
        {
            var cancellation = CreateDefaultCancellationTokenSource();

            return Observe(taskInfo, function(cancellation.Token), cancellation, true);
        }

        /// <summary>
        /// Runs the specified work. Use the internally generated cancel token. 
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
        /// <param name="taskInfo">Task information including an ID that uniquely identifies the task.</param>
        /// <param name="function">The asynchronous work.</param>
        /// <returns>A task.</returns>
        /// <exception cref="ArgumentException">
        /// The specified task ID has already been registered.
        /// </exception>
        public Task<TResult> RunWithCancellation<TResult>(TTaskInfo taskInfo, Func<CancellationToken, Task<TResult>> function)
        {
            var cancellation = CreateDefaultCancellationTokenSource();

            return Observe(taskInfo, function(cancellation.Token), cancellation, true);
        }

        /// <summary>
        /// Runs the specified work. Use the internally generated cancel token. 
        /// </summary>
        /// <param name="taskInfo">Task information including an ID that uniquely identifies the task.</param>
        /// <param name="function">The asynchronous work.</param>
        /// <returns>A task.</returns>
        /// <exception cref="ArgumentException">
        /// The specified task ID has already been registered.
        /// </exception>
        public ValueTask RunWithCancellation(TTaskInfo taskInfo, Func<CancellationToken, ValueTask> function)
        {
            var cancellation = CreateDefaultCancellationTokenSource();

            return Observe(taskInfo, function(cancellation.Token), cancellation, true);
        }

        /// <summary>
        /// Runs the specified work. Use the internally generated cancel token. 
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
        /// <param name="taskInfo">Task information including an ID that uniquely identifies the task.</param>
        /// <param name="function">The asynchronous work.</param>
        /// <returns>A task.</returns>
        /// <exception cref="ArgumentException">
        /// The specified task ID has already been registered.
        /// </exception>
        public ValueTask<TResult> RunWithCancellation<TResult>(TTaskInfo taskInfo, Func<CancellationToken, ValueTask<TResult>> function)
        {
            var cancellation = CreateDefaultCancellationTokenSource();
            
            return Observe(taskInfo, function(cancellation.Token), cancellation, true);
        }

        /// <summary>
        /// Creates a default cancel token.
        /// </summary>
        /// <returns>A cancel token.</returns>
        protected virtual CancellationTokenSource CreateDefaultCancellationTokenSource()
        {
            return new CancellationTokenSource();
        }

        #endregion

        #region Run

        /// <summary>
        /// Runs the specified work.
        /// </summary>
        /// <param name="taskInfo">Task information including an ID that uniquely identifies the task.</param>
        /// <param name="function">The task.</param>
        /// <param name="cancellation">The Cancellation token that can be used to cancel the work.</param>
        /// <param name="disposableCancellation">A value that indicates whether the cancel token can be disposed after the task is completed.</param>
        /// <returns>A task.</returns>
        /// <exception cref="ArgumentException">
        /// The specified task ID has already been registered.
        /// </exception>
        public Task Run(TTaskInfo taskInfo, Func<CancellationToken, Task> function, CancellationTokenSource? cancellation = null, bool disposableCancellation = false)
        {
            return Observe(taskInfo, function(cancellation?.Token ?? default), cancellation, disposableCancellation);
        }

        /// <summary>
        /// Runs the specified work.
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
        /// <param name="taskInfo">Task information including an ID that uniquely identifies the task.</param>
        /// <param name="function">The task.</param>
        /// <param name="cancellation">The Cancellation token that can be used to cancel the work.</param>
        /// <param name="disposableCancellation">A value that indicates whether the cancel token can be disposed after the task is completed.</param>
        /// <returns>A task.</returns>
        /// <exception cref="ArgumentException">
        /// The specified task ID has already been registered.
        /// </exception>
        public Task<TResult> Run<TResult>(TTaskInfo taskInfo, Func<CancellationToken, Task<TResult>> function, CancellationTokenSource? cancellation = null, bool disposableCancellation = false)
        {
            return Observe(taskInfo, function(cancellation?.Token ?? default), cancellation, disposableCancellation);
        }

        /// <summary>
        /// Runs the specified work.
        /// </summary>
        /// <param name="taskInfo">Task information including an ID that uniquely identifies the task.</param>
        /// <param name="function">The task.</param>
        /// <param name="cancellation">The Cancellation token that can be used to cancel the work.</param>
        /// <param name="disposableCancellation">A value that indicates whether the cancel token can be disposed after the task is completed.</param>
        /// <returns>A task.</returns>
        /// <exception cref="ArgumentException">
        /// The specified task ID has already been registered.
        /// </exception>
        public ValueTask Run(TTaskInfo taskInfo, Func<CancellationToken, ValueTask> function, CancellationTokenSource? cancellation = null, bool disposableCancellation = false)
        {
            return Observe(taskInfo, function(cancellation?.Token ?? default), cancellation, disposableCancellation);
        }

        /// <summary>
        /// Runs the specified work.
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
        /// <param name="taskInfo">Task information including an ID that uniquely identifies the task.</param>
        /// <param name="function">The task.</param>
        /// <param name="cancellation">The Cancellation token that can be used to cancel the work.</param>
        /// <param name="disposableCancellation">A value that indicates whether the cancel token can be disposed after the task is completed.</param>
        /// <returns>A task.</returns>
        /// <exception cref="ArgumentException">
        /// The specified task ID has already been registered.
        /// </exception>
        public ValueTask<TResult> Run<TResult>(TTaskInfo taskInfo, Func<CancellationToken, ValueTask<TResult>> function, CancellationTokenSource? cancellation = null, bool disposableCancellation = false)
        {
            return Observe(taskInfo, function(cancellation?.Token ?? default), cancellation, disposableCancellation);
        }

        #endregion

        #region Observe

        /// <summary>
        /// Observes the specified task.
        /// </summary>
        /// <param name="taskInfo">Task information including an ID that uniquely identifies the task.</param>
        /// <param name="task">The task.</param>
        /// <param name="cancellation">The Cancellation token that can be used to cancel the work.</param>
        /// <param name="disposableCancellation">A value that indicates whether the cancel token can be disposed after the task is completed.</param>
        /// <returns>A task.</returns>
        /// <exception cref="ArgumentException">
        /// The specified task ID has already been registered.
        /// </exception>
        public async Task Observe(TTaskInfo taskInfo, Task task, CancellationTokenSource? cancellation = null, bool disposableCancellation = false)
        {
            await using var context = new TaskContext(taskInfo, cancellation, disposableCancellation);

            OnStart(context);

            await task.ContinueWith(t => OnComplete(t, context)).ConfigureAwait(false);
        }

        /// <summary>
        /// Observes the specified task.
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
        /// <param name="taskInfo">Task information including an ID that uniquely identifies the task.</param>
        /// <param name="task">The task.</param>
        /// <param name="cancellation">The Cancellation token that can be used to cancel the work.</param>
        /// <param name="disposableCancellation">A value that indicates whether the cancel token can be disposed after the task is completed.</param>
        /// <returns>A task.</returns>
        /// <exception cref="ArgumentException">
        /// The specified task ID has already been registered.
        /// </exception>
        public async Task<TResult> Observe<TResult>(TTaskInfo taskInfo, Task<TResult> task, CancellationTokenSource? cancellation = null, bool disposableCancellation = false)
        {
            await using var context = new TaskContext(taskInfo, cancellation, disposableCancellation);

            OnStart(context);

            return await task.ContinueWith(t => OnComplete(t, context)).ConfigureAwait(false);
        }

        /// <summary>
        /// Observes the specified task.
        /// </summary>
        /// <param name="taskInfo">Task information including an ID that uniquely identifies the task.</param>
        /// <param name="task">The task.</param>
        /// <param name="cancellation">The Cancellation token that can be used to cancel the work.</param>
        /// <param name="disposableCancellation">A value that indicates whether the cancel token can be disposed after the task is completed.</param>
        /// <returns>A task.</returns>
        /// <exception cref="ArgumentException">
        /// The specified task ID has already been registered.
        /// </exception>
        public async ValueTask Observe(TTaskInfo taskInfo, ValueTask task, CancellationTokenSource? cancellation = null, bool disposableCancellation = false)
        {
            await using var context = new TaskContext(taskInfo, cancellation, disposableCancellation);

            OnStart(context);

            try
            {
                await task.ConfigureAwait(false);

                OnComplete(context);
            }
            catch (Exception ex)
            {
                OnFailed(context, ex);
                throw;
            }
        }

        /// <summary>
        /// Observes the specified task.
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
        /// <param name="taskInfo">Task information including an ID that uniquely identifies the task.</param>
        /// <param name="task">The task.</param>
        /// <param name="cancellation">The Cancellation token that can be used to cancel the work.</param>
        /// <param name="disposableCancellation">A value that indicates whether the cancel token can be disposed after the task is completed.</param>
        /// <returns>A task.</returns>
        /// <exception cref="ArgumentException">
        /// The specified task ID has already been registered.
        /// </exception>
        public async ValueTask<TResult> Observe<TResult>(TTaskInfo taskInfo, ValueTask<TResult> task, CancellationTokenSource? cancellation = null, bool disposableCancellation = false)
        {
            await using var context = new TaskContext(taskInfo, cancellation, disposableCancellation);

            OnStart(context);

            try
            {
                var result = await task.ConfigureAwait(false);

                OnComplete(context);

                return result;
            }
            catch (Exception ex)
            {
                OnFailed(context, ex);
                throw;
            }
        }

        #endregion

        #region OnStart

        /// <summary>
        /// Executes the process at the start of the task.
        /// </summary>
        /// <param name="context">The context that holds the state of the task.</param>
        /// <exception cref="ArgumentException">
        /// The specified task ID has already been registered.
        /// </exception>
        private void OnStart(TaskContext context)
        {
            if (!m_TaskContexts.TryAdd(context.TaskID, context))
            {
                throw new ArgumentException("The specified task ID has already been registered.", nameof(context));
            }

            NotifyStart(context.TaskInfo);
        }

        #endregion

        #region OnComplete / OnFailed

        /// <summary>
        /// Executes the process when the task is completed.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <param name="context">The context that holds the state of the task.</param>
        /// <returns>A task.</returns>
        private Task OnComplete(Task task, TaskContext context)
        {
            m_TaskContexts.Remove(context.TaskID, out _);

            context.Dispose();

            if (task.IsFaulted)
            {
                NotifyFailed(context.TaskInfo, task.Exception);

                if (task.Exception != null)
                {
                    throw task.Exception;
                }
            }
            else
            {
                NotifyCompleted(context.TaskInfo);
            }

            return task;
        }

        /// <summary>
        /// Executes the process when the task is completed.
        /// </summary>
        /// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
        /// <param name="task">The task.</param>
        /// <param name="context">The context that holds the state of the task.</param>
        /// <returns>A result returned by the task.</returns>
        private TResult OnComplete<TResult>(Task<TResult> task, TaskContext context)
        {
            m_TaskContexts.Remove(context.TaskID, out _);

            context.Dispose();

            if (task.IsFaulted)
            {
                NotifyFailed(context.TaskInfo, task.Exception);

                if (task.Exception != null)
                {
                    throw task.Exception;
                }
            }
            else
            {
                NotifyCompleted(context.TaskInfo);
            }

            return task.Result;
        }

        /// <summary>
        /// Executes the process when the task is completed.
        /// </summary>
        /// <param name="context">The context that holds the state of the task.</param>
        /// <returns></returns>
        private void OnComplete(TaskContext context)
        {
            m_TaskContexts.Remove(context.TaskID, out _);

            context.Dispose();

            NotifyCompleted(context.TaskInfo);
        }

        /// <summary>
        /// Executes the process when the task is failed.
        /// </summary>
        /// <param name="context">The context that holds the state of the task.</param>
        /// <param name="exception">Exception that occurred.</param>
        private void OnFailed(TaskContext context, Exception exception)
        {
            m_TaskContexts.Remove(context.TaskID, out _);

            context.Dispose();

            NotifyFailed(context.TaskInfo, exception);
        }

        #endregion

        #region RequestCancel

        /// <summary>
        /// Requests cancellation for the specified task.
        /// </summary>
        /// <param name="taskId">An ID that uniquely identifies the task.</param>
        /// <returns>True if it can request cancellation. Otherwise false.</returns>
        public bool RequestCancel(Guid taskId)
        {
            if (m_TaskContexts.TryGetValue(taskId, out TaskContext context))
            {
                return context.RequestCancel();
            }

            return false;
        }

        /// <summary>
        /// Requests cancellation for all registered task.
        /// </summary>
        public void RequestCancelAll()
        {
            foreach (var state in m_TaskContexts.Values)
            {
                state.RequestCancel();
            }
        }

        #endregion

        #region IsRunning

        /// <summary>
        /// Gets whether the specified task is running.
        /// </summary>
        /// <param name="taskId">An ID that uniquely identifies the task.</param>
        /// <returns>True if the task is running. Otherwise false.</returns>
        public bool IsRunning(Guid taskId)
        {
            if (!m_TaskContexts.TryGetValue(taskId, out TaskContext _)) { return false; }

            return true;
        }

        #endregion

        #region Listener

        private readonly List<ITaskListener<TTaskInfo>> m_Listeners = new List<ITaskListener<TTaskInfo>>();

        /// <summary>
        /// Registers the specified listener.
        /// </summary>
        /// <param name="onStart">The method to execute at the start of each task.</param>
        /// <param name="onCompleted">The method to execute when each task is completed.</param>
        /// <param name="onFailed">The method to execute when each task is failed.</param>
        /// <returns>An object for unregistering this listener.</returns>
        public IDisposable RegisterListener(Action<TTaskInfo>? onStart = null, Action<TTaskInfo>? onCompleted = null, Action<TTaskInfo, Exception?>? onFailed = null)
        {
            return RegisterListener(new DefaultTaskListener<TTaskInfo>(onStart, onCompleted, onFailed));
        }

        /// <summary>
        /// Registers the specified listener.
        /// </summary>
        /// <param name="listener">The listener.</param>
        /// <returns>An object for unregistering this listener.</returns>
        public IDisposable RegisterListener(ITaskListener<TTaskInfo> listener)
        {
            lock (m_Listeners)
            {
                m_Listeners.Add(listener);
            }

            return new Releaser(() => UnregisterListener(listener));
        }

        /// <summary>
        /// Unregisters the specified listener.
        /// </summary>
        /// <param name="listener">The listener.</param>
        /// <returns>True if the listener is released. Otherwise false.</returns>
        public bool UnregisterListener(ITaskListener<TTaskInfo> listener)
        {
            if (m_Listeners.Contains(listener))
            {
                lock (m_Listeners)
                {
                    return m_Listeners.Remove(listener);
                }
            }

            return false;
        }

        /// <summary>
        /// Notifies the listeners that the specified task will start.
        /// </summary>
        /// <param name="taskInfo">Task information including an ID that uniquely identifies the task.</param>
        private void NotifyStart(TTaskInfo taskInfo)
        {
            lock (m_Listeners)
            {
                foreach (var listener in m_Listeners)
                {
                    listener.OnStart(taskInfo);
                }
            }
        }

        /// <summary>
        /// Notifies the listeners that the specified task has been completed.
        /// </summary>
        /// <param name="taskInfo">Task information including an ID that uniquely identifies the task.</param>
        private void NotifyCompleted(TTaskInfo taskInfo)
        {
            lock (m_Listeners)
            {
                foreach (var listener in m_Listeners)
                {
                    listener.OnCompleted(taskInfo);
                }
            }
        }

        /// <summary>
        /// Notifies the listeners that the specified task has failed.
        /// </summary>
        /// <param name="taskInfo">Task information including an ID that uniquely identifies the task.</param>
        /// <param name="exception">Exception that occurred.</param>
        private void NotifyFailed(TTaskInfo taskInfo, Exception? exception)
        {
            lock (m_Listeners)
            {
                foreach (var listener in m_Listeners)
                {
                    listener.OnFailed(taskInfo, exception);
                }
            }
        }

        /// <summary>
        /// Object for unregistering this listener.
        /// </summary>
        private readonly struct Releaser : IDisposable
        {
            internal Releaser(Action onDispose)
            {
                m_OnDispose = onDispose;
            }

            private readonly Action m_OnDispose;

            public void Dispose()
            {
                m_OnDispose();
            }
        }

        #endregion


        #region context

        private readonly System.Collections.Concurrent.ConcurrentDictionary<Guid, TaskContext> m_TaskContexts = new System.Collections.Concurrent.ConcurrentDictionary<Guid, TaskContext>();

        /// <summary>
        /// Context that holds the state of a task.
        /// </summary>
        private readonly struct TaskContext : IDisposable, IAsyncDisposable
        {
            /// <summary>
            /// Create a new instance.
            /// </summary>
            /// <param name="taskInfo">Task information including an ID that uniquely identifies the task.</param>
            /// <param name="cancellation">The Cancellation token that can be used to cancel the work.</param>
            /// <param name="disposableCancellation">A value that indicates whether the cancel token can be disposed after the task is completed.</param>
            internal TaskContext(TTaskInfo taskInfo, CancellationTokenSource? cancellation, bool disposableCancellation)
            {
                TaskInfo = taskInfo;
                Cancellation = cancellation;
                DisposableCancellation = disposableCancellation;
            }

            /// <summary>
            /// Gets the ID that uniquely identifies the task.
            /// </summary>
            internal Guid TaskID { get => TaskInfo.TaskID; }

            /// <summary>
            /// Gets the task information.
            /// </summary>
            internal TTaskInfo TaskInfo { get; }

            /// <summary>
            /// Gets the Cancellation token that can be used to cancel the work.
            /// </summary>
            internal CancellationTokenSource? Cancellation { get; }

            /// <summary>
            /// Gets a value that indicates whether the cancel token can be disposed after the task is completed.
            /// </summary>
            internal bool DisposableCancellation { get; }

            /// <summary>
            /// Requests cancellation for this task.
            /// </summary>
            /// <returns></returns>
            internal bool RequestCancel()
            {
                if (Cancellation == null) { return false; }
                if (Cancellation.IsCancellationRequested) { return false; }

                Cancellation.Cancel();
                return true;
            }

            /// <inheritdoc/>
            public void Dispose()
            {
                if (DisposableCancellation)
                {
                    Cancellation?.Dispose();
                }
            }

            /// <inheritdoc/>
            public ValueTask DisposeAsync()
            {
                Dispose();
                return default;
            }
        }

        #endregion

    }

}
