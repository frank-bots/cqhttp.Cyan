namespace cqhttp.Cyan.Messages.CQElements {
    ///
    public class ElementMusicCustom : Base.BaseElementShare {
        /// <summary>
        /// 音乐自定义分享
        /// </summary>
        /// <param name="url">分享链接，即点击分享后进入的音乐页面（如歌曲介绍页）</param>
        /// <param name="audio">音频链接（如mp3链接）</param>
        /// <param name="title">音乐的标题，建议12字以内。</param>
        /// <param name="content">音乐的简介，建议30字以内。该参数可被忽略。</param>
        /// <param name="image">音乐的封面图片链接。若参数为空或被忽略，则显示默认图片。</param>
        public ElementMusicCustom (
            string url,
            string audio,
            string title,
            string content = "",
            string image = ""
        ) : base (url, title, content, image, ("audio", audio)) { }
    }
}