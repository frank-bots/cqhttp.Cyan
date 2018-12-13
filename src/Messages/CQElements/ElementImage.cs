using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using cqhttp.Cyan.Messages.CQElements.Base;

namespace cqhttp.Cyan.Messages.CQElements {
    /// <summary>注意酷Q Air是发送不了图片的</summary>
    public class ElementImage : ElementFile {

        /// <returns><see cref="NullElementException"/></returns>
        public ElementImage () : base () { }
        /// <summary>
        /// 通过含图片的byte array构造图片Element
        /// </summary>
        /// <param name="imageBytes"></param>
        /// <param name="useCache"></param>
        /// <returns></returns>
        public ElementImage (byte[] imageBytes, bool useCache = true):
            base ("image", imageBytes, useCache) { }
        /// <summary>
        /// 通过网络资源构造图片Element
        /// </summary>
        /// <param name="url">资源位置</param>
        /// <param name="useCache">是否缓存于酷Q客户端</param>
        /// <returns></returns>
        public ElementImage (string url, bool useCache = true):
            base ("image", url, useCache) { }
        /// <summary></summary>
        public ElementImage (params (string key, string val) [] dict):
            base ("image", dict) { }

    }
}