using cqhttp.Cyan.Events.CQEvents;
using cqhttp.Cyan.Events.CQEvents.Base;
using cqhttp.Cyan.Utils;

namespace cqhttp.Cyan.Clients {
    /// <summary></summary>
    public partial class CQApiClient {
        private static object dialoguePoolLock = new object ();
        private static void ComposeDialogue (
            ref InvokeDialogueException d,
            ref CQEvent e
        ) {
            lock (dialoguePoolLock) {
                Log.Debug ("got a dialogue");
                long uid = (e as MessageEvent).sender.user_id;
                long bid =
                    (e is GroupMessageEvent) ? (e as GroupMessageEvent).group_id :
                    (e is DiscussMessageEvent) ? (e as DiscussMessageEvent).discuss_id :
                    uid;
                if (d.acceptAll && bid != uid)
                    uid = bid;
                DialoguePool.Join (uid, bid, d.content);
            }
            return;
        }
    }
}