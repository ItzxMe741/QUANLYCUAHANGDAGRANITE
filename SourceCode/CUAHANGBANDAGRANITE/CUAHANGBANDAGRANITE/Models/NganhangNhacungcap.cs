using System;
using System.Collections.Generic;

namespace CUAHANGBANDAGRANITE.Models;

public partial class NganhangNhacungcap
{
    public int IdnhNcc { get; set; }

    public int? Nganhangidnganhang { get; set; }

    public int? Nhacungcapidncc { get; set; }

    public string? Sotaikhoan { get; set; }

    public bool? Active { get; set; }

    public virtual Nganhang? NganhangidnganhangNavigation { get; set; }

    public virtual Nhacungcap? NhacungcapidnccNavigation { get; set; }
}
