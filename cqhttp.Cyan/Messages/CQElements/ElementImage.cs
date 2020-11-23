using System;
using System.Text.RegularExpressions;
using cqhttp.Cyan.Messages.CQElements.Base;

namespace cqhttp.Cyan.Messages.CQElements {
    /// <summary>注意酷Q Air是发送不了图片的</summary>
    public class ElementImage : ElementFile {
        /// <summary>
        /// 通过含图片的byte array构造图片Element
        /// </summary>
        /// <param name="imageBytes"></param>
        /// <param name="useCache"></param>
        /// <returns></returns>
        public ElementImage (byte[] imageBytes, bool useCache = true) :
            base ("image", imageBytes, useCache) { }
        /// <summary>
        /// 通过网络资源构造图片Element
        /// </summary>
        /// <param name="url">资源位置</param>
        /// <param name="useCache">是否缓存于酷Q客户端</param>
        /// <returns></returns>
        public ElementImage (string url, bool useCache = true) :
            base ("image", url, useCache) { }

        /// <summary>
        /// 不同的图片URL可能指向相同的图片
        /// </summary>
        public override bool Equals (object other) {
            if (!(other is ElementImage that)) return false;
            string hash1 = this.TryGetHash (), hash2 = that.TryGetHash ();
            if (hash1 != null && hash2 != null) return hash1 == hash2;
            return ElementFile.Equals (this, other);
        }

        static Regex uri_pat = new Regex (@"\/offpic_new\/([0-9]*)\/\/\1-[0-9]*-([0-9A-F]*)\/");
        string TryGetHash () {
            var uri = new Uri (this.file_url);
            if (uri.Host != "c2cpicdw.qpic.cn")
                return null;
            var match = uri_pat.Match (uri.AbsolutePath);
            if (!match.Success) return null;
            return match.Groups[2].Value;
        }
        ///
        public override int GetHashCode() => base.GetHashCode();
    }
}