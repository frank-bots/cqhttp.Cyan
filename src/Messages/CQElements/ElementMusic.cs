using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace cqhttp.Cyan.Messages.CQElements {
    ///
    public class ElementMusic : Base.Element {
        static Regex musicIdRe = new Regex ("<a href=\"/song?id=([0-9]+)\">");
        ///
        public ElementMusic (string type, string keyword) : base ("music", ("type", type), ("id", GetMusicID (type, keyword).Result)) { }
        private async static Task<string> GetMusicID (string type, string keyword) {
            if (type == "163")
                using (var i = new HttpClient ()) {
                    var res = JToken.Parse (await i.GetStringAsync (new System.Uri ("https://music.163.com/api/search/get?type=1&limit=1&s=" + keyword)));
                    if (
                        res["code"].ToObject<int> () != 200 ||
                        res["result"]["songCount"].ToObject<int> () == 0
                    ) {
                        return "511728615"; // 404 Not Found(Prod.by CashMoneyAP)
                    }
                    Logger.Log (Enums.Verbosity.DEBUG, $"解析了163音乐搜索{keyword}的搜索结果");
                    return res["result"]["songs"][0]["id"].ToString ();
                }
            // else if (type == "qq")

            // else if (type == "xiami")
            else if (type == "qq" || type == "xiami") {
                Logger.Log (Enums.Verbosity.ERROR, "暂未实现163与xiami搜索结果的解析");
                throw new System.NotImplementedException ("由于作者太菜,还没掌握qq和虾米音乐的搜索技巧，所以抱歉,这里抛出了一个微小的异常");
            }
            throw new Exceptions.ErrorElementException ("请将type设为163,qq,xiami之一");
        }
    }
}