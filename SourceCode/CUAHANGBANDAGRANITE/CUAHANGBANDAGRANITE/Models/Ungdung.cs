using System;
using System.Collections.Generic;

namespace CUAHANGBANDAGRANITE.Models;

public partial class Ungdung
{
    public int Idungdung { get; set; }

    public string? Maungdung { get; set; }

    public string? Tenungdung { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<Noidungungdung> Noidungungdungs { get; set; } = new List<Noidungungdung>();
}
