using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using cqhttp.Cyan.Events.CQEvents.Base;
using cqhttp.Cyan.Events.CQResponses.Base;

namespace cqhttp.Cyan.Events.EventListener {
    /// <summary></summary>
    public abstract class CQEventListener {
        /// <summary>用于验证消息真实性</summary>
        public byte[] secret { get; private set; }
        /// <summary></summary>
        protected Func<CQEvent, Task<CQResponse>> listen_callback;
        /// <summary></summary>
        protected Task listen_task;
        /// <summary></summary>
        protected object listen_lock = new object ();
        /// <summary></summary>
        public CQEventListener (string secret) {
            this.secret = (secret == "" ? null : Encoding.UTF8.GetBytes (secret));
        }
        /// <summary></summary>
        public virtual void StartListen (Func<CQEvents.Base.CQEvent, Task<CQResponses.Base.CQResponse>> callback) { }
        /// <summary></summary>
        protected static bool Verify (byte[] secret, string signature, byte[] buffer, int offset, int length) {
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