using System;
using System.Collections.Generic;

namespace e_okul.Models;

public partial class Der
{
    public int DersId { get; set; }

    public string? DersAdi { get; set; }

    public int? Akts { get; set; }

    public int? Kredi { get; set; }

    public int? HaftalikSaati { get; set; }

    public int? OgretmenId { get; set; }

    public virtual ICollection<Notlar> Notlars { get; set; } = new List<Notlar>();

    public virtual Ogretman? Ogretmen { get; set; }

    public virtual ICollection<SinifNotOrt> SinifNotOrts { get; set; } = new List<SinifNotOrt>();
}
