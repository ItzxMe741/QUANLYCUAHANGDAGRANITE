using System;
using System.Collections.Generic;

namespace CUAHANGBANDAGRANITE.Models;

public partial class Xuatxu
{
    public int Idxuatxu { get; set; }

    public string? Maxuatxu { get; set; }

    public string? Xuatxu1 { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<Dagranite> Dagranites { get; set; } = new List<Dagranite>();
}
