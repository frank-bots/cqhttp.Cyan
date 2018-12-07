namespace cqhttp.Cyan.Enums {
    public enum MessageType {
        private_,
        group_,
        discuss_
    }
    public enum PostType {
        meta_event,
        message,
        notice,
        request
    }
    /// <summary>
    /// 事件
    /// </summary>
    public enum NoticeType {
        group_upload,
        group_admin,
        group_decrease,
        group_increase,
        friend_add
    }
}