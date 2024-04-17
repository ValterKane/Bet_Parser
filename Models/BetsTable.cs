using System;
using System.Collections.Generic;

namespace _1XBetParser.Models;

public partial class BetsTable
{
    public int MatchId { get; set; }

    public int BetTypeId { get; set; }

    public decimal? BetValue { get; set; }

    public virtual TypeBetTable BetType { get; set; } = null!;

    public virtual MatchTable Match { get; set; } = null!;
}
