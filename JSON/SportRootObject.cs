using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1XBetParser.JSON
{
    public class SportRoot : IRootObject<SportValue>
    {
        public string Error { get; set; }
        public int ErrorCode { get; set; }
        public string Guid { get; set; }
        public int Id { get; set; }
        public bool Success { get; set; }
        public SportValue[] Value { get; set; }
    }

    public class SportValue
    {
        public int C { get; set; }
        public int CC { get; set; }
        public int CID { get; set; }
        public string E { get; set; }
        public int I { get; set; }
        public int IT { get; set; }
        public string N { get; set; }
        public int V { get; set; }
        public int Z { get; set; }
        public int MS { get; set; }
    }
}
