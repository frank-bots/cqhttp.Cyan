using System;
using System.Threading.Tasks;
using cqhttp.Cyan.Events.CQEvents.Base;
using cqhttp.Cyan.Events.CQResponses.Base;
using cqhttp.Cyan.WebsocketUtils;

namespace cqhttp.Cyan.Events.EventListener {

    /// <summary></summary>
    public class ReverseWSListener : _WebsocketProcessor {
        string accessToken;
        WebsocketDaemon.WebsocketServerInstance server;
        /// <summary></summary>
        public ReverseWSListener (
            int bind_port,
            string path,
            string accessToken = ""
        ) : base ("") {
            this.accessToken = accessToken;
            server = new WebsocketDaemon.WebsocketServerInstance (bind_port, path);
        }
        /// <summary></summary>
        public override void StartListen (
            Func<CQEvent, CQResponse> callback
        ) {
            if (server.socket.ConnectionInfo.Headers["Authorization"].Contains (accessToken))
                server.socket.OnMessage = (m) => {
                    Process (m);
                };
            else throw new Exceptions.ErrorEventException ("access token认证失败");
        }
    }
}