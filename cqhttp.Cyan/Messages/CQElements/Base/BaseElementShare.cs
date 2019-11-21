namespace cqhttp.Cyan.Messages.CQElements.Base {
    /// <summary>分享链接</summary>
    /// <remark>
    /// 本消息段只能单独发送,
    /// 请通过<see cref="CommonMessages.MessageShare"/>发送
    /// </remark>
    public class BaseElementShare : Element {
        /// <value>分享链接</value>
        public string url { protected set; get; }
        /// <value>分享的标题，建议12字以内。</value>
        public string title { protected set; get; }
        /// <value>分享的简介，建议30字以内。可为空。</value>
        public string content { protected set; get; }
        /// <value>分享的图片链接</value>
        /// <remark>若参数为空或被忽略，则显示默认图片。</remark>
        public string image { protected set; get; }
        ///
        public BaseElementShare (
            string url, string title, string content = "", string image = "",
            params (string k, string v) [] p
        ) : base (
            "share",
            ("url", url),
            ("title", title),
            ("content", content),
            ("image", image)
        ) {
            this.isSingle = true;
            this.url = url;
            this.title = title;
            this.content = content;
            this.image = image;
            foreach (var i in p)
                data.Add (i.k, i.v);

            if (content == "")
                this.data.Remove ("content");
            if (image == "")
                this.data.Remove ("image");
        }
    }
}