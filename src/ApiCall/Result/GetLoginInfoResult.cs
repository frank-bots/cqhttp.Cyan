namespace cqhttp.Cyan.ApiCall.Results {
    ///
    public class GetLoginInfoResult : Base.ApiResult {
        ///
        public long user_id { get; private set; }
        ///
        public string nickname { get; private set; }
        ///
        public override void Parse (string result) {
            Base.ApiResult i = PreCheck (result);
            try {
                user_id =
                    i.raw_data["user_id"].ToObject<long> ();
                nickname =
                    i.raw_data["nickname"].ToString ();
            } catch {
                Logger.Log (Enums.Verbosity.ERROR, "调用发送消息API未返回必要的参数");
                throw new Exceptions.ErrorApicallException ();
            }
        }
    }
}