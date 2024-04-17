using _1XBetParser.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1XBetParser.Controllers
{
    public class Remover
    {
        private ParserbdContext _parserbdContext;
        public Remover(ParserbdContext DBContext)
        {
            _parserbdContext = DBContext;
        }

        public void Remove()
        {

            _parserbdContext.SportTables.RemoveRange(_parserbdContext.SportTables.ToList());
            _parserbdContext.ChampTables.RemoveRange(_parserbdContext.ChampTables.ToList());
            _parserbdContext.MatchTables.RemoveRange(_parserbdContext.MatchTables.ToList());
            _parserbdContext.BetsTables.RemoveRange(_parserbdContext.BetsTables.ToList());
            _parserbdContext.TypeBetTables.RemoveRange(_parserbdContext.TypeBetTables.ToList());
            _parserbdContext.SaveChanges();
        }
    }
}
