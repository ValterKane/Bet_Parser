using System;
using System.Collections.Generic;

namespace _1XBetParser.Models;

public partial class ChampTable
{
    public int ChampId { get; set; }

    public string? ChampRuName { get; set; }

    public string? ChampEuName { get; set; }

    public string? LocationName { get; set; }

    public int? SportId { get; set; }

    public virtual ICollection<MatchTable> MatchTables { get; set; } = new List<MatchTable>();

    public virtual SportTable? Sport { get; set; }
}
