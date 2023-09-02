using System;
using System.Collections.Generic;

namespace e_okul.Models;

public partial class Ogretman
{
    public int OgretmenId { get; set; }

    public string? Adi { get; set; }

    public string? Soyadi { get; set; }

    public virtual ICollection<Der> Ders { get; set; } = new List<Der>();

    public virtual ICollection<Notlar> Notlars { get; set; } = new List<Notlar>();
}
