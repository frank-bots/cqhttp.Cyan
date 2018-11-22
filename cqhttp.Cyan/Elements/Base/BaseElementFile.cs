using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace cqhttp.Cyan.Elements.Base {
    /// <summary>
    /// 包括图片，语音
    /// </summary>
    public class ElementFile : Element {
        /// <summary>
        /// stores the location of the file
        /// could be base64://, http(s):// or file://
        /// <see>https://tools.ietf.org/html/rfc8089</see>
        /// </summary>
        public string file { get; private set; }

        /// <summary>represents whether the file has downloaded</summary>
        private bool isFixed {
            get {
                return file.Substring (0, file.IndexOf (':')) == "base64"; //"[base64]://"
            }
        }
        /// <summary>以二进制形式存储的文件</summary>
        private byte[] bin_content;

        /// <returns><see cref="NullElementException"/></returns>
        public ElementFile () : base () { }
        public ElementFile (string type, params (string key, string val) [] dict):
            base (type, dict) { }
        /// <summary>
        /// 通过byte array构造文件Element
        /// </summary>
        /// <param name="type">Element种类</param>
        /// <param name="bytes"></param>
        /// <param name="useCache"></param>
        /// <returns></returns>
        public ElementFile (string type, byte[] bytes, bool useCache):
            base (type, ("file", $"base64://{Convert.ToBase64String (bytes)}")) {
                if (!useCache) data["cache"] = "0";
            }
        public ElementFile (string type, string url, bool useCache):
            base (type, ("file", url)) {
                if (!useCache) data["cache"] = "0";
            }
        
        private void GetFilePath () {
            try {
                this.file = data["file"];
            } catch (KeyNotFoundException) {
                throw new ErrorElementException ("data中没有file段***");
            }
        }
        /// <summary>
        /// 下载图片并转为base64存储，并删除data中的url项
        /// 若仍需url可从<see cref="file"/>中获取
        /// </summary>
        /// <returns>返回是否成功获取到</returns>
        private async Task<bool> Fix () {
            try {
                using (var http = new HttpClient ()) {
                    bin_content = await http.GetByteArrayAsync (file);
                    data["file"] =
                        $"base64://{Convert.ToBase64String (bin_content)}";
                }
            } catch { return false; }
            return true;
        }
    }
}