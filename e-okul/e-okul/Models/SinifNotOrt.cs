using System;
using System.Collections.Generic;

namespace e_okul.Models;

public partial class SinifNotOrt
{
    public int OrtId { get; set; }

    public int DersId { get; set; }

    public int? SinifOrt { get; set; }

    public int? BolumId { get; set; }

    public virtual Bolum? Bolum { get; set; }

    public virtual Der Ders { get; set; } = null!;
}
