namespace cqhttp.Cyan.Messages.CQElements {
    /// <summary>
    /// 位置信息
    /// </summary>
    public class ElementLocation : Base.Element {
        /// <summary>
        /// 构造位置信息
        /// </summary>
        /// <param name="latitude">纬度</param>
        /// <param name="longitude">经度</param>
        /// <param name="title">地址名字</param>
        /// <param name="content">详细地址</param>
        /// <param name="style">未知参数</param>
        public ElementLocation (
                float latitude,
                float longitude,
                string title,
                string content,
                string style = "1"
            ):
            base ("location",
                ("lat", latitude.ToString ()),
                ("lon", longitude.ToString ()),
                ("title", title),
                ("content", content),
                ("style", style)
            ) { }
    }
}