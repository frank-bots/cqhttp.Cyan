using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cqhttp.Cyan.Events.CQEvents.Base;
using cqhttp.Cyan.Events.CQResponses.Base;
using Newtonsoft.Json;

namespace cqhttp.Cyan.Clients.Listeners {
    class Listener : IListener {
        protected List<Func<CQEvent, Task<CQResponse>>> handlers =
            new List<Func<CQEvent, Task<CQResponse>>> ();
        public void RegisterHandler (Func<CQEvent, Task<CQResponse>> handler) {
            handlers.Add (handler);
        }
        protected async Task<CQResponse> GetResponse (string message) {
            CQResponse response = null;
            foreach (var handler in handlers) {
                try {
                    response = await handler (
                        JsonConvert.DeserializeObject<CQEvent> (message)
                    );
                } catch (Exception e) {
                    Log.Error ("处理事件时出现异常：");
                    Log.Error (e.ToString ());
                }
            }
            return response;
        }
    }
}