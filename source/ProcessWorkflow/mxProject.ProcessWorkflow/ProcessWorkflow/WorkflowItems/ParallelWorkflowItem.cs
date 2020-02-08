using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cysharp.Diagnostics;

namespace mxProject.ProcessWorkflow.WorkflowItems
{

    /// <summary>
    /// 複数のコマンドを並列実行します。
    /// </summary>
    public class ParallelWorkflowItem : MultiCommandWorkflowItemBase
    {

        #region コンストラクタ

        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">名称</param>
        public ParallelWorkflowItem(string id, string name) : this(id, name, null)
        {
        }

        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">名称</param>
        /// <param name="commands">コマンド</param>
        public ParallelWorkflowItem(string id, string name, IEnumerable<IProcessCommand> commands) : base(id, name, commands)
        {
        }

        #endregion

        /// <summary>
        /// 処理を実行します。
        /// </summary>
        /// <param name="prevExitCode">前処理の終了コード</param>
        /// <returns>終了コード</returns>
        public override async Task<int> ExecuteAsync(int prevExitCode)
        {

            IProcessCommand[] commands = Commands.ToArray();

            Task<int>[] tasks = new Task<int>[commands.Length];

            for (int i = 0; i < commands.Length; ++i)
            {
                tasks[i] = ExecuteCommandAsync(commands[i], prevExitCode);
            }

            int[] exitCodes = await Task.WhenAll(tasks);

            ProcessCommandResult[] results = new ProcessCommandResult[commands.Length];

            for (int i = 0; i < commands.Length; ++i)
            {
                results[i] = new ProcessCommandResult(commands[i], exitCodes[i]);
            }

            return HandleExitCode(results);

        }

    }

}
