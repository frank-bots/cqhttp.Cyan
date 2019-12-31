using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace cqhttp.Cyan.Messages.CQElements.Base {
    /// <summary>
    /// 包括图片，语音
    /// </summary>
    public class ElementFile : Element {
        /// <summary>
        /// stores the location of the file
        /// could be base64://, http(s):// or file://
        /// <see>https://tools.ietf.org/html/rfc8089</see>
        /// </summary>
        public string file_url { get; private set; }

        /// <summary>represents whether the file has downloaded</summary>
        public bool is_fixed {
            get {
                return file_url.Substring (0, file_url.IndexOf (':')) == "base64"; //"[base64]://"
            }
        }
        /// <summary>以二进制形式存储的文件</summary>
        private byte[] bin_content;
        /// <summary>
        /// 手动构造一个消息段，一般用不到
        /// </summary>
        /// <param name="type">消息段类型</param>
        /// <param name="dict">手动输入的键值对</param>
        public ElementFile (string type, params (string key, string val) [] dict):
            base (type, dict) { GetFilePath (); }
        /// <summary>
        /// 构造消息段，一般不会手动调用
        /// </summary>
        /// <param name="type">消息段类型</param>
        /// <param name="data">消息段键值对</param>
        public ElementFile (string type, Dictionary<string, string> data):
            base (type, data) { GetFilePath (); }
        /// <summary>
        /// 通过byte array构造文件消息段
        /// </summary>
        /// <param name="type">消息段种类</param>
        /// <param name="bytes"></param>
        /// <param name="use_cache"></param>
        /// <returns></returns>
        public ElementFile (string type, byte[] bytes, bool use_cache):
            base (type, ("file", $"base64://{Convert.ToBase64String (bytes)}")) {
                if (!use_cache) data["cache"] = "0";
            }
        /// <summary>
        /// 通过url构建文件消息段
        /// </summary>
        /// <param name="type">消息段种类</param>
        /// <param name="url"></param>
        /// <param name="use_cache">是否缓存于酷Q端</param>
        /// <returns></returns>
        public ElementFile (string type, string url, bool use_cache):
            base (type, ("file", url)) {
                file_url = url;
                if (!use_cache) data["cache"] = "0";
            }

        private void GetFilePath () {
            try {
                this.file_url = data["file"];
            } catch (KeyNotFoundException) {
                throw new Exceptions.ErrorElementException ("data中没有file段***");
            }
        }
        /// <summary>
        /// 下载图片并转为base64存储，并删除data中的url项
        /// 网络环境恶劣的情况下最多获取<see cref="Config.network_max_failure"/>次
        /// 若仍需url可从<see cref="ElementFile.file_url"/>中获取
        /// </summary>
        /// <returns>返回是否成功获取到</returns>
        public async Task<bool> Fix () {
            for (int i = 0; i < Config.network_max_failure; i++) {
                try {
                    using (var http = new HttpClient ()) {
                        HttpResponseMessage response = await http.GetAsync (file_url);
                        response.EnsureSuccessStatusCode ();
                        bin_content = await response.Content.ReadAsByteArrayAsync ();
                        data["file"] =
                            $"base64://{Convert.ToBase64String (bin_content)}";
                    }
                    break;
                } catch { }
            }
            if (data.ContainsKey ("file") == false || data["file"].Length == 0)
                return false;
            // checkFormat(bin_content)
            return true;
        }

    }
}