using System;
using System.Collections.Generic;
using System.Text;

namespace mxProject.Tasks
{

    /// <summary>
    /// Default task listener.
    /// </summary>
    internal readonly struct DefaultTaskListener<TTaskInfo> : ITaskListener<TTaskInfo> where TTaskInfo : ITaskInfo
    {
        internal DefaultTaskListener(Action<TTaskInfo>? onStart = null, Action<TTaskInfo>? onCompleted = null, Action<TTaskInfo, Exception?>? onFailed = null)
        {
            m_OnStart = onStart;
            m_OnCompleted = onCompleted;
            m_OnFailed = onFailed;
        }

        private readonly Action<TTaskInfo>? m_OnStart;
        private readonly Action<TTaskInfo>? m_OnCompleted;
        private readonly Action<TTaskInfo, Exception?>? m_OnFailed;

        /// <inheritdoc/>
        public void OnStart(TTaskInfo taskInfo)
        {
            m_OnStart?.Invoke(taskInfo);
        }

        /// <inheritdoc/>
        public void OnCompleted(TTaskInfo taskInfo)
        {
            m_OnCompleted?.Invoke(taskInfo);
        }

        /// <inheritdoc/>
        public void OnFailed(TTaskInfo taskInfo, Exception? exception)
        {
            m_OnFailed?.Invoke(taskInfo, exception);
        }
    }

}
