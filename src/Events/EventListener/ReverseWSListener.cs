using System;
using System.Threading.Tasks;
using cqhttp.Cyan.ApiCall.Requests.Base;
using cqhttp.Cyan.ApiCall.Results.Base;

namespace cqhttp.Cyan.Events.EventListener {

    /// <summary></summary>
    public class ReverseWSListener : CQEventListener {
        string accessToken;
        string path;
        /// <summary></summary>
        public ReverseWSListener (
            int bind_port,
            string path,
            string accessToken = ""
        ) : base ("") {
            this.accessToken = accessToken;
            this.path = path;
            throw new NotImplementedException ();
        }
        /// <summary></summary>
        public override void StartListen (System.Func<CQEvents.Base.CQEvent, CQResponses.Base.CQResponse> callback) {
            throw new NotImplementedException ();
        }
        private void Process (string message) {
            throw new NotImplementedException ();
        }
    }
}