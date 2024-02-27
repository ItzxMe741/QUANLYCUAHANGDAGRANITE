using System;
using System.Collections.Generic;

namespace CUAHANGBANDAGRANITE.Models;

public partial class Loaidum
{
    public int Idloai { get; set; }

    public string? Maloai { get; set; }

    public string? Tenloai { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<Dagranite> Dagranites { get; set; } = new List<Dagranite>();
}
