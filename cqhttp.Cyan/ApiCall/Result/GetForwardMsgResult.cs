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
                // for compatibility with go-cqhttp
                this.messages.Add (new ElementForward.ElementNode (
                    node.GetValue ("name")?.ToObject<string> () ?? node.GetValue ("nickname")?.ToObject<string> (),
                    node.GetValue ("uin")?.ToObject<long> () ?? node.GetValue ("user_id")?.ToObject<long> () ?? 0,
                    node["content"].ToObject<Messages.Message> ()
                ));
            }
        }
    }
}