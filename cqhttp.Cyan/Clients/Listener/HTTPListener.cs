using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using cqhttp.Cyan.Events.CQResponses.Base;

namespace cqhttp.Cyan.Clients.Listeners {
    class HTTPListener : Listener {
        byte[] secret;
        HttpListener listener;
        public HTTPListener (int port, string secret = "") {
            this.secret = (secret == "" ? null :
                Encoding.UTF8.GetBytes (secret));
            if (port != -1) {
                listener = new HttpListener ();
                listener.Prefixes.Add ($"http://+:{port}/");
                listener.Start ();
                Task.Run (async () => {
                    while (true) {
                        var context = await listener.GetContextAsync ();
                        var _ = ProcessContext (context);
                    }
                });
            }
        }
        private async Task ProcessContext (HttpListenerContext context) {
            try {
                var request = context.Request;
                if (!request.ContentType.StartsWith (
                        "application/json",
                        StringComparison.Ordinal
                    ))
                    return;

                string message = GetContent (secret, request);
                if (string.IsNullOrEmpty (message))
                    return;
                CQResponse response = await GetResponse (message);
                context.Response.ContentType = "application/json";
                if (response != null) {
                    byte[] output = Encoding.UTF8.GetBytes (response.ToString ());
                    context.Response.ContentLength64 = output.Length;
                    await context.Response.OutputStream.WriteAsync (
                        output, 0, output.Length
                    );
                } else {
                    context.Response.StatusCode = 204;
                }
            } catch (Exception e) {
                Log.Error ($"网络出现未知错误{e}\n{e.Message}");
            }
        }
        private static string GetContent (byte[] secret, HttpListenerRequest request) {
            var ms = new System.IO.MemoryStream ();
            request.InputStream.CopyTo (ms);
            byte[] bytes = ms.ToArray ();
            (request.InputStream as IDisposable).Dispose ();

            var signature = request.Headers.Get ("X-Signature");

            if (secret != null)
                using (var hmac = new System.Security.Cryptography.HMACSHA1 (secret)) {
                    hmac.Initialize ();
                    string result = BitConverter.ToString (
                        hmac.ComputeHash (bytes, 0, bytes.Length)
                    ).Replace ("-", "");
                    if (!string.Equals (
                            signature,
                            $"sha1={result}",
                            StringComparison.OrdinalIgnoreCase
                        )) {
                        return null;
                    }
                }
            return request.ContentEncoding.GetString (bytes);
        }
    }
}