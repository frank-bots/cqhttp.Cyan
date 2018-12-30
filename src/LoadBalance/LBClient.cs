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
        List<int> normal_bots = new List<int> ();
        object update_normal_bots_lock = new object ();
        System.Random x = new System.Random ((int) System.DateTime.Now.ToFileTime ());
        ///
        void Add (CQApiClient singleClient) {
            bots.Add (singleClient);
            singleClient.OnEvent += (client, e) => OnEvent (client, e);
        }
        ///
        public static LBClient operator + (LBClient lbClient, CQApiClient singleClient) {
            lbClient.Add (singleClient);
            return lbClient;
        }
        ///
        public LBClient (params CQApiClient[] init_clients) {
            Task.Run (() => {
                while (true) {
                    normal_bots.Clear ();
                    Thread.Sleep (Config.checkAliveInterval);
                    lock (update_normal_bots_lock) {
                        for (int i = 0; i < bots.Count; i++)
                            if (bots[i].alive) normal_bots.Add (i);
                    }
                }
            });
            foreach (var i in init_clients) Add (i);
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
        ///
        public async Task<SendmsgResult> SendMessageAsync (
            Enums.MessageType messageType,
            long target,
            Messages.Message message
        ) {
            CQApiClient current;
            lock (update_normal_bots_lock) {
                current = bots[x.Next (normal_bots.Count)];
            }
            return await current.SendMessageAsync (messageType, target, message);
        }
        ///
        public event CQApiClient.OnEventDelegate OnEvent;
    }
}