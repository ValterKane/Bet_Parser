using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1XBetParser.JSON
{

    public class BetRoot:IRootObject<BetValue>
    {
        public string Error { get; set; }
        public int ErrorCode { get; set; }
        public string Guid { get; set; }
        public int Id { get; set; }
        public bool Success { get; set; }
        public BetValue[] Value { get; set; }
    }

    public class BetValue
    {
        public AE[] AE { get; set; }
        public string CE { get; set; }
        public string CHIMG { get; set; }
        public int CI { get; set; }
        public int CID { get; set; }
        public string CN { get; set; }
        public int COI { get; set; }
        public Ee[] E { get; set; }
        public int EC { get; set; }
        public bool HLU { get; set; }
        public int HS { get; set; }
        public bool HSI { get; set; }
        public bool HSRT { get; set; }
        public int I { get; set; }
        public int KI { get; set; }
        public string L { get; set; }
        public string LE { get; set; }
        public int LI { get; set; }
        public MIO1 MIO { get; set; }
        public MI1[] MIS { get; set; }
        public int[] MS { get; set; }
        public int N { get; set; }
        public string O1 { get; set; }
        public int O1C { get; set; }
        public string O1CT { get; set; }
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
        public string SGI { get; set; }
        public int SI { get; set; }
        public string SIMG { get; set; }
        public string SN { get; set; }
        public int SS { get; set; }
        public int SSI { get; set; }
        public int SST { get; set; }
        public string STI { get; set; }
        public int SmI { get; set; }
        public int T { get; set; }
        public string TN { get; set; }
        public string TNS { get; set; }
        public int B { get; set; }
        public bool GSE { get; set; }
        public bool HL { get; set; }
        public string LS { get; set; }
        public string[] RLI { get; set; }
        public SG[] SG { get; set; }
        public string O2CT { get; set; }
        public bool IDA { get; set; }
        public int MG { get; set; }
        public string TG { get; set; }
        public int TI { get; set; }
        public string DI { get; set; }
    }

    public class MIO1
    {
        public string Loc { get; set; }
        public string SSc { get; set; }
        public string TSt { get; set; }
    }

    public class AE
    {
        public int G { get; set; }
        public ME[] ME { get; set; }
    }

    public class ME
    {
        public float C { get; set; }
        public int G { get; set; }
        public int T { get; set; }
        public float P { get; set; }
        public int CE { get; set; }
    }

    public class Ee
    {
        public float C { get; set; }
        public int G { get; set; }
        public int T { get; set; }
        public int CE { get; set; }
        public float P { get; set; }
    }

    public class MI1
    {
        public int K { get; set; }
        public string V { get; set; }
    }

    public class SG
    {
        public int CI { get; set; }
        public E1[] E { get; set; }
        public int EC { get; set; }
        public int I { get; set; }
        public int MG { get; set; }
        public int N { get; set; }
        public int P { get; set; }
        public string PN { get; set; }
        public int SI { get; set; }
        public int SS { get; set; }
        public int T { get; set; }
        public string TG { get; set; }
        public int TI { get; set; }
    }

    public class E1
    {
        public float C { get; set; }
        public int G { get; set; }
        public int T { get; set; }
        public int CE { get; set; }
        public float P { get; set; }
    }

}
