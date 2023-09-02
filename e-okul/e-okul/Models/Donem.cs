using System;
using System.Collections.Generic;

namespace e_okul.Models;

public partial class Donem
{
    public int DonemId { get; set; }

    public int OgrId { get; set; }

    public int? DonemSayisi { get; set; }

    public int? TopAkts { get; set; }

    public int? TopDers { get; set; }

    public double? Dno { get; set; }

    public virtual Ogrenci Ogr { get; set; } = null!;
}
