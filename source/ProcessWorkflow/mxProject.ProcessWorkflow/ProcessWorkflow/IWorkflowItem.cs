using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace mxProject.ProcessWorkflow
{

    /// <summary>
    /// ワークフローアイテムに必要な機能を提供します。
    /// </summary>
    public interface IWorkflowItem
    {

        /// <summary>
        /// ID を取得します。
        /// </summary>
        string ID { get; }

        /// <summary>
        /// 名称を取得します。
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 処理を実行します。
        /// </summary>
        /// <param name="prevExitCode">前処理の終了コード</param>
        /// <returns>終了コード</returns>
        Task<int> ExecuteAsync(int prevExitCode);

        /// <summary>
        /// 処理が成功したときの次処理を取得または設定します。
        /// </summary>
        IWorkflowItem NextOnSucceed { get; set; }

        /// <summary>
        /// 処理が失敗したときの次処理を取得または設定します。
        /// </summary>
        IWorkflowItem NextOnFailed { get; set; }

    }

}
