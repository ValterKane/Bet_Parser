using _1XBetParser.JSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1XBetParser.Parsers
{
    public class MatchesParser : IParser<MatchValue>
    {

        private string curl_string;
        private Dictionary<string, string> _params = new()
        {
            {"sports", "1"}, {"champs", "1"}, {"count", "50"}, {"tf", "2200000"}, {"tz", "3"}, {"antisports", "188"}, {"mode", "4"}, {"country", "1"},
            {"partner", "51" }, {"getEmpty", "true"}, {"gr","44" }
        };

        public ICurlCreator _curlCreator { get; set; }
        public CurlRequester _curlRequester { get; set; } = new CurlRequester();
        public Dictionary<string, string> InnerParams { get => _params; set => _params = value; }

        public MatchesParser(ICurlCreator? urlCreator)
        {
            _curlCreator = urlCreator;

        }

        public async Task<IRootObject<MatchValue>> Parse()
        {
            curl_string = _curlCreator.GetGurls("https://1xstavka.ru/LineFeed/Get1x2_VZip", InnerParams);
            var result = await _curlRequester.GetRequest(curl_string);

            if (result != null)
            {
                return JsonConvert.DeserializeObject<MatchRoot>(result);
            }
            else
            {
                return null;
            }
        }
    }
}
