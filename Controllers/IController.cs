using _1XBetParser.JSON;
using _1XBetParser.Models;
using _1XBetParser.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1XBetParser.Controllers
{
    public interface IController<T>
    {
        public IParser<T> _parser { get; set; }
        public ParserbdContext DBContext { get; set; }
        public IRootObject<T>? RootObject { get; set; }

        public bool IsContains(int id);
        public Task<bool> LoadData();
    }
}
