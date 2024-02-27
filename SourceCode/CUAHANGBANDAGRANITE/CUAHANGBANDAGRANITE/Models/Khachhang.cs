using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CUAHANGBANDAGRANITE.Models;

public partial class Khachhang
{
    public int Idkhachhang { get; set; }

    public int? Taikhoanidtaikhoan { get; set; }

    public string? Makhachhang { get; set; }

    public string? Tenkhachhang { get; set; }

	[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
	public DateTime? Ngaysinh { get; set; }

    public string? Diachi { get; set; }

    public string? Sdt { get; set; }

	[NotMapped]
	public required IFormFile FrontImage { get; set; }
	public string? Image { get; set; }

    public string? Email { get; set; }

    public string? Masothue { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<NganhangKhachhang> NganhangKhachhangs { get; set; } = new List<NganhangKhachhang>();

    public virtual ICollection<Phieuxuatkho> Phieuxuatkhos { get; set; } = new List<Phieuxuatkho>();

    public virtual Taikhoan? TaikhoanidtaikhoanNavigation { get; set; }
}
