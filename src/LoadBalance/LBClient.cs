using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using cqhttp.Cyan.ApiCall.Requests.Base;
using cqhttp.Cyan.ApiCall.Results;
using cqhttp.Cyan.ApiCall.Results.Base;
using cqhttp.Cyan.Events.CQEvents.Base;
using cqhttp.Cyan.Events.CQResponses.Base;
using cqhttp.Cyan.Instance;

namespace cqhttp.Cyan.LoadBalance {
    /// <summary>
    /// 对bot进行负载均衡
    /// </summary>
    public class LBClient {
        List<CQApiClient> bots = new List<CQApiClient> ();
        List<int> normal_bots;
        object update_normal_bots_lock = new object ();
        System.Random x = new System.Random ((int) System.DateTime.Now.ToFileTime ());
        ///
        public static LBClient operator + (LBClient cli, CQApiClient acli) {
            cli.bots.Add (acli);
            return cli;
        }
        ///
        public LBClient (params CQApiClient[] init_clients) {
            Task.Run (() => {
                while (true) {
                    Thread.Sleep (100000);
                    lock (update_normal_bots_lock) {
                        for (int i = 0; i < bots.Count; i++)
                            if (bots[i].alive) normal_bots.Add (i);
                    }
                }
            });
        }
        /// <summary>
        /// 负载均衡地发送请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> SendRequestAsync (ApiRequest request) {
            CQApiClient current;
            lock (update_normal_bots_lock) {
                current = bots[x.Next (normal_bots.Count)];
            }
            return await current.SendRequestAsync (request);
        }
        /// <summary>
        /// 负载均衡发送文字
        /// </summary>
        public async Task<SendmsgResult> SendTextAsync (
            Enums.MessageType messageType,
            long target,
            string text
        ) {
            CQApiClient current;
            lock (update_normal_bots_lock) {
                current = bots[x.Next (normal_bots.Count)];
            }
            return await current.SendTextAsync (messageType, target, text);
        }
        public delegate CQResponse OnEventDelegate (CQApiClient client, CQEvent eventObj);
        public event OnEventDelegate OnEvent;
    }
}