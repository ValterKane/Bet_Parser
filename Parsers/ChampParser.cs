using _1XBetParser.JSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1XBetParser.Parsers
{
    public class ChampParser : IParser<ChampValue>
    {
        private string curl_string;
        private Dictionary<string, string> _params = new()
        {
            {"sport", "1"}, {"tf", "2200000"}, {"tz", "3"}, {"country", "1"}, {"partner", "51"}, {"virtualSports", "true"}, {"gr", "44"}, {"groupChamps", "true" },
        };
        public ICurlCreator _curlCreator { get; set; }
        public CurlRequester _curlRequester { get; set; } = new CurlRequester();
        public Dictionary<string, string> InnerParams { get => _params; set => _params = value; }

        public ChampParser(ICurlCreator? urlCreator)
        {
            _curlCreator = urlCreator;
            
        }

        public async Task<IRootObject<ChampValue>> Parse()
        {
            curl_string = _curlCreator.GetGurls("https://1xstavka.ru/LineFeed/GetChampsZip", InnerParams);
            var result = await _curlRequester.GetRequest(curl_string);

            if (result != null)
            {
                return JsonConvert.DeserializeObject<ChampRoot>(result);
            }
            else
            {
                return null;
            }
        }
    }
}
