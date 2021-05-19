namespace cqhttp.Cyan.Events.CQEvents {
    /// <summary>
    /// 属性this.user_id中存储了新好友QQ
    /// </summary>
    [DiscriminatorValue ("friend_add")]
    public class FriendAddEvent : Base.NoticeEvent { }
}