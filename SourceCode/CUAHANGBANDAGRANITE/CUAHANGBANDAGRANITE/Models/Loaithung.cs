using System;
using System.Collections.Generic;

namespace CUAHANGBANDAGRANITE.Models;

public partial class Loaithung
{
    public int Idloaithung { get; set; }

    public int? Dagraniteidda { get; set; }

    public int? Quycachidquycach { get; set; }

    public string? Maloaithung { get; set; }

    public string? Tenloaithung { get; set; }

    public double? Dongiaban { get; set; }

    public double? Sothungcon { get; set; }

    public string? Ghichu { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<Chitietnhap> Chitietnhaps { get; set; } = new List<Chitietnhap>();

    public virtual ICollection<Chitietxuat> Chitietxuats { get; set; } = new List<Chitietxuat>();

    public virtual Dagranite? DagraniteiddaNavigation { get; set; }

    public virtual Quycachdonggoi? QuycachidquycachNavigation { get; set; }
}
