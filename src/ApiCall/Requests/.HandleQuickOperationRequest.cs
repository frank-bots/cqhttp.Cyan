namespace cqhttp.Cyan.ApiCall.Requests {

    /// <summary>
    /// 响应事件
    /// </summary>
    public class HandleQuickOperationRequest : Base.ApiRequest {
        string context, operation;
        ///
        public HandleQuickOperationRequest (string context, string operation):
            base ("/.handle_quick_operation") {
                this.context = context;
                this.operation = operation;
            }

        ///
        public override string content {
            get {
                return $"{{\"context\":{context},\"operation\":{operation}}}";
            }
        }
    }
}