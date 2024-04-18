using System;
using System.Collections.Generic;

namespace _1XBetParser.Models;

public partial class MatchTable
{
    public int MatchId { get; set; }

    public string? Opponent1 { get; set; }

    public string? Opponent2 { get; set; }

    public int? ChampId { get; set; }

    public string? MatchTime { get; set; }

    public string? Temperture { get; set; }

    public string? MatchTablecol { get; set; }

    public string? MatchType { get; set; }

    public string? MatchPlace { get; set; }

    public string? MatchSubtype { get; set; }

    public string? WetherType { get; set; }

    public string? WindOnMatch { get; set; }

    public string? AirPressureOnMatch { get; set; }

    public string? HumidityOnMatch { get; set; }

    public string? DownfallOnMatch { get; set; }

    public virtual ICollection<BetsTable> BetsTables { get; set; } = new List<BetsTable>();

    public virtual ChampTable? Champ { get; set; }
}
