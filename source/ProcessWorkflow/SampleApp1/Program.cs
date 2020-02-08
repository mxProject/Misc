using System;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using mxProject.ProcessWorkflow;

namespace SampleApp1
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            int exitCode = await MainAsync(args);
            Console.WriteLine($"ExitCode = {exitCode}");
            return exitCode;
        }

        private static async Task<int> MainAsync(string[] args)
        {

            // 環境変数から前処理の終了コードを取得して出力
            var variables = System.Environment.GetEnvironmentVariables();
            var key = ProcessCommandVariables.PrevExitCode;
            Console.WriteLine($"EnvironmentVariables[{key}] = {variables[key]}");

            // 引数を出力
            for (int i = 0; i < args.Length; ++i)
            {
                Console.WriteLine($"args[{i}] = {args[i]}");
            }

            // 先頭の引数は繰り返し回数を表すものとする
            int repeatCount = Convert.ToInt32(args[0]);

            if (repeatCount < 0) { return 1; }

            for (int i = 0; i < repeatCount; ++i)
            {
                await Task.Delay(1000);
                Console.WriteLine($"{i + 1}/{repeatCount}");
            }

            return 0;

        }

    }
}
