using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CUAHANGBANDAGRANITE.Models;

public partial class Nhanvien
{
    public int Idnhanvien { get; set; }

    public int? Taikhoanidtaikhoan { get; set; }

    public string? Manhanvien { get; set; }

    public string? Tennhanvien { get; set; }

    public string? Sdt { get; set; }

	[NotMapped]
	public required IFormFile FrontImage { get; set; }
	public string? Image { get; set; }

    public string? Email { get; set; }

	[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

	public DateTime? Ngaysinh { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<Phieunhapkho> Phieunhapkhos { get; set; } = new List<Phieunhapkho>();

    public virtual ICollection<Phieuthanhtoan> Phieuthanhtoans { get; set; } = new List<Phieuthanhtoan>();

    public virtual ICollection<Phieuthutien> Phieuthutiens { get; set; } = new List<Phieuthutien>();

    public virtual ICollection<Phieuxuatkho> Phieuxuatkhos { get; set; } = new List<Phieuxuatkho>();

    public virtual Taikhoan? TaikhoanidtaikhoanNavigation { get; set; }
}
