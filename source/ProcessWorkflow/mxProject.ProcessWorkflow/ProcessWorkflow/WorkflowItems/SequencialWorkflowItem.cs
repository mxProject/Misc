using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cysharp.Diagnostics;

namespace mxProject.ProcessWorkflow.WorkflowItems
{

    /// <summary>
    /// 複数のコマンドを順に実行します。
    /// </summary>
    public class SequencialWorkflowItem : MultiCommandWorkflowItemBase
    {

        #region コンストラクタ

        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        /// <param name="name">名称</param>
        public SequencialWorkflowItem(string id, string name) : this(id, name, null)
        {
        }

        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="commands">コマンド</param>
        public SequencialWorkflowItem(string id, string name, IEnumerable<IProcessCommand> commands) : base(id, name, commands)
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
            ProcessCommandResult[] results = new ProcessCommandResult[commands.Length];

            for (int i = 0; i < commands.Length; ++i)
            {
                int exitCode = await ExecuteCommandAsync(commands[i], prevExitCode);
                results[i] = new ProcessCommandResult(commands[i], exitCode);
            }

            return HandleExitCode(results);

        }

    }

}
