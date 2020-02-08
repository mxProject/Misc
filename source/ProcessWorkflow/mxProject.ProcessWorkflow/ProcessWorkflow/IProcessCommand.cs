using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Diagnostics;

namespace mxProject.ProcessWorkflow
{

    /// <summary>
    /// コマンドに必要な機能を提供します。
    /// </summary>
    public interface IProcessCommand
    {
        /// <summary>
        /// ID を取得します。
        /// </summary>
        ProcessCommandID ID { get; }

        /// <summary>
        /// 名称を取得します。
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 処理を開始します。
        /// </summary>
        /// <param name="prevExitCode">前処理の終了コード</param>
        /// <returns>標準出力を返す非同期シーケンス</returns>
        ProcessAsyncEnumerable StartAsync(int prevExitCode);
    }

}
