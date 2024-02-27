using System;
using System.Collections.Generic;

namespace CUAHANGBANDAGRANITE.Models;

public partial class NganhangKhachhang
{
    public int IdnhKh { get; set; }

    public int? Nganhangidnganhang { get; set; }

    public int? Khachhangidkhachhang { get; set; }

    public string? Sotaikhoan { get; set; }

    public bool? Active { get; set; }

    public virtual Khachhang? KhachhangidkhachhangNavigation { get; set; }

    public virtual Nganhang? NganhangidnganhangNavigation { get; set; }
}
