using System;
using System.Collections.Generic;
using System.Text;

namespace mxProject.ProcessWorkflow
{

    /// <summary>
    /// 終了コードの制御に必要な機能を提供します。
    /// </summary>
    public interface IExitCodeHandler
    {

        /// <summary>
        /// 指定されたコマンドの実行結果から終了コードを決定します。
        /// </summary>
        /// <param name="results">コマンドの実行結果</param>
        /// <returns>終了コード</returns>
        int HandleExitCode(ProcessCommandResult[] results);

    }

}
