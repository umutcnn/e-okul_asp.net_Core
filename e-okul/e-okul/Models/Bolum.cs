using System;
using System.Collections.Generic;

namespace e_okul.Models;

public partial class Bolum
{
    public int BolumId { get; set; }

    public string? BolumAdi { get; set; }

    public virtual ICollection<Ogrenci> Ogrencis { get; set; } = new List<Ogrenci>();

    public virtual ICollection<SinifNotOrt> SinifNotOrts { get; set; } = new List<SinifNotOrt>();
}
