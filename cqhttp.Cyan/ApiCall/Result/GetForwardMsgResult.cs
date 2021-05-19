using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using cqhttp.Cyan.Messages.CQElements;

namespace cqhttp.Cyan.ApiCall.Results {
    ///
    public class GetForwardMsgResult : Base.ApiResult {
        ///
        public List<ElementForward.ElementNode> messages =
            new List<ElementForward.ElementNode> ();
        ///
        public override void Parse (JToken result) {
            PreCheck (result);
            var messages = result["data"]["messages"] as JArray;
            if (messages == null)
                throw new Exceptions.ErrorMessageException ("转发消息获取异常");
            foreach (JObject node in messages) {
                node.TryGetValue ("sender", out var sender);
                // for compatibility with go-cqhttp
                this.messages.Add (new ElementForward.ElementNode (
                    sender?["nickname"].ToObject<string> () ?? node["nickname"].ToObject<string> (),
                    sender?["user_id"].ToObject<long> () ?? node["user_id"].ToObject<long> (),
                    node["content"].ToObject<Messages.Message> ()
                ));
            }
        }
    }
}