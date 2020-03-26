namespace cqhttp.Cyan.Clients {
    /// <summary>
    /// 反向websocket连接方式
    /// </summary>
    public class CQReverseWSClient : CQApiClient {
        /// <summary>
        /// 当cqhttp中use_ws_reverse配置项为true时使用
        /// </summary>
        /// <param name="bind_port">端口</param>
        /// <param name="api_path">即ws_reverse_api_url</param>
        /// <param name="event_path">即ws_reverse_event_url</param>
        /// <param name="access_token"></param>
        /// <param name="use_group_table"></param>
        /// <param name="use_message_table"></param>
        /// <returns></returns>
        public CQReverseWSClient (
            int bind_port,
            string api_path,
            string event_path,
            string access_token = "",
            bool use_group_table = false,
            bool use_message_table = false
        ) : base (
            new Callers.ReverseWSCaller (bind_port, api_path, access_token),
            new Listeners.ReverseWSListener (bind_port, event_path, access_token),
            use_group_table, use_message_table) {

            (this.listener as Listeners.ReverseWSListener).caller = this.caller;
            this.listener.RegisterHandler (HandleEvent);

            initiate_task = System.Threading.Tasks.Task.Run(Initiate);
        }
    }
}