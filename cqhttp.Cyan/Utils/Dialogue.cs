using System;
using System.Collections.Generic;
using cqhttp.Cyan.Events.CQEvents;
using cqhttp.Cyan.Events.CQEvents.Base;
using cqhttp.Cyan.Clients;
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
        public bool acceptAll { get; private set; }
        ///
        public Dialogue content { get; private set; }
        /// <param name="dialogue">向DialoguePool中加入的对话</param>
        /// <param name="acceptAll">表示在多人对话中是否接受同一群组内所有人的消息, 仅当当前event为群组消息或讨论组消息时有效</param>
        public InvokeDialogueException (Dialogue dialogue, bool acceptAll = false) {
            content = dialogue;
            this.acceptAll = acceptAll;
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
        Dictionary<string, Func<CQApiClient, Message, string>> operations =
            new Dictionary<string, Func<CQApiClient, Message, string>> ();

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
        public Func<CQApiClient, Message, string> this [string state_name] {
            set {
                operations[state_name] = value;
            }
        }
        /// <summary>
        /// 用于更新FSM状态，请勿调用
        /// </summary>
        public bool update (CQApiClient cli, Message m) {
            if (operations.ContainsKey (state) == false) return false;
            state = operations[state] (cli, m);
            return true;
        }
    }
    /// <summary>
    /// 用于标记某一对话的作用范围
    /// 
    /// 某群/某人/某群中对某人
    /// </summary>
    public static class DialoguePool {
        static Dictionary<long, Dialogue> pool = new Dictionary<long, Dialogue> ();
        /// <summary>
        /// 是否存在某一段正在进行的对话
        /// 
        /// 请勿调用!
        /// </summary>
        /// <returns>是否阻止此消息向用户逻辑传递</returns>
        public static bool Handle (CQApiClient cli, MessageEvent e) {
            if (pool.Count == 0) return false;
            long uid = e.sender.user_id;
            long gid;
            if (e is GroupMessageEvent) {
                gid = (e as GroupMessageEvent).group_id;
            } else if (e is DiscussMessageEvent) {
                gid = (e as DiscussMessageEvent).discuss_id;
            } else { //e is PrivateMessageEvent
                gid = uid;
            }
            long hash = (uid >> 1) ^ (gid << 1);
            if (pool.ContainsKey (hash)) {
                if (pool[hash].update (cli, e.message) == false) {
                    pool.Remove (hash);
                    return false;
                }
                return pool[hash].blockEvent;
            }
            return false;
        }
        /// <summary>
        /// 添加Dialogue, 每次收到MessageEvent时都会检查
        /// </summary>
        /// <param name="uid">用户QQ号</param>
        /// <param name="gid">群号或讨论组号, 私聊的话与uid相同</param>
        /// <param name="d">需要添加的Dialogue</param>
        public static void Join (long uid, long gid, Dialogue d) {
            pool[(uid >> 1) ^ (gid << 1)] = d;
        }
    }
}