using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Events.CQResponses.Base {
    /// <summary></summary>
    public abstract class CQReplyMessageResponse : CQResponse {
        /// <summary></summary>
        [JsonProperty]
        JToken reply;
        /// <summary></summary>
        [JsonProperty]
        bool auto_escape;
        /// <summary></summary>
        public CQReplyMessageResponse (Messages.Message reply, bool auto_escape) {
            this.reply = reply.ToString ();
            this.auto_escape = auto_escape;
        }
    }
}