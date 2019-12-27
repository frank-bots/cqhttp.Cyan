namespace cqhttp.Cyan.ApiCall.Results {
    ///
    public class GetLoginInfoResult : Base.ApiResult {
        ///
        public long user_id { get; private set; }
        ///
        public string nickname { get; private set; }
        ///
        public override void Parse (Newtonsoft.Json.Linq.JToken result) {
            PreCheck (result);
            try {
                user_id =
                    raw_data["user_id"].ToObject<long> ();
                nickname =
                    raw_data["nickname"].ToString ();
            } catch {
                Log.Error ("调用发送消息API未返回必要的参数");
                throw new Exceptions.ErrorApicallException ();
            }
        }
    }
}