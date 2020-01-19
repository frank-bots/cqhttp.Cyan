using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.ApiCall.Results.Base {
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
        public string status { get; private set; } //"ok","async","failed"

        /// <summary>
        /// status ok为0,async为1,failed参照链接
        /// <see>https://d.cqp.me/Pro/%E5%BC%80%E5%8F%91/Error</see>
        /// </summary>
        [Obsolete ("收到错误的API响应将抛出异常,不用在此作出判断", true)]
        public int retcode { get; private set; }

        /// <summary>
        /// 原封不动
        /// <see>https://cqhttp.cc/docs/4.6/#/API</see>
        /// </summary>
        public JToken raw_data { get; private set; }
        /// <summary>将收到的消息保存到this中</summary>
        public virtual void Parse (JToken result) {
            PreCheck (result);
            return;
        }
        ///
        protected void PreCheck (JToken parsed) {
            int retcode = parsed["retcode"].ToObject<int> ();
            switch (retcode) {
            case 0:
                this.raw_data = parsed["data"];
                break;
            case 1:
                throw new Exceptions.AsyncApicallException ("限速调用返回" + this.GetType ().Name);
                // 此处选择抛出异常原因有二:
                //  1: PreCheck后必定会处理parsed内容, 而异步调用并没返回任何内容, 抛异常可以跳过后面的步骤
                //  2: 让用户明确知道自己在进行异步调用
            default:
                ErrorHandler.Handle (retcode);
                break;
            }
        }
    }
}