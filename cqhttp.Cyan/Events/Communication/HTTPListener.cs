using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace cqhttp.Cyan.Communication {
    /// <summary>
    /// HTTP监听上报消息
    /// </summary>
    /// <remarks>
    /// 只能实例化一次！！！
    /// 多次实例化会导致后来者的行为覆盖前者
    /// （onRecv为static)
    /// </remarks>
    class Listener {
        private IWebHost host;
        public static Func<string, string> onReceive;
        public Listener (Func<string, string> onRecv, params string[] prefixes) {
            onReceive = onRecv;
            host = new WebHostBuilder ().
            UseKestrel ().
            UseUrls (prefixes).
            UseStartup<RequestHandler> ().
            Build ();
            host.Start ();
        }
        public async void Stop () {
            await host.StopAsync ();
        }
    }
    class RequestHandler {
        public void ConfigureServices (IServiceCollection services) {

        }
        public void Configure (IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
            app.Run (new Microsoft.AspNetCore.Http.RequestDelegate (HttpRequestHandler));
    }

        private async Task HttpRequestHandler (HttpContext context) {
            byte[] content = new byte[2000];
            if (context.Request.ContentLength != 0)
                try {
                    context.Request.Body.Read (content, 0, 2000);
                } catch (Exception e) {
                    throw new NetworkFailureException (e.Message);
                }

            string ret = "";
            Listener.onReceive (Encoding.UTF8.GetString(content));
            await context.Response.WriteAsync (ret);
        }
    }
}