using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Cysharp.Diagnostics;

namespace mxProject.ProcessWorkflow.ProcessCommands
{

    /// <summary>
    /// コマンドの基本実装。
    /// </summary>
    public abstract class ProcessCommandBase : IProcessCommand
    {

        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">名称</param>
        protected ProcessCommandBase(ProcessCommandID id, string name)
        {
            ID = id;
            Name = name;
        }

        /// <summary>
        /// ID を取得します。
        /// </summary>
        public ProcessCommandID ID { get; }

        /// <summary>
        /// 名称を取得します。
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 処理を開始します。
        /// </summary>
        /// <param name="prevExitCode">前処理の終了コード</param>
        /// <returns>標準出力を返す非同期シーケンス</returns>
        public abstract ProcessAsyncEnumerable StartAsync(int prevExitCode);

        /// <summary>
        /// 指定された環境変数に「前処理の終了コード」を追加します。
        /// </summary>
        /// <param name="environmentVariable">環境変数</param>
        /// <param name="prevExitCode">前処理の終了コード</param>
        /// <returns></returns>
        protected IDictionary<string, string> AddPrevExitCode(IDictionary<string, string> environmentVariable, int prevExitCode)
        {
            if (environmentVariable == null) environmentVariable = new Dictionary<string, string>();

            environmentVariable[ProcessCommandVariables.PrevExitCode] = prevExitCode.ToString();

            return environmentVariable;
        }

        /// <summary>
        /// 指定された環境変数に「前処理の終了コード」を追加します。
        /// </summary>
        /// <param name="environmentVariable">環境変数</param>
        /// <param name="prevExitCode">前処理の終了コード</param>
        /// <returns></returns>
        protected StringDictionary AddPrevExitCode(StringDictionary environmentVariable, int prevExitCode)
        {
            if (environmentVariable == null) environmentVariable = new StringDictionary();

            environmentVariable[ProcessCommandVariables.PrevExitCode] = prevExitCode.ToString();

            return environmentVariable;
        }

    }

}
