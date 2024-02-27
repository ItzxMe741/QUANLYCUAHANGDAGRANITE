using System;
using System.Collections.Generic;

namespace CUAHANGBANDAGRANITE.Models;

public partial class Hinhthucthanhtoan
{
    public int Idhttt { get; set; }

    public int? Nganhangidnganhang { get; set; }

    public string? Mahttt { get; set; }

    public string? Tenhttt { get; set; }

    public string? Ghichu { get; set; }

    public bool? Active { get; set; }

    public virtual Nganhang? NganhangidnganhangNavigation { get; set; }

    public virtual ICollection<Phieuthanhtoan> Phieuthanhtoans { get; set; } = new List<Phieuthanhtoan>();

    public virtual ICollection<Phieuthutien> Phieuthutiens { get; set; } = new List<Phieuthutien>();
}
