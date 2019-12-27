using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.ApiCall.Results {
    ///
    public class GetGroupListResult : Base.ApiResult {
        ///
        public List < (long, string) > groupList = new List < (long, string) > ();
        ///
        public override void Parse (JToken result) {
            PreCheck (result);
            try {
                foreach (var j in raw_data as JArray) {
                    groupList.Add (
                        (j["group_id"].ToObject<long> (), j["group_name"].ToString ())
                    );
                }
            } catch {
                Log.Error ("调用发送消息API未返回message_id");
                throw new Exceptions.ErrorApicallException ();
            }
        }
    }
}