using System;
using System.Collections.Generic;

namespace _1XBetParser.Models;

public partial class TypeBetTable
{
    public int BetTypeId { get; set; }

    public string? BetTypeName { get; set; }

    public virtual ICollection<BetsTable> BetsTables { get; set; } = new List<BetsTable>();
}
