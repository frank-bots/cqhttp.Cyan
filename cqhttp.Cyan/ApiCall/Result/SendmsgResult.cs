namespace cqhttp.Cyan.ApiCall.Results {
    ///
    public class SendmsgResult : Base.ApiResult {
        ///<summary>用于撤回消息的消息ID</summary>
        public int message_id { get; private set; }
        ///
        public override void Parse (Newtonsoft.Json.Linq.JToken result) {
            try {
                PreCheck (result);
                message_id =
                    raw_data["message_id"].ToObject<int> ();
            } catch (Exceptions.AsyncApicallException) {
                Log.Warn ("以限速方式调用发送消息API, 未返回message_id");
            } catch {
                Log.Error ("调用发送消息API未返回message_id");
                throw new Exceptions.ErrorApicallException ();
            }
        }
    }
}