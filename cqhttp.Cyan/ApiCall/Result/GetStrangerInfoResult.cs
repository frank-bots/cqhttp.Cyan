namespace cqhttp.Cyan.ApiCall.Results {
    ///
    public class GetStrangerInfoResult : Base.ApiResult {
        ///
        public long user_id { get; private set; }
        ///
        public string nickname { get; private set; }
        ///
        public string sex { get; private set; }
        ///
        public int age { get; private set; }
        ///
        public override void Parse (Newtonsoft.Json.Linq.JToken result) {
            PreCheck (result);
            try {
                user_id = raw_data["user_id"].ToObject<long> ();
                nickname = raw_data["nickname"].ToObject<string> ();
                sex = raw_data["sex"].ToObject<string> ();
                age = raw_data["age"].ToObject<int> ();
            } catch {
                Log.Error ("调用发送消息API未返回必要的参数");
                throw new Exceptions.ErrorApicallException ();
            }
        }
    }
}