using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1XBetParser
{
    public interface ICurlCreator
    {
        public string GetGurls(string _base, Dictionary<string, string> _params);
    }
}
