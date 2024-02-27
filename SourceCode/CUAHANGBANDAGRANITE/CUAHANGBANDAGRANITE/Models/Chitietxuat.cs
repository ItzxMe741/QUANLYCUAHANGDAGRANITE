using System;
using System.Collections.Generic;

namespace CUAHANGBANDAGRANITE.Models;

public partial class Chitietxuat
{
    public int Idctx { get; set; }

    public int? Loaithungidloaithung { get; set; }

    public int? Phieuxuatkhoidpxk { get; set; }

    public double? Dientichkhdat { get; set; }

    public double? Vat { get; set; }

    public double? Sothunggiaokh { get; set; }

    public double? Dongiaxuat { get; set; }

    public bool? Xacnhan { get; set; }

    public bool? Active { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual Loaithung? LoaithungidloaithungNavigation { get; set; }

    public virtual Phieuxuatkho? PhieuxuatkhoidpxkNavigation { get; set; }
}
