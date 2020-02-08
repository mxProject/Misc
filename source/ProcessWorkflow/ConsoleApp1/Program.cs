using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Diagnostics;
using mxProject.ProcessWorkflow;
using mxProject.ProcessWorkflow.WorkflowItems;
using mxProject.ProcessWorkflow.ProcessCommands;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {

            string filePath = @"E:\Data\Program\Source\Work\ProcessX\ConsoleApp1\SampleApp1\bin\Debug\netcoreapp3.1\SampleApp1.exe";

            var item = new ParallelWorkflowItem("root", "最初の処理")
            {
                // 実行するコマンド（並列実行）
                Commands = new[]{
                    ProcessCommandFactory.FromCommandLine("command1", "コマンド1", filePath + " 5")
                    , ProcessCommandFactory.FromCommandLine("command2", "コマンド2", filePath + " 4")
                }
                ,
                // 終了コードの制御は既定
                // 何れかのコマンドの終了コードが 0 でない場合、最初に見つかった終了コードを返す
                ExitCodeHandler = null
                ,
                // 成功時の次処理
                NextOnSucceed = new SequencialWorkflowItem("root-succeed", "成功時の後処理")
                {
                    // 実行するコマンド（直列実行）
                    Commands = new[]
                    {
                        ProcessCommandFactory.FromFile("command3", "コマンド3", filePath, "3")
                        , ProcessCommandFactory.FromFile("command4", "コマンド4", filePath, "-2")
                    }
                    ,
                    // 終了コードの制御
                    ExitCodeHandler = ExitCodeHandlerFactory.Create(
                        // 既定の終了コード
                        -1
                        , new[]
                        {
                            // command3 の終了コード = 0 && command4 の終了コード = 0 => 0
                            (new[] { ("command3", 0), ("command4", 0) }, 0)
                            // command3 の終了コード = 1 && command4 の終了コード = 1 => 11
                            , (new[] { ("command3", 1), ("command4", 1) }, 11)
                            // command4 の終了コード = 1 => 10
                            , (new[] { ("command4", 1) }, 10)
                        }
                    )
                    ,
                    // 成功時の次処理
                    NextOnSucceed = new WorkflowItem("root-succeed-succeed", "成功時の後処理")
                    {
                        Command = ProcessCommandFactory.FromFile("command5", "コマンド5", filePath, "1")
                    }
                    ,
                    // 失敗時の次処理
                    NextOnFailed = new WorkflowItem("root-succeed-failed", "失敗時の後処理")
                    {
                        // 実行するコマンド
                        Command = ProcessCommandFactory.FromFile("command6", "コマンド6", filePath, "2")
                    }
                }
                ,
                // 失敗時の次処理
                NextOnFailed = new WorkflowItem("root-failed", "失敗時の後処理")
                {
                    // 実行するコマンド
                    Command = ProcessCommandFactory.FromFile("command7", "コマンド7", filePath, "1")
                }
            };


            // ワークフローを実行する
            IWorkflowItem current = item;
            int exitCode = 0;

            try
            {
                while (current != null)
                {
                    Console.WriteLine($"----- {current.ID}:{current.Name} -----");
                    exitCode = await current.ExecuteAsync(exitCode);
                    Console.WriteLine($"{current.ID}.ExitCode = {exitCode}");

                    if (exitCode == 0)
                    {
                        current = current.NextOnSucceed;
                    }
                    else
                    {
                        current = current.NextOnFailed;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

    }


}
