using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using cqhttp.Cyan.Events;
using cqhttp.Cyan.Events.CQEvents.Base;
using cqhttp.Cyan.Events.CQEvents.CQResponses.Base;

namespace cqhttp.Cyan.Events.EventListener {
    /// <summary>
    /// HTTP监听上报消息
    /// </summary>
    public class HttpEventListener : Events.EventListener.CQEventListener {
        private HttpListener listener;
        private object listen_lock = new object ();
        private Task listen_task;
        private Func<CQEvent, CQResponse> listen_callback;
        /// <summary></summary>
        public HttpEventListener (int port, string secret = "") : base (port, secret) {
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
            string requestContent = null;
            try {
                await Task.Run (() => {
                    var request = context.Request;
                    using (var response = context.Response) {
                        if (!request.ContentType.StartsWith ("application/json", StringComparison.Ordinal))
                            return;

                        requestContent = GetContent (secret, request);
                        if (string.IsNullOrEmpty (requestContent))
                            return;
                        
                        var responseObject =
                            listen_callback (CQEventHandler.HandleEvent (requestContent));

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
                Console.WriteLine (e.ToString ());
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

        private static bool Verify (byte[] secret, string signature, byte[] buffer, int offset, int length) {
            if (secret is null)
                return true;
            using (var hmac = new HMACSHA1 (secret)) {
                hmac.Initialize ();
                string result = BitConverter.ToString (hmac.ComputeHash (buffer, offset, length)).Replace ("-", "");
                return string.Equals (signature, $"sha1={result}", StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}