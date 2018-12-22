namespace cqhttp.Cyan.ApiCall.Result {
    ///
    public class SendmsgResult : Base.ApiResult {
        ///<summary>用于撤回消息的消息ID</summary>
        public int message_id { get; private set; }
        ///
        public override void Parse (string result) {
            Base.ApiResult i = base.PreCheck (result);
            try {
                message_id =
                    i.raw_data["message_id"].ToObject<int> ();
            } catch {
                Logger.Log (Enums.Verbosity.ERROR, "调用发送消息API未返回message_id");
                throw new Exceptions.ErrorApicallException ();
            }
        }
    }
}