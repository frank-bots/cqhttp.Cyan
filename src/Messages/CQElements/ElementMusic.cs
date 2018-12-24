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

            using (var i = new HttpClient ()) {
                var res = JToken.Parse (await i.GetStringAsync (new System.Uri ("https://music.163.com/api/search/get?type=1&limit=1&s=" + keyword)));
                if (
                    res["code"].ToObject<int> () != 200 ||
                    res["result"]["songCount"].ToObject<int> () == 0
                ) {
                    return "511728615"; // 404 Not Found(Prod.by CashMoneyAP)
                }
                return res["result"]["songs"][0]["id"].ToString ();
            }
        }
    }
}