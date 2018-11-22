using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace tg_duxin {
    class Listener {
        private IWebHost host;
        public static Func<string, string> onReceive;
        public Listener(Func<string, string> onRecv, params string[] prefixes) {
            onReceive = onRecv;
            host = new WebHostBuilder().
                UseKestrel().
                UseUrls(prefixes).
                UseStartup<RequestHandler>().
                Build();
            host.Start();
        }
        public async void Stop() {
            await host.StopAsync();
        }
    }
    class RequestHandler {
        public void ConfigureServices(IServiceCollection services) {

        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
            app.Run(new Microsoft.AspNetCore.Http.RequestDelegate(HttpRequestHandler));
        }

        private async Task HttpRequestHandler(HttpContext context) {
            byte[] content = new byte[2000];
            if (context.Request.ContentLength != 0)
                context.Request.Body.Read(content, 0, 2000);
            string ret = "";
                Listener.onReceive(Convert.ToString(content));
            await context.Response.WriteAsync(ret);
        }
    }
}
