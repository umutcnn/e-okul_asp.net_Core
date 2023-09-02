using System;
using System.Collections.Generic;

namespace e_okul.Models;

public partial class Harf
{
    public int HarfId { get; set; }

    public string? Harf1 { get; set; }

    public double? Katsayi { get; set; }

    public int? Ortalama { get; set; }

    public virtual ICollection<Notlar> Notlars { get; set; } = new List<Notlar>();
}
