using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using cqhttp.Cyan.Events.CQEvents.Base;
using cqhttp.Cyan.Events.CQResponses.Base;

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
        public override void StartListen (Func<CQEvent, Task<CQResponse>> callback) {
            listen_callback = callback;
            lock (listen_lock) {
                listener.Start ();
                listen_task = Task.Run (async () => {
                    while (true) {
                        var context = listener.GetContext ();
                        await ProcessContext (context);
                    }
                });
            }
        }
        private async Task ProcessContext (HttpListenerContext context) {
            try {
                var request = context.Request;
                if (!request.ContentType.StartsWith ("application/json", StringComparison.Ordinal))
                    return;

                string requestContent = GetContent (secret, request);
                if (string.IsNullOrEmpty (requestContent))
                    return;
                CQResponse responseObject = null;
                try {
                    responseObject =
                        await listen_callback (CQEventHandler.HandleEvent (requestContent));
                } catch (Exception e) {
                    Logger.Error (
                        $"处理事件时发生未处理的异常{e},错误信息为{e.Message}"
                    );
                }
                context.Response.ContentType = "application/json";
                if (responseObject != null) {
                    byte[] output = Encoding.UTF8.GetBytes (responseObject.ToString ());
                    context.Response.ContentLength64 = output.Length;
                    await context.Response.OutputStream.WriteAsync (
                        output, 0, output.Length
                    );
                } else {
                    context.Response.StatusCode = 204;
                }
            } catch (Exception e) {
                Logger.Error ($"网络出现未知错误{e}\n{e.Message}");
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