using System;
using System.Collections.Generic;

namespace _1XBetParser.Models;

public partial class MatchTable
{
    public int MatchId { get; set; }

    public string? Opponent1 { get; set; }

    public string? Opponent2 { get; set; }

    public int? ChampId { get; set; }

    public string? Match_time { get; set; }

    public virtual ICollection<BetsTable> BetsTables { get; set; } = new List<BetsTable>();

    public virtual ChampTable? Champ { get; set; }
}
