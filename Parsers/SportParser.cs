using _1XBetParser.JSON;
using Newtonsoft.Json;

namespace _1XBetParser.Parsers
{
    public class SportParser:IParser<SportValue>
    {
        private string curl_string;
        private Dictionary<string, string> _params = new()
        {
            {"gr", "44"}, {"country", "1"}, {"antisports", "188"}, {"partner", "51"}, {"virtualSports", "true"}, {"groupChamps", "true"},
        };
        public ICurlCreator _curlCreator { get; set; }
        public CurlRequester _curlRequester { get; set; } = new CurlRequester();
        public Dictionary<string, string> InnerParams { get => _params; set => _params = value; }

        public SportParser(ICurlCreator? urlCreator)
        {
            _curlCreator = urlCreator;
            
        }

        public async Task<IRootObject<SportValue>> Parse()
        {
            curl_string = _curlCreator.GetGurls("https://1xstavka.ru/LiveFeed/GetSportsShortZip", InnerParams);
            var result = await _curlRequester.GetRequest(curl_string);

            if (result != null)
            {
                return JsonConvert.DeserializeObject<SportRoot>(result);
            }
            else
            {
                return null;
            }
        }
    }
}
