namespace cqhttp.Cyan.Utils {
    /// <summary>
    /// 一些常用的转换
    /// </summary>
    public class Convert {
        //善用代码折叠
        static string[] emojiDict = {
            "😲",
            "😳",
            "😍",
            "😧",
            "😏",
            "😭",
            "😇",
            "🤐",
            "💤",
            "🥺",
            "😰",
            "😡",
            "😜",
            "😬",
            "🙂",
            "🙁",
            "😦",
            "😤",
            "🤮",
            "捂嘴笑",
            "😊",
            "🤔",
            "🤨",
            "😐",
            "😴"
        };
        /// <summary>
        /// 将QQ表情转换为emoji
        /// </summary>
        /// <param name="faceId">表情编号(1-170)</param>
        public static string ToEmoji (int faceId) {
            if (faceId < emojiDict.Length)
                return emojiDict[faceId];
            else return "未知表情";
        }
        /// <summary></summary>
        public static string ToEmoji (cqhttp.Cyan.Messages.CQElements.ElementFace i) {
            return ToEmoji (int.Parse (i.data["id"]));
        }
    }
}