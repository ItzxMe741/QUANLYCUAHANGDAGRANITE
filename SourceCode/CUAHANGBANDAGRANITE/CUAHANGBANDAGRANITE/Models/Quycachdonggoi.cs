using System;
using System.Collections.Generic;

namespace CUAHANGBANDAGRANITE.Models;

public partial class Quycachdonggoi
{
    public int Idquycach { get; set; }

    public string? Maquycach { get; set; }

    public string? Tenquycach { get; set; }

    public double? Dientichbemat { get; set; }

    public double? Khoiluong { get; set; }

    public double? Sovien { get; set; }

    public string? Ghichu { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<Loaithung> Loaithungs { get; set; } = new List<Loaithung>();
}
