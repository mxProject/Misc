using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cysharp.Diagnostics;

namespace mxProject.ProcessWorkflow.WorkflowItems
{

    /// <summary>
    /// ワークフローアイテムの基本実装。
    /// </summary>
    public abstract class WorkflowItemBase : IWorkflowItem
    {

        #region コンストラクタ

        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">名称</param>
        protected WorkflowItemBase(string id, string name)
        {
            ID = id;
            Name = name;
        }

        #endregion

        #region 設定値

        /// <summary>
        /// ID を取得します。
        /// </summary>
        public string ID { get; }

        /// <summary>
        /// 名称を取得します。
        /// </summary>
        public string Name { get; }

        #endregion

        #region 実行

        /// <summary>
        /// 処理を実行します。
        /// </summary>
        /// <param name="prevExitCode">前処理の終了コード</param>
        /// <returns>終了コード</returns>
        public abstract Task<int> ExecuteAsync(int prevExitcode);

        /// <summary>
        /// 指定されたコマンドを実行します。
        /// </summary>
        /// <param name="command">コマンド</param>
        /// <param name="prevExitCode">前処理の終了コード</param>
        /// <returns></returns>
        protected async Task<int> ExecuteCommandAsync(IProcessCommand command, int prevExitCode)
        {
            try
            {
                await foreach (string item in command.StartAsync(prevExitCode))
                {
                    Console.WriteLine($"[{command.Name}] {item}");
                }
                return 0;
            }
            catch (ProcessErrorException ex)
            {
                Console.WriteLine(ex.Message);
                return ex.ExitCode;
            }
        }

        #endregion

        #region 次処理

        /// <summary>
        /// 処理が成功したときの次処理を取得または設定します。
        /// </summary>
        public IWorkflowItem NextOnSucceed { get; set; }

        /// <summary>
        /// 処理が失敗したときの次処理を取得または設定します。
        /// </summary>
        public IWorkflowItem NextOnFailed { get; set; }

        #endregion

    }

}
