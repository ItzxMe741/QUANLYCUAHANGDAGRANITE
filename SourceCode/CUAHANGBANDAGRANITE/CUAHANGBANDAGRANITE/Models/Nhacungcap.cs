using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CUAHANGBANDAGRANITE.Models;

public partial class Nhacungcap
{
    public int Idncc { get; set; }

    public int? Taikhoanidtaikhoan { get; set; }

    public string? Mancc { get; set; }

    public string? Tenncc { get; set; }

    public string? Diachi { get; set; }

    public string? Sdt { get; set; }

    public string? Masothue { get; set; }

    public string? Email { get; set; }

    public string? Ghichu { get; set; }

	[NotMapped]
	public required IFormFile FrontImage { get; set; }
	public string? Image { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<NganhangNhacungcap> NganhangNhacungcaps { get; set; } = new List<NganhangNhacungcap>();

    public virtual ICollection<Phieunhapkho> Phieunhapkhos { get; set; } = new List<Phieunhapkho>();

    public virtual Taikhoan? TaikhoanidtaikhoanNavigation { get; set; }
}
