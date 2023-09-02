using System;
using System.Collections.Generic;

namespace e_okul.Models;

public partial class Ogrenci
{
    public int OgrId { get; set; }

    public int? OgrNo { get; set; }

    public string? OgrAdi { get; set; }

    public string? OgrSoyadi { get; set; }

    public string? OgrMail { get; set; }

    public int? OgrBolumId { get; set; }

    public double? Gno { get; set; }

    public virtual ICollection<Donem> Donems { get; set; } = new List<Donem>();

    public virtual ICollection<Notlar> Notlars { get; set; } = new List<Notlar>();

    public virtual Bolum? OgrBolum { get; set; }
}
