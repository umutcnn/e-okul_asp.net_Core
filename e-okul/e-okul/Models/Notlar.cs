using System;
using System.Collections.Generic;

namespace e_okul.Models;

public partial class Notlar
{
    public int NotId { get; set; }

    public int OgrId { get; set; }

    public int? DersId { get; set; }

    public int? OgretmenId { get; set; }

    public int? AraSinav { get; set; }

    public int? Final { get; set; }

    public int? Ortalama { get; set; }

    public int? HarfId { get; set; }

    public virtual Der? Ders { get; set; }

    public virtual Harf? Harf { get; set; }

    public virtual Ogrenci Ogr { get; set; } = null!;

    public virtual Ogretman? Ogretmen { get; set; }
}
