using System;
using System.Collections.Generic;

namespace CUAHANGBANDAGRANITE.Models;

public partial class Xe
{
    public int Idxe { get; set; }

    public int? Donvivanchuyeniddvvc { get; set; }

    public string? Maxe { get; set; }

    public string? Tenxe { get; set; }

    public double? Trongtai { get; set; }

    public double? Sokhoi { get; set; }

    public string? Ghichu { get; set; }

    public bool? Active { get; set; }

    public virtual Donvivanchuyen? DonvivanchuyeniddvvcNavigation { get; set; }

    public virtual ICollection<Thongtinxe> Thongtinxes { get; set; } = new List<Thongtinxe>();
}
