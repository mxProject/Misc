using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Diagnostics;

namespace mxProject.ProcessWorkflow.ProcessCommands
{

    /// <summary>
    /// コマンドライン文字列を実行するコマンドの基底実装。
    /// </summary>
    public abstract class CommandLineCommandBase : ProcessCommandBase
    {

        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">名称</param>
        /// <param name="workingDirectory">作業ディレクトリ</param>
        /// <param name="environmentVariable">環境変数</param>
        /// <param name="encoding">エンコーディング</param>
        protected CommandLineCommandBase(ProcessCommandID id, string name, string workingDirectory = null, IDictionary<string, string> environmentVariable = null, Encoding encoding = null) : base(id, name)
        {
            WorkingDirectory = workingDirectory;
            EnvironmentVariable = environmentVariable;
            Encoding = encoding;
        }

        /// <summary>
        /// 作業ディレクトリを取得します。
        /// </summary>
        public string WorkingDirectory { get; }

        /// <summary>
        /// 環境変数を取得します。
        /// </summary>
        public IDictionary<string, string> EnvironmentVariable { get; }

        /// <summary>
        /// エンコーディングを取得します。
        /// </summary>
        public Encoding Encoding { get; }

    }

}
