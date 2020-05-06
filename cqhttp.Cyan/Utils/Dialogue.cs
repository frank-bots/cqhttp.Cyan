using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cqhttp.Cyan.Clients;
using cqhttp.Cyan.Enums;
using cqhttp.Cyan.Events.CQEvents.Base;
using cqhttp.Cyan.Messages;

namespace cqhttp.Cyan.Utils {
    /// <summary>
    /// 一个非常特殊的Exception
    /// 
    /// 在执行OnEvent函数时, 若此SDKcatch到了这个Exception, 
    /// 则将此Exception内的Dialogue置入DialoguePool。
    /// 这是使用Dialogue的较为方便的方式之一。
    /// 
    /// 当然, 直接调用<see cref="Utils.DialoguePool.Join"/>置入也是可以的
    /// </summary>
    public class InvokeDialogueException : Exception {
        ///
        public Dialogue content { get; private set; }
        /// <param name="dialogue">向DialoguePool中加入的对话</param>
        public InvokeDialogueException (Dialogue dialogue) {
            content = dialogue;
        }
    }
    /// <summary>
    /// 表示一段对话
    /// 
    /// 其本质上是一个FSM, 其状态由一字符串表示
    /// 
    /// FSM的初始状态为BEGIN, 可以自由设置中间状态
    /// 
    /// 当FSM进入某个没有定义操作的状态时, FSM停止并回收
    /// </summary>
    public class Dialogue {
        string state = "BEGIN";
        /// <value>
        /// 是否在处理消息时进行阻断, 不交由其它逻辑进行处理
        /// 
        /// 也就是说, 若此值为真, 则OnEvent与OnEventAsync不会收到被这一FSM处理过的上报消息事件
        /// </value>
        public bool blockEvent { get; private set; }
        Dictionary<string, Func<CQApiClient, Message, Task<string>>> operations =
            new Dictionary<string, Func<CQApiClient, Message, Task<string>>> ();

        /// <summary>
        /// 表示一段对话
        /// </summary>
        /// <param name="blockEvent">是否在处理消息时进行阻断, 不交由其它逻辑进行处理</param>
        public Dialogue (bool blockEvent = true) {
            this.blockEvent = blockEvent;
        }
        /// <summary>
        /// 用于设置某一状态下FSM的行为。
        /// </summary>
        /// <value></value>
        public Func<CQApiClient, Message, Task<string>> this[string state_name] {
            set {
                operations[state_name] = value;
            }
        }
        ///
        internal async Task<bool> Update (CQApiClient cli, Message m) {
            if (operations.ContainsKey (state) == false) return false;
            state = await operations[state] (cli, m);
            return true;
        }
    }
    /// <summary>
    /// 用于标记某一对话的作用范围
    /// </summary>
    public static class DialoguePool {
        static Dictionary<(MessageType, long), Dialogue> pool =
            new Dictionary<(MessageType, long), Dialogue> ();
        /// <returns>是否阻止此消息向用户逻辑传递</returns>
        internal async static Task<bool> Handle (CQApiClient cli, MessageEvent e) {
            if (pool.Count == 0) return false;
            var endpoint = e.GetEndpoint ();
            if (pool.ContainsKey (endpoint)) {
                var block = pool[endpoint].blockEvent;
                if (await pool[endpoint].Update (cli, e.message) == false) {
                    pool.Remove (endpoint);
                    return false;
                }
                return block;
            }
            return false;
        }
        /// <summary>
        /// 添加Dialogue, 每次收到MessageEvent时都会检查
        /// </summary>
        /// <param name="endpoint">消息来源</param>
        /// <param name="d">需要添加的Dialogue</param>
        public static void Join ((MessageType, long) endpoint, Dialogue d) {
            pool[endpoint] = d;
        }
    }
}