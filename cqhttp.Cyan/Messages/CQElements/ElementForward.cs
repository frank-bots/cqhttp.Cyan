using System;
using System.Collections.Generic;
using cqhttp.Cyan.ApiCall.Requests;
using cqhttp.Cyan.ApiCall.Results;
using cqhttp.Cyan.Clients;
using cqhttp.Cyan.Messages.CQElements.Base;
using Newtonsoft.Json;

namespace cqhttp.Cyan.Messages.CQElements {
    /// <summary>
    /// at某人
    /// </summary>
    public class ElementForward : Element {
        /// <summary>合并转发节点</summary>
        public class ElementNode : Element {
            /// <summary></summary>
            [JsonProperty]
            public new Dictionary<string, object> data { get; set; }

            /// <summary>发送者昵称</summary>
            public string nickname { get; set; }
            /// <summary>发送者 QQ 号</summary>
            public long user_id { get; set; }
            /// <summary>消息内容</summary>
            public Message content { get; set; }
            /// <summary></summary>
            public ElementNode (string nickname, long user_id, Message content)
            : base ("node") {
                this.data = new Dictionary<string, object> {
                    ["nickname"] = this.nickname,
                    ["user_id"] = user_id.ToString (),
                    ["content"] = content,
                };
                this.nickname = nickname;
                this.user_id = user_id;
                this.content = content;
            }
        }
        string forward_id;
        List<ElementNode> messages = null;
        /// <param name="forward_id">合并转发 id</param>
        public ElementForward (string forward_id)
        : base ("forward", ("id", forward_id)) {
            this.forward_id = forward_id;
            this.isSingle = true;
        }
        ///
        public ElementForward (List<ElementNode> messages) : base ("forward") {
            this.messages = messages;
        }
        /// <summary>获取回复的消息内容</summary>
        public async System.Threading.Tasks.Task<List<ElementNode>> GetMessage (CQApiClient client) {
            if (messages != null) return messages;
            var result = await client.SendRequestAsync (
                new GetForwardMsgRequest (forward_id)
            ) as GetForwardMsgResult;
            messages = result.messages;
            return messages;
        }
    }
}