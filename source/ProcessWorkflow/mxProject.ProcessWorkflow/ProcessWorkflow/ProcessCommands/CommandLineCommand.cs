using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Diagnostics;

namespace mxProject.ProcessWorkflow.ProcessCommands
{

    /// <summary>
    /// コマンドライン文字列を実行するコマンド。
    /// </summary>
    public class CommandLineCommand : CommandLineCommandBase
    {

        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">名称</param>
        /// <param name="commandLine">コマンドライン文字列</param>
        /// <param name="workingDirectory">作業ディレクトリ</param>
        /// <param name="environmentVariable">環境変数</param>
        /// <param name="encoding">エンコーディング</param>
        public CommandLineCommand(ProcessCommandID id, string name, string commandLine, string workingDirectory = null, IDictionary<string, string> environmentVariable = null, Encoding encoding = null)
            : base(id, name, workingDirectory, environmentVariable, encoding)
        {
            CommandLine = commandLine;
        }

        /// <summary>
        /// コマンドライン文字列を取得します。
        /// </summary>
        public string CommandLine { get; }

        /// <summary>
        /// 処理を開始します。
        /// </summary>
        /// <param name="prevExitCode">前処理の終了コード</param>
        /// <returns>標準出力を返す非同期シーケンス</returns>
        public override ProcessAsyncEnumerable StartAsync(int prevExitCode)
        {
            IDictionary<string, string> variables = AddPrevExitCode(EnvironmentVariable, prevExitCode);

            return ProcessX.StartAsync(CommandLine, WorkingDirectory, variables, Encoding);
        }
    }

}
