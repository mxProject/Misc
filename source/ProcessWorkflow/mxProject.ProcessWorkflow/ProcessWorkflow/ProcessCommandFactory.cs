using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using mxProject.ProcessWorkflow.ProcessCommands;

namespace mxProject.ProcessWorkflow
{

    /// <summary>
    /// コマンドファクトリー。
    /// </summary>
    public static class ProcessCommandFactory
    {

        /// <summary>
        /// コマンドライン文字列を指定してコマンドを生成します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">名称</param>
        /// <param name="commandLine">コマンドライン文字列</param>
        /// <param name="workingDirectory">作業ディレクトリ</param>
        /// <param name="environmentVariable">環境変数</param>
        /// <param name="encoding">エンコーディング</param>
        /// <returns>コマンド</returns>
        public static IProcessCommand FromCommandLine(string id, string name, string commandLine, string workingDirectory = null, IDictionary<string, string> environmentVariable = null, Encoding encoding = null)
        {
            return FromCommandLine(new ProcessCommandID(id), name, commandLine, workingDirectory, environmentVariable, encoding);
        }

        /// <summary>
        /// コマンドライン文字列を指定してコマンドを生成します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">名称</param>
        /// <param name="commandLine">コマンドライン文字列</param>
        /// <param name="workingDirectory">作業ディレクトリ</param>
        /// <param name="environmentVariable">環境変数</param>
        /// <param name="encoding">エンコーディング</param>
        /// <returns>コマンド</returns>
        public static IProcessCommand FromCommandLine(ProcessCommandID id, string name, string commandLine, string workingDirectory = null, IDictionary<string, string> environmentVariable = null, Encoding encoding = null)
        {
            return new CommandLineCommand(id, name, commandLine, workingDirectory, environmentVariable, encoding);
        }

        /// <summary>
        /// ファイルパスを指定してコマンドを生成します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">名称</param>
        /// <param name="filePath">ファイルパス</param>
        /// <param name="arguments">起動時引数</param>
        /// <param name="workingDirectory">作業ディレクトリ</param>
        /// <param name="environmentVariable">環境変数</param>
        /// <param name="encoding">エンコーディング</param>
        /// <returns>コマンド</returns>
        public static IProcessCommand FromFile(string id, string name, string filePath, string arguments = null, string workingDirectory = null, IDictionary<string, string> environmentVariable = null, Encoding encoding = null)
        {
            return FromFile(new ProcessCommandID(id), name, filePath, arguments, workingDirectory, environmentVariable, encoding);
        }

        /// <summary>
        /// ファイルパスを指定してコマンドを生成します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">名称</param>
        /// <param name="filePath">ファイルパス</param>
        /// <param name="arguments">起動時引数</param>
        /// <param name="workingDirectory">作業ディレクトリ</param>
        /// <param name="environmentVariable">環境変数</param>
        /// <param name="encoding">エンコーディング</param>
        /// <returns>コマンド</returns>
        public static IProcessCommand FromFile(ProcessCommandID id, string name, string filePath, string arguments = null, string workingDirectory = null, IDictionary<string, string> environmentVariable = null, Encoding encoding = null)
        {
            return new FileCommand(id, name, filePath, arguments, workingDirectory, environmentVariable, encoding);
        }

        /// <summary>
        /// プロセス開始情報を指定してコマンドを生成します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">名称</param>
        /// <param name="startInfo">プロセス開始情報</param>
        /// <returns>コマンド</returns>
        public static IProcessCommand FromStartInfo(string id, string name, ProcessStartInfo startInfo)
        {
            return FromStartInfo(new ProcessCommandID(id), name, startInfo);
        }

        /// <summary>
        /// プロセス開始情報を指定してコマンドを生成します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">名称</param>
        /// <param name="startInfo">プロセス開始情報</param>
        /// <returns>コマンド</returns>
        public static IProcessCommand FromStartInfo(ProcessCommandID id, string name, ProcessStartInfo startInfo)
        {
            return new ProcessStartCommand(id, name, startInfo);
        }

    }

}
