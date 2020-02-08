using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace mxProject.ProcessWorkflow
{

    public static class ExitCodeHandlerFactory
    {

        public static ExitCodeHandler Create(int defaultExitCode, (ProcessCommandID commandId, int commandExitCode) pattern, int returnExitCode)
        {
            return Create(defaultExitCode, new[] { pattern }, returnExitCode);
        }

        public static ExitCodeHandler Create(int defaultExitCode, (ProcessCommandID commandId, int commandExitCode)[] patterns, int returnExitCode)
        {
            return Create(defaultExitCode, new[] { (patterns, returnExitCode) });
        }

        public static ExitCodeHandler Create(int defaultExitCode, ((ProcessCommandID commandId, int commandExitCode)[] patterns, int returnExitCode)[] filters)
        {

            var filterMethods = new List<ExitCodeFilter>();

            foreach (var filter in filters)
            {

                var dic = new Dictionary<ProcessCommandID, int>();

                foreach (var pattern in filter.patterns)
                {
                    dic.Add(pattern.Item1, pattern.Item2);
                }

                var obj = new ExitCodeFilterObject(dic, filter.returnExitCode);

                filterMethods.Add(obj.Match);

            }

            return new ExitCodeHandler(defaultExitCode, filterMethods);

        }

        public static ExitCodeHandler Create(int defaultExitCode, (string commandId, int commandExitCode) pattern, int returnExitCode)
        {
            return Create(defaultExitCode, new[] { pattern }, returnExitCode);
        }

        public static ExitCodeHandler Create(int defaultExitCode, (string commandId, int commandExitCode)[] patterns, int returnExitCode)
        {
            return Create(defaultExitCode, new[] { (patterns, returnExitCode) });
        }

        public static ExitCodeHandler Create(int defaultExitCode, ((string commandId, int commandExitCode)[] patterns, int returnExitCode)[] filters)
        {

            var filterMethods = new List<ExitCodeFilter>();

            foreach (var filter in filters)
            {

                var dic = new Dictionary<ProcessCommandID, int>();

                foreach (var pattern in filter.patterns)
                {
                    dic.Add(new ProcessCommandID(pattern.Item1), pattern.Item2);
                }

                var obj = new ExitCodeFilterObject(dic, filter.returnExitCode);

                filterMethods.Add(obj.Match);

            }

            return new ExitCodeHandler(defaultExitCode, filterMethods);

        }

    }

    /// <summary>
    /// 終了コードの制御に必要な機能を提供します。
    /// </summary>
    public class ExitCodeHandler : IExitCodeHandler
    {

        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        /// <param name="defaultExitCode">既定の終了コード</param>
        /// <param name="filters">フィルタ</param>
        public ExitCodeHandler(int defaultExitCode, ExitCodeFilter filter)
        {
            Filters = filter == null ? null : new ReadOnlyCollection<ExitCodeFilter>(new ExitCodeFilter[] { filter });
            DefaultExitCode = defaultExitCode;
        }

        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        /// <param name="defaultExitCode">既定の終了コード</param>
        /// <param name="filters">フィルタ</param>
        public ExitCodeHandler(int defaultExitCode, IEnumerable<ExitCodeFilter> filters)
        {
            Filters = filters == null ? null : new ReadOnlyCollection<ExitCodeFilter>(filters?.ToArray());
            DefaultExitCode = defaultExitCode;
        }

        /// <summary>
        /// フィルタを取得します。
        /// </summary>
        public ICollection<ExitCodeFilter> Filters { get; }

        /// <summary>
        /// 既定の終了コードを取得します。
        /// </summary>
        public int DefaultExitCode { get; }

        /// <summary>
        /// 指定されたコマンドの実行結果から終了コードを決定します。
        /// </summary>
        /// <param name="results">コマンドの実行結果</param>
        /// <returns>終了コード</returns>
        public int HandleExitCode(ProcessCommandResult[] results)
        {
            if (Filters != null)
            {
                foreach (ExitCodeFilter filter in Filters)
                {
                    if (filter(results, out int exitcode)) { return exitcode; }
                }
            }

            return DefaultExitCode;
        }

    }

    /// <summary>
    /// 指定された実行結果が条件に合致する場合に終了コードを返すメソッドであることを表します。
    /// </summary>
    /// <param name="results">コマンドの実行結果</param>
    /// <param name="exitCode">終了コード</param>
    /// <returns>実行結果が条件に合致する場合、true を返します。</returns>
    public delegate bool ExitCodeFilter(ProcessCommandResult[] results, out int exitCode);


    public class ExitCodeFilterObject
    {

        public ExitCodeFilterObject(IDictionary<ProcessCommandID, int> patterns, int exitCode)
        {
            m_Patterns = patterns;
            ExitCode = exitCode;
        }

        private IDictionary<ProcessCommandID, int> m_Patterns;

        /// <summary>
        /// 終了コードを取得します。
        /// </summary>
        public int ExitCode { get; }

        /// <summary>
        /// 指定された実行結果が条件に合致するかどうかを取得します。
        /// </summary>
        /// <param name="exitCode">終了コード</param>
        /// <param name="results">コマンドの実行結果</param>
        /// <returns>実行結果が条件に合致する場合、true を返します。</returns>
        public bool Match(ProcessCommandResult[] results, out int exitCode)
        {
            foreach (var result in results)
            {
                if (m_Patterns.TryGetValue(result.Command.ID, out int exit))
                {
                    if (exit != result.ExitCode)
                    {
                        exitCode = default;
                        return false;
                    }
                }
            }

            exitCode = ExitCode;
            return true;
        }

    }

}
