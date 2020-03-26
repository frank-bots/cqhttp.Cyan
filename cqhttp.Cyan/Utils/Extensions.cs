using System.Threading.Tasks;

namespace cqhttp.Cyan.Utils {
    internal static class Extensions {
        /// <summary>
        /// 若condition在<see cref="Config.timeout"/>秒后仍然为否, 抛出异常
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="e">超时后抛出的异常</param>
        /// <param name="interval">检查条件的间隔(毫秒)</param>
        internal static async Task TimeOut (
            this System.Func<bool> condition,
            string e,
            int interval = 200
        ) {
            int cnt = 0;
            while (condition () == false && cnt++ * interval < Config.timeout * 1000)
                await Task.Delay (interval);
            if (condition () == false) {
                Log.Error ($"操作超时: {e}");
                throw new Exceptions.NetworkFailureException (e);
            }
        }
    }
}