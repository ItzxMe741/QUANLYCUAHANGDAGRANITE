using System;
using System.Collections.Generic;

namespace CUAHANGBANDAGRANITE.Models;

public partial class Thongtinxe
{
    public int Idttxe { get; set; }

    public int? Phieuxuatkhoidpxk { get; set; }

    public int? Xeidxe { get; set; }

    public double? Soluongxe { get; set; }

    public bool? Active { get; set; }

    public virtual Phieuxuatkho? PhieuxuatkhoidpxkNavigation { get; set; }

    public virtual Xe? XeidxeNavigation { get; set; }
}
