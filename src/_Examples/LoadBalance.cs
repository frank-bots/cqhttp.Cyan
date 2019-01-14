using cqhttp.Cyan.Instance;
using cqhttp.Cyan.LoadBalance;
namespace cqhttp.Cyan.Examples {
    /// <summary>
    /// 负载均衡的使用
    /// </summary>
    public class LoadBalance {
        ///
        public static void Main_ () {
            var manager = new LBClient (
                new CQHTTPClient (
                    accessUrl: "http://egurl1.com"
                ),
                new CQWebsocketClient(
                    accessUrl: "ws://egurl3.com"
                )
            );
            manager += new CQHTTPClient (
                accessUrl: "http://egurl2.com"
            );
            var supress_await = manager.SendTextAsync(
                Enums.MessageType.private_,
                1234567890,
                "example message"
            );
            System.Console.ReadLine();
        }
    }
}