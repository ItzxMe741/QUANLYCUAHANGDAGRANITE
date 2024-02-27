using System;
using System.Collections.Generic;

namespace CUAHANGBANDAGRANITE.Models;

public partial class Noidungungdung
{
    public int Idndud { get; set; }

    public int? Ungdungidungdung { get; set; }

    public int? Dagraniteidda { get; set; }

    public bool? Active { get; set; }

    public virtual Dagranite? DagraniteiddaNavigation { get; set; }

    public virtual Ungdung? UngdungidungdungNavigation { get; set; }
}
