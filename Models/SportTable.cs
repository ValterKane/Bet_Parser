using System;
using System.Collections.Generic;

namespace _1XBetParser.Models;

public partial class SportTable
{
    public int SportId { get; set; }

    public string? SportName { get; set; }

    public virtual ICollection<ChampTable> ChampTables { get; set; } = new List<ChampTable>();
}
