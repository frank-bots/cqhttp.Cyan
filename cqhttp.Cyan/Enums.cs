namespace cqhttp.Cyan.Enums {
    /// <summary>消息类型</summary>
    public enum MessageType {
        /// <summary>私聊</summary>
        private_,
        /// <summary>群聊</summary>
        group_,
        /// <summary>讨论组</summary>
        discuss_
    }
    /// <summary>消息上报类型</summary>
    public enum PostType {
        /// <summary>元事件</summary>
        meta_event,
        /// <summary>消息事件</summary>
        message,
        /// <summary></summary>
        notice,
        /// <summary></summary>
        request
    }
    /// <summary>
    /// 通知事件
    /// </summary>
    public enum NoticeType {
        /// <summary>群文件上传</summary>
        group_upload,
        /// <summary>群管理员变动</summary>
        group_admin,
        /// <summary>群成员减少</summary>
        group_decrease,
        /// <summary>群成员增加</summary>
        group_increase,
        /// <summary>群员被禁言</summary>
        group_ban,
        /// <summary></summary>
        group_lift_ban,
        /// <summary>好友申请</summary>
        friend_add,
        /// <summary>群消息撤回</summary>
        group_recall,
        /// <summary>戳一戳，运气王或群荣誉变更</summary>
        notify,
    }
    /// <summary>
    /// 日志 严重性
    /// </summary>
    public enum Verbosity {
        /**<summary/>*/
        OFF,
        /**<summary/>*/
        FATAL,
        /**<summary/>*/
        ERROR,
        /**<summary/>*/
        WARN,
        /**<summary/>*/
        INFO,
        /**<summary/>*/
        DEBUG,
        /**<summary/>*/
        ALL
    }

    /// <summary>
    /// 日志输出方式
    /// </summary>
    public enum LogType {
        /// <summary>
        /// 文件
        /// </summary>
        file = 1,
        /// <summary>
        /// 控制台
        /// </summary>
        console = 2,
    }
}