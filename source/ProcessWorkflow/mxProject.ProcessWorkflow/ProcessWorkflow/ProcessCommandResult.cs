using System;
using System.Collections.Generic;
using System.Text;

namespace mxProject.ProcessWorkflow
{

    /// <summary>
    /// コマンドの実行結果。
    /// </summary>
    public readonly struct ProcessCommandResult
    {

        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        /// <param name="command">コマンド</param>
        /// <param name="exitCode">終了コード</param>
        public ProcessCommandResult(IProcessCommand command, int exitCode)
        {
            Command = command;
            ExitCode = exitCode;
        }

        /// <summary>
        /// コマンドを取得します。
        /// </summary>
        public IProcessCommand Command { get; }

        /// <summary>
        /// 終了コードを取得します。
        /// </summary>
        public int ExitCode { get; }

    }

}
