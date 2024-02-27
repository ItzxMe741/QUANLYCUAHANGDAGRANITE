using System;
using System.Collections.Generic;

namespace CUAHANGBANDAGRANITE.Models;

public partial class Chitietnhap
{
    public int Idctn { get; set; }

    public int? Loaithungidloaithung { get; set; }

    public int? Phieunhapkhoidpnk { get; set; }

    public double? Sothung { get; set; }

    public double? Vat { get; set; }

    public double? Dongianhap { get; set; }

    public bool? Xacnhan { get; set; }

    public bool? Active { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual Loaithung? LoaithungidloaithungNavigation { get; set; }

    public virtual Phieunhapkho? PhieunhapkhoidpnkNavigation { get; set; }
}
