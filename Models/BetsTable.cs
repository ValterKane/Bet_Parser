﻿using System;
using System.Collections.Generic;

namespace _1XBetParser.Models;

public partial class BetsTable
{
    public int MatchId { get; set; }

    public int BetTypeId { get; set; }

    public double? BetValue { get; set; }

    public string? BetName { get; set; }

    public int BetId { get; set; }

    public virtual TypeBetTable BetType { get; set; } = null!;

    public virtual MatchTable Match { get; set; } = null!;
}
