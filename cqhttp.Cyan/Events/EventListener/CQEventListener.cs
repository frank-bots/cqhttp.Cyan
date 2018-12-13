using System;
using System.Text;

namespace cqhttp.Cyan.Events.EventListener {
    /// <summary></summary>
    public abstract class CQEventListener {
        int port;
        /// <summary>用于验证消息真实性</summary>
        public byte[] secret { get; private set; }
        /// <summary></summary>
        public CQEventListener (int port, string secret) {
            this.port = port;
            this.secret = (secret == "" ? null : Encoding.UTF8.GetBytes (secret));
        }
        /// <summary></summary>
        public virtual void StartListen (Func<CQEvents.Base.CQEvent, Events.CQEvents.CQResponses.Base.CQResponse> callback) { }

    }
}