using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Diagnostics;

namespace mxProject.ProcessWorkflow.ProcessCommands
{

    /// <summary>
    /// ファイルを実行するコマンド。
    /// </summary>
    public class FileCommand : CommandLineCommandBase
    {

        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">名称</param>
        /// <param name="filePath">ファイルパス</param>
        /// <param name="arguments">起動時引数</param>
        /// <param name="workingDirectory">作業ディレクトリ</param>
        /// <param name="environmentVariable">環境変数</param>
        /// <param name="encoding">エンコーディング</param>
        public FileCommand(ProcessCommandID id, string name, string filePath, string arguments = null, string workingDirectory = null, IDictionary<string, string> environmentVariable = null, Encoding encoding = null)
            : base(id, name, workingDirectory, environmentVariable, encoding)
        {
            FilePath = filePath;
            Arguments = arguments;
        }

        /// <summary>
        /// ファイルパスを取得します。
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// 起動時引数を取得します。
        /// </summary>
        public string Arguments { get; }

        /// <summary>
        /// 処理を開始します。
        /// </summary>
        /// <param name="prevExitCode">前処理の終了コード</param>
        /// <returns>標準出力を返す非同期シーケンス</returns>
        public override ProcessAsyncEnumerable StartAsync(int prevExitCode)
        {
            IDictionary<string, string> variables = AddPrevExitCode(EnvironmentVariable, prevExitCode);

            return ProcessX.StartAsync(FilePath, Arguments, WorkingDirectory, variables, Encoding);
        }
    }

}
