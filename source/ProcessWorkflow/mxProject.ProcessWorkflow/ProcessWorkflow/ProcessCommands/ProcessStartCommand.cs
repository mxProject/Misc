using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Cysharp.Diagnostics;

namespace mxProject.ProcessWorkflow.ProcessCommands
{

    /// <summary>
    /// プロセス開始情報に従って処理を実行するコマンド。
    /// </summary>
    public class ProcessStartCommand : ProcessCommandBase
    {

        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">名称</param>
        /// <param name="startInfo">プロセス開始情報</param>
        public ProcessStartCommand(ProcessCommandID id, string name, ProcessStartInfo startInfo) : base(id, name)
        {
            StartInfo = startInfo;
        }

        /// <summary>
        /// プロセス開始情報を取得します。
        /// </summary>
        public ProcessStartInfo StartInfo { get; }

        /// <summary>
        /// 処理を開始します。
        /// </summary>
        /// <param name="prevExitCode">前処理の終了コード</param>
        /// <returns>標準出力を返す非同期シーケンス</returns>
        public override ProcessAsyncEnumerable StartAsync(int prevExitCode)
        {
            AddPrevExitCode(StartInfo.EnvironmentVariables, prevExitCode);

            return ProcessX.StartAsync(StartInfo);
        }
    }

}
