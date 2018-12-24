using System;
using System.Threading.Tasks;
using cqhttp.Cyan.ApiCall.Requests;
using cqhttp.Cyan.ApiCall.Requests.Base;
using cqhttp.Cyan.ApiCall.Result;
using cqhttp.Cyan.ApiCall.Result.Base;
using cqhttp.Cyan.Enums;
using cqhttp.Cyan.Events.CQEvents.Base;
using cqhttp.Cyan.Events.CQEvents.CQResponses.Base;
using cqhttp.Cyan.Events.EventListener;
using cqhttp.Cyan.Events.MetaEvents;
using cqhttp.Cyan.Messages;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Instance {
    /// <summary></summary>
    public abstract class CQApiClient {
        /// <summary>
        /// 酷Q配置中的access_token
        /// <see>https://cqhttp.cc/docs/4.6/#/API?id=%E8%AF%B7%E6%B1%82%E6%96%B9%E5%BC%8F</see>
        /// </summary>
        public string accessToken = "";
        /// <summary>
        /// API地址
        /// </summary>
        public string accessUrl;
        /// <summary>
        /// 当前实例的QQ号
        /// </summary>
        public long self_id { get; private set; }
        /// <summary>
        /// 当前实例的QQ昵称
        /// </summary>
        public string self_nick { get; private set; }
        /// <summary>
        /// 表示插件是否正常运行
        /// </summary>
        public bool alive { get; private set; }

        /// <summary>
        /// 指向本实例的群组记录对象
        /// </summary>
        public Utils.GroupTable groupTable = null;

        /// <summary></summary>
        public CQApiClient (string accessUrl, string accessToken = "") {
            this.accessToken = accessToken;
            this.accessUrl = accessUrl;
            if (!Initiate ().Result) throw new Exceptions.ErrorApicallException ();
        }
        private async Task<bool> Initiate () {
            ApiResult loginInfo = await SendRequestAsync (new GetLoginInfoRequest ());
            this.self_id = loginInfo.raw_data["user_id"].ToObject<long> ();
            this.self_nick = loginInfo.raw_data["nickname"].ToString ();
            return true;
        }
        /// <summary>通用发送请求函数，一般不需调用</summary>
        public virtual Task<ApiResult> SendRequestAsync (ApiRequest x) {
            if (groupTable != null) {
                if (x is GetGroupListRequest) {
                    GetGroupListResult res = x.response as GetGroupListResult;
                    foreach (var i in res.groupList)
                        groupTable[i.Item1] = i.Item2;
                }
            }
            return null;
        }
        /// <summary>发送消息(自行构造)</summary>
        public async Task<ApiResult> SendMessageAsync (
            MessageType messageType,
            long target,
            Message message
        ) {
            return await SendRequestAsync (new SendmsgRequest (messageType, target, message));
        }
        /// <summary>发送纯文本消息</summary>
        public async Task<ApiResult> SendTextAsync (
            MessageType messageType,
            long target,
            string text
        ) {
            return await SendRequestAsync (new SendmsgRequest (messageType, target,
                new Message { data = new System.Collections.Generic.List<Messages.CQElements.Base.Element> { new Messages.CQElements.ElementText (text) } }
            ));
        }

        //////////////////////////////////////////////////////////////////////////////////////
        /// <summary></summary>
        protected CQEventListener __eventListener;
        /// <summary></summary>
        public delegate CQResponse OnEvent (CQApiClient client, CQEvent eventObj);
        /// <summary></summary>
        public event OnEvent OnEventDelegate;
        /// <summary></summary>
        protected CQResponse __HandleEvent (CQEvent event_) {
            Logger.Log (Verbosity.INFO, $"收到了完整的上报事件{event_.postType}");
            if (event_ is MetaEvent) {
                if (event_ is HeartbeatEvent) {
                    if ((event_ as HeartbeatEvent).status.online)
                        alive = true;
                    else alive = false;
                } else if (event_ is LifecycleEvent) {
                    if ((event_ as LifecycleEvent).enabled)
                        alive = true;
                    else alive = false;
                }
                Logger.Log (Verbosity.INFO, $"根据元事件判定cqhttp状态是否正常:{alive}");
                return new Events.CQEvents.CQResponses.EmptyResponse ();
            }
            return OnEventDelegate (this, event_);
        }
    }
}