using System;
using System.Collections.Generic;
using System.Text;

namespace mxProject.Tasks
{

    /// <summary>
    /// Provides the functionality required for task information.
    /// </summary>
    public interface ITaskInfo
    {
        /// <summary>
        /// Gets the ID that uniquely identifies the task.
        /// </summary>
        Guid TaskID { get; }
    }

}
