using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1XBetParser.CurlCreators
{
    internal class LeageCurl : ICurlCreator
    {
        public string GetGurls(string _base, Dictionary<string, string> _params)
        {
            string _temp = _base + "?";

            foreach (var _key in _params)
            {
                _temp += $"{_key.Key}={_key.Value}&";
            }
            return _temp.Substring(0, _temp.Length - 1);
        }
    }
}
