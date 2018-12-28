using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.ApiCall.Result.Base {
    /// <summary>
    /// 调用API时cqhttp的响应
    /// </summary>
    [JsonObject]
    public abstract class ApiResult {
        /// <summary>
        /// "ok","async","failed"
        /// 参照<see>https://cqhttp.cc/docs/4.6/#/API</see>中有关status的说明
        /// </summary>
        /// <value></value>
        [Obsolete ("收到错误的API响应将抛出异常,不用在此作出判断", true)]
        [JsonProperty ("status")]
        public string status { get; private set; } //"ok","async","failed"

        /// <summary>
        /// status ok为0,async为1,failed参照链接
        /// <see>https://d.cqp.me/Pro/%E5%BC%80%E5%8F%91/Error</see>
        /// </summary>
        [Obsolete ("收到错误的API响应将抛出异常,不用在此作出判断", true)]
        [JsonProperty ("retcode")]
        public int retcode { get; private set; }

        /// <summary>
        /// 原封不动
        /// <see>https://cqhttp.cc/docs/4.6/#/API</see>
        /// </summary>
        public Newtonsoft.Json.Linq.JToken raw_data { get; private set; }
        /// <summary>将收到的消息保存到this中</summary>
        public virtual void Parse (string result) {
            Base.ApiResult i = PreCheck (result);
            return;
        }
        ///
        protected ApiResult PreCheck (string result) {
            JToken parsed = JToken.Parse (result);
            int retcode = parsed["retcode"].ToObject<int> ();
            switch (retcode) {
                case 0:
                case 1:
                    this.raw_data = parsed["data"];
                    break;

                default:
                    ErrorHandler.Handle (retcode);
                    break;
            }
            return this;
        }
    }
}