using System;
using System.Collections.Generic;

namespace TohumBankasiOtomasyonu.Models;

public partial class BitkiGorselleri
{
    public int GorselId { get; set; }

    public int BitkiId { get; set; }

    public string DosyaYolu { get; set; }

    public int AnaGorsel { get; set; }

    public virtual Bitkiler Bitki { get; set; }
}
