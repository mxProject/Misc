using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cysharp.Diagnostics;

namespace mxProject.ProcessWorkflow.WorkflowItems
{
    
    /// <summary>
    /// 単一のコマンドを実行します。
    /// </summary>
    public class WorkflowItem : WorkflowItemBase
    {

        #region コンストラクタ

        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">名称</param>
        public WorkflowItem(string id, string name) : this(id, name, null)
        {
        }

        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">名称</param>
        /// <param name="command">コマンド</param>
        public WorkflowItem(string id, string name, IProcessCommand command) : base(id, name)
        {
            Command = command;
        }

        #endregion

        /// <summary>
        /// コマンドを取得または設定します。
        /// </summary>
        public IProcessCommand Command { get; set; }

        /// <summary>
        /// 処理を実行します。
        /// </summary>
        /// <param name="prevExitCode">前処理の終了コード</param>
        /// <returns>終了コード</returns>
        public override async Task<int> ExecuteAsync(int prevExitCode)
        {
            try
            {
                await ExecuteCommandAsync(Command, prevExitCode);
                return 0;
            }
            catch (ProcessErrorException ex)
            {
                return ex.ExitCode;
            }
        }

    }

}
