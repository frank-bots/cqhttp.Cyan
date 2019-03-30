using System;
using System.Collections.Generic;
using cqhttp.Cyan.Instance;
using cqhttp.Cyan.Messages;

namespace cqhttp.Cyan.Utils {
    /// <summary>
    /// 其本质上是一个FSM, 其状态由一字符串表示
    /// 
    /// FSM的初始状态为BEGIN, 可以自由设置中间状态
    /// 
    /// 当FSM进入某个没有定义操作的状态时, FSM停止并回收
    /// </summary>
    public class Dialogue {
        string state = "BEGIN";
        Dictionary<string, Func<CQApiClient, Message, string>> operations =
            new Dictionary<string, Func<CQApiClient, Message, string>> ();

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
}