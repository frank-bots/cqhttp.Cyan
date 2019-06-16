using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Events.CQResponses.Base {
    /// <summary></summary>
    public abstract class CQReplyMessageResponse : CQResponse {
        /// <summary></summary>
        [Newtonsoft.Json.JsonProperty]
        JToken reply;
        /// <summary></summary>
        [Newtonsoft.Json.JsonProperty]
        bool auto_escape;
        /// <summary></summary>
        public CQReplyMessageResponse (Messages.Message reply, bool auto_escape) {
            this.reply = JToken.Parse (reply.ToString ());
            this.auto_escape = auto_escape;
        }
    }
}