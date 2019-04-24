using System;
using System.Threading.Tasks;
using cqhttp.Cyan.ApiCall.Requests.Base;
using cqhttp.Cyan.ApiCall.Results.Base;

namespace cqhttp.Cyan.Events.EventListener {
    /// <summary></summary>
    public class ReverseWSListener : CQEventListener {
        private int listen_port;
        /// <summary></summary>
        public ReverseWSListener (int bind_port) : base ("") {
            throw new NotImplementedException ();
        }
        /// <summary></summary>
        public override void StartListen (System.Func<CQEvents.Base.CQEvent, CQResponses.Base.CQResponse> callback) {
            throw new NotImplementedException ();
        }
    }
}