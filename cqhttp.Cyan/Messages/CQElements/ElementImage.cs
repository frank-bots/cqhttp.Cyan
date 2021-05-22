using System;
using System.Collections.Generic;
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
        public ElementImage (string url, bool useCache = true) :
            base ("image", url, useCache) { }

        /// <summary>
        /// 通过属性字典构造图片Element
        /// </summary>
        /// <param name="data">Element属性</param>
        public ElementImage (Dictionary<string, string> data) :
            base ("image", data) { }

        /// <summary>
        /// 不同的图片URL可能指向相同的图片
        /// </summary>
        public override bool Equals (object other) {
            if (!(other is ElementImage that)) return false;
            string hash1 = this.TryGetHash (), hash2 = that.TryGetHash ();
            if (hash1 != null && hash2 != null) return hash1 == hash2;
            return ElementFile.Equals (this, other);
        }

        static Regex uri_pat = new Regex (@"([0-9A-F]{32})\/");
        string TryGetHash () {
            var uri = new Uri (this.file_url);
            if (!uri.Host.EndsWith ("qpic.cn"))
                return null;
            var match = uri_pat.Match (uri.AbsolutePath);
            if (!match.Success) return null;
            return match.Groups[1].Value;
        }
        ///
        public override int GetHashCode () => base.GetHashCode ();
    }

    /// <summary>闪照</summary>
    public class ElementFlashImage : ElementImage {
        /// <summary>
        /// 通过含图片的byte array构造图片Element
        /// </summary>
        /// <param name="imageBytes"></param>
        /// <param name="useCache"></param>
        /// <returns></returns>
        public ElementFlashImage (byte[] imageBytes, bool useCache = true) :
            base (imageBytes, useCache) => data["type"] = "flash";
        /// <summary>
        /// 通过网络资源构造图片Element
        /// </summary>
        /// <param name="url">资源位置</param>
        /// <param name="useCache">是否缓存于酷Q客户端</param>
        public ElementFlashImage (string url, bool useCache = true) :
            base (url, useCache) => data["type"] = "flash";

        /// <summary>
        /// 通过属性字典构造图片Element
        /// </summary>
        /// <param name="data">Element属性</param>
        public ElementFlashImage (Dictionary<string, string> data) :
            base (data) => data["type"] = "flash";
    }
}