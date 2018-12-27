using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using cqhttp.Cyan.Enums;
using cqhttp.Cyan.Events;
using cqhttp.Cyan.Events.CQEvents.Base;
using cqhttp.Cyan.Events.CQEvents.CQResponses.Base;

namespace cqhttp.Cyan.Events.EventListener {
    /// <summary>
    /// HTTP监听上报消息
    /// </summary>
    public class HttpEventListener : Events.EventListener.CQEventListener {
        private HttpListener listener;

        /// <summary></summary>
        public HttpEventListener (int port, string secret = "") : base (secret) {
            listener = new HttpListener ();
            listener.Prefixes.Add ($"http://+:{port}/");
        }
        /// <summary></summary>
        public override void StartListen (Func<CQEvent, CQResponse> callback) {
            listen_callback = callback;
            lock (listen_lock) {
                listener.Start ();
                listen_task = Task.Run (() => {
                    while (true) {
                        var context = listener.GetContext ();
                        ProcessContext (context);
                    }
                });
            }
        }
        private async void ProcessContext (HttpListenerContext context) {
            try {
                await Task.Run (() => {
                    var request = context.Request;
                    using (var response = context.Response) {
                        if (!request.ContentType.StartsWith ("application/json", StringComparison.Ordinal))
                            return;

                        string requestContent = GetContent (secret, request);
                        if (string.IsNullOrEmpty (requestContent))
                            return;
                        CQResponse responseObject = null;
                        try {
                            responseObject =
                                listen_callback (CQEventHandler.HandleEvent (requestContent));
                        } catch (Exception e) {
                            Logger.Log (
                                Verbosity.ERROR,
                                $"处理事件时发生未处理的异常{e},错误信息为{e.Message}"
                            );
                        }

                        response.ContentType = "application/json";
                        if (responseObject != null) {
                            using (var outStream = response.OutputStream)
                            using (var streamWriter = new StreamWriter (outStream)) {
                                string jsonResponse = responseObject.ToString ();
                                streamWriter.Write (jsonResponse);
                            }
                        } else {
                            response.StatusCode = 204;
                        }
                    }
                });
            } catch (Exception e) {
                Logger.Log (
                    Verbosity.ERROR,
                    $"网络出现未知错误{e}"
                );
            }
        }
        private static string GetContent (byte[] secret, HttpListenerRequest request) {
            var ms = new MemoryStream ();
            request.InputStream.CopyTo (ms);
            byte[] bytes = ms.ToArray ();
            (request.InputStream as IDisposable).Dispose ();

            var signature = request.Headers.Get ("X-Signature");

            if (Verify (secret, signature, bytes, 0, bytes.Length)) {
                string requestContent;
                requestContent = request.ContentEncoding.GetString (bytes);
                return requestContent;
            }
            return null;
        }
    }
}