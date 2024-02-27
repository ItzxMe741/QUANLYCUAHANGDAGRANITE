using System;
using System.Collections.Generic;

namespace CUAHANGBANDAGRANITE.Models;

public partial class Nganhang
{
    public int Idnganhang { get; set; }

    public string? Manganhang { get; set; }

    public string? Tennganhang { get; set; }

    public string? Ghichu { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<Hinhthucthanhtoan> Hinhthucthanhtoans { get; set; } = new List<Hinhthucthanhtoan>();

    public virtual ICollection<NganhangKhachhang> NganhangKhachhangs { get; set; } = new List<NganhangKhachhang>();

    public virtual ICollection<NganhangNhacungcap> NganhangNhacungcaps { get; set; } = new List<NganhangNhacungcap>();
}
