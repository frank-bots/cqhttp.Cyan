using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.ApiCall.Results {
    ///
    public class GetGroupMemberInfoResult : Base.ApiResult {
        /// <summary>获取到的群成员信息</summary>
        public Utils.GroupMemberInfo memberInfo { get; private set; }
        ///
        public override void Parse (JToken result) {
            PreCheck (result);
            memberInfo = raw_data.ToObject<Utils.GroupMemberInfo> ();
        }
    }
}