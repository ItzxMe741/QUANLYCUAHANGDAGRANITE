using System;
using System.Collections.Generic;

namespace CUAHANGBANDAGRANITE.Models;

public partial class Taikhoan
{
    public int Idtaikhoan { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Matkhau { get; set; }

    public string? Vaitro { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<Donvivanchuyen> Donvivanchuyens { get; set; } = new List<Donvivanchuyen>();

    public virtual ICollection<Khachhang> Khachhangs { get; set; } = new List<Khachhang>();

    public virtual ICollection<Nhacungcap> Nhacungcaps { get; set; } = new List<Nhacungcap>();

    public virtual ICollection<Nhanvien> Nhanviens { get; set; } = new List<Nhanvien>();
}
