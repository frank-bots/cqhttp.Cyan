namespace cqhttp.Cyan.Utils {
    /// <summary>
    /// 一些有用的构造
    /// </summary>
    public class Get {
        /// <summary>
        /// 群头像(url的形式)
        /// </summary>
        public static string GroupAvatarUrl (long group_id) =>
            $"https://p.qlogo.cn/gh/{group_id}/{group_id}/0";

        /// <summary>
        /// QQ用户头像(url的形式)
        /// </summary>
        public static string UserAvatarUrl (ulong user_id) =>
            $"https://q1.qlogo.cn/g?b=qq&nk={user_id}&s=0";

    }
}