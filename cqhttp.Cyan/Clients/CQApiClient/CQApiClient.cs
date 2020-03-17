using System.Threading.Tasks;
using cqhttp.Cyan.ApiCall.Requests;
using cqhttp.Cyan.ApiCall.Results;
using cqhttp.Cyan.Utils;

namespace cqhttp.Cyan.Clients {
    /// <summary></summary>
    public partial class CQApiClient {
        /// <summary>
        /// 当前酷Q实例的各种参数
        /// </summary>
        public InstanceVersionInfo instance_version_info = null;
        /// <summary>
        /// 当前实例的QQ号
        /// </summary>
        public long self_id { get; private set; }
        /// <summary>
        /// 当前实例的QQ昵称
        /// </summary>
        public string self_nick { get; private set; }
        /// <summary>
        /// 连接到的实例是否为酷Q pro
        /// </summary>
        public bool is_pro {
            get {
                return this.instance_version_info.coolq_edition == "pro";
            }
        }
        /// <summary>
        /// 表示插件是否正常运行
        /// </summary>
        public bool alive { get; private set; }
        /// <summary>
        /// 是否已经初始化完成（检查连通性并获取self_id与self_nick）
        /// </summary>
        public bool initiated {
            get {
                return initiate_task.IsCompleted;
            }
        }
        int alive_counter = 0;
        /// <summary>
        /// 指向本实例的群组记录对象
        /// </summary>
        public GroupTable group_table = null;
        /// <summary>
        /// 消息记录
        /// </summary>
        public MessageTable message_table = null;

        ///
        protected Callers.ICaller caller;
        ///
        protected Listeners.IListener listener;

        /// <summary></summary>
        public CQApiClient (
            Callers.ICaller caller,
            Listeners.IListener listener,
            bool use_group_table = false,
            bool use_message_table = false
        ) {
            this.caller = caller;
            this.listener = listener;
            if (use_group_table) this.group_table = new GroupTable ();
            if (use_message_table) this.message_table = new MessageTable ();
        }
        /// <summary>
        /// 
        /// </summary>
        protected Task initiate_task;
        /// <summary>
        /// 检查连通性并获取self_id与self_nick
        /// </summary>
        protected async System.Threading.Tasks.Task Initiate () {
            GetLoginInfoResult loginInfo =
                await SendRequestAsync (new GetLoginInfoRequest ())
            as GetLoginInfoResult;
            GetVersionInfoResult versionInfo =
                await SendRequestAsync (new GetVersionInfoRequest ())
            as GetVersionInfoResult;
            this.self_id = loginInfo.user_id;
            this.self_nick = loginInfo.nickname;
            this.instance_version_info = versionInfo.instanceVersionInfo;
        }
    }
}