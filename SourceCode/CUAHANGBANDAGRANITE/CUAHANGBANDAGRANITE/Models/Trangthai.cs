using System;
using System.Collections.Generic;

namespace CUAHANGBANDAGRANITE.Models;

public partial class Trangthai
{
    public int Idtt { get; set; }

    public string? Matt { get; set; }

    public string? Tentt { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<Phieunhapkho> Phieunhapkhos { get; set; } = new List<Phieunhapkho>();

    public virtual ICollection<Phieuxuatkho> Phieuxuatkhos { get; set; } = new List<Phieuxuatkho>();
}
