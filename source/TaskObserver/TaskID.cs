using System;
using System.Collections.Generic;
using System.Text;

namespace mxProject.Tasks
{

    /// <summary>
    /// ID that uniquely identifies the task.
    /// </summary>
    public readonly struct TaskID : ITaskInfo, IEquatable<TaskID>
    {

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="taskId">An ID that uniquely identifies the task.</param>
        public TaskID(Guid taskId)
        {
            Value = taskId;
        }

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="taskId">An ID that uniquely identifies the task.</param>
        /// <returns></returns>
        public static TaskID FromGuid(Guid taskId)
        {
            return new TaskID(taskId);
        }

        /// <summary>
        /// Gets the ID that uniquely identifies the task.
        /// </summary>
        public Guid Value { get; }

        Guid ITaskInfo.TaskID { get => Value; }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return obj is TaskID iD && Equals(iD);
        }

        /// <inheritdoc/>
        public bool Equals(TaskID other)
        {
            return Value.Equals(other.Value);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Value.ToString();
        }

    }

}
