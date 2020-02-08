using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cysharp.Diagnostics;

namespace mxProject.ProcessWorkflow.WorkflowItems
{

    /// <summary>
    /// 複数のコマンドを実行するワークフローアイテムの基底実装。
    /// </summary>
    public abstract class MultiCommandWorkflowItemBase : WorkflowItemBase
    {

        #region コンストラクタ

        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">名称</param>
        protected MultiCommandWorkflowItemBase(string id, string name) : this(id, name, null)
        {
        }

        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">名称</param>
        /// <param name="commands">コマンド</param>
        protected MultiCommandWorkflowItemBase(string id, string name, IEnumerable<IProcessCommand> commands) : base(id, name)
        {
            Commands = commands;
        }

        #endregion

        /// <summary>
        /// コマンドを取得または設定します。
        /// </summary>
        public IEnumerable<IProcessCommand> Commands { get; set; }

        #region 終了コード

        /// <summary>
        /// 終了コードの決定処理を取得または設定します。
        /// </summary>
        public IExitCodeHandler ExitCodeHandler { get; set; }

        /// <summary>
        /// 各コマンドの実行結果から終了コードを決定します。
        /// </summary>
        /// <param name="results">実行結果</param>
        /// <returns>終了コード</returns>
        protected int HandleExitCode(ProcessCommandResult[] results)
        {
            return (ExitCodeHandler ?? DefaultExitCodeHandler.Singleton).HandleExitCode(results);
        }

        /// <summary>
        /// 既定の終了コード制御。
        /// </summary>
        private sealed class DefaultExitCodeHandler : IExitCodeHandler
        {

            internal readonly static DefaultExitCodeHandler Singleton = new DefaultExitCodeHandler();

            /// <summary>
            /// 指定されたコマンドの実行結果から終了コードを決定します。
            /// </summary>
            /// <param name="results">コマンドの実行結果</param>
            /// <returns>終了コード</returns>
            public int HandleExitCode(ProcessCommandResult[] results)
            {
                foreach (ProcessCommandResult result in results)
                {
                    if (result.ExitCode != 0) { return result.ExitCode; }
                }
                return 0;
            }

        }

        #endregion

    }

}
