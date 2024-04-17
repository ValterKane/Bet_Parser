using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1XBetParser.JSON
{

    public class MatchRoot:IRootObject<MatchValue>
    {
        public string Error { get; set; }
        public int ErrorCode { get; set; }
        public string Guid { get; set; }
        public int Id { get; set; }
        public bool Success { get; set; }
        public MatchValue[] Value { get; set; }
    }

    public class MatchValue
    {
        public object[] AE { get; set; }
        public string CE { get; set; }
        public string CHIMG { get; set; }
        public int CI { get; set; }
        public int CID { get; set; }
        public string CN { get; set; }
        public int COI { get; set; }
        public E[] E { get; set; }
        public int EC { get; set; }
        public int HS { get; set; }
        public int I { get; set; }
        public int KI { get; set; }
        public string L { get; set; }
        public string LE { get; set; }
        public int LI { get; set; }
        public MIO MIO { get; set; }
        public MI[] MIS { get; set; }
        public int[] MS { get; set; }
        public int N { get; set; }
        public string O1 { get; set; }
        public int O1C { get; set; }
        public string O1E { get; set; }
        public int O1I { get; set; }
        public string[] O1IMG { get; set; }
        public int[] O1IS { get; set; }
        public string O2 { get; set; }
        public int O2C { get; set; }
        public string O2E { get; set; }
        public int O2I { get; set; }
        public string[] O2IMG { get; set; }
        public int[] O2IS { get; set; }
        public string PN { get; set; }
        public int S { get; set; }
        public string SE { get; set; }
        public int SGC { get; set; }
        public int SI { get; set; }
        public string SN { get; set; }
        public int SSI { get; set; }
        public string SSN { get; set; }
        public string SSNE { get; set; }
        public int T { get; set; }
        public string TN { get; set; }
        public string TNS { get; set; }
        public int B { get; set; }
    }

    public class MIO
    {
        public string TSt { get; set; }
    }

    public class E
    {
        public float C { get; set; }
        public int G { get; set; }
        public int T { get; set; }
    }

    public class MI
    {
        public int K { get; set; }
        public string V { get; set; }
    }

}
