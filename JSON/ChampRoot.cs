using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1XBetParser.JSON
{
    public class ChampRoot : IRootObject<ChampValue>
    {
        public string Error { get; set; }
        public int ErrorCode { get; set; }
        public string Guid { get; set; }
        public int Id { get; set; }
        public bool Success { get; set; }
        public ChampValue[] Value { get; set; }
    }

    public class ChampValue
    {
        public string CHIMG { get; set; }
        public int CI { get; set; }
        public int GC { get; set; }
        public string L { get; set; }
        public string LE { get; set; }
        public int LI { get; set; }
        public string SE { get; set; }
        public int SI { get; set; }
        public string SN { get; set; }
        public int T { get; set; }
        public string CHIMGALT { get; set; }
        public int CSC { get; set; }
        public SC[] SC { get; set; }
        public string COIMG { get; set; }
    }

    public class SC
    {
        public string CHIMG { get; set; }
        public int CI { get; set; }
        public string COIMG { get; set; }
        public int GC { get; set; }
        public string L { get; set; }
        public string LE { get; set; }
        public int LI { get; set; }
        public string SE { get; set; }
        public int SI { get; set; }
        public string SN { get; set; }
        public int T { get; set; }
    }
}
