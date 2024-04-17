using _1XBetParser.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1XBetParser.Parsers
{
    public interface IParser<T>
    {
        public ICurlCreator _curlCreator { get; set; }
        public CurlRequester _curlRequester { get; set; }

        public Dictionary<string, string> InnerParams { get; set; }

        public Task<IRootObject<T>> Parse();
    }
}
