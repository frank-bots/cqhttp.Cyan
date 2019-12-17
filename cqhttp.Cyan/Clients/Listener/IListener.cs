using System;
using System.Threading.Tasks;
using cqhttp.Cyan.Events.CQEvents.Base;
using cqhttp.Cyan.Events.CQResponses.Base;

namespace cqhttp.Cyan.Clients.Listeners {
    /// <summary>
    /// 
    /// </summary>
    /// <remark>
    /// 如果注册了多个handler，那么会取最后注册的handler的响应作为事件响应
    /// </remark>
    public interface IListener {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        void RegisterHandler (Func<CQEvent, Task<CQResponse>> handler);
    }
}