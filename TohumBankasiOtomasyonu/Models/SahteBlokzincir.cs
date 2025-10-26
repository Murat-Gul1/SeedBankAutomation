using System;
using System.Collections.Generic;

namespace TohumBankasiOtomasyonu.Models;

public partial class SahteBlokzincir
{
    public int BlokId { get; set; }

    public string ZamanDamgasi { get; set; }

    public string OncekiHash { get; set; }

    public string Hash { get; set; }

    public string Veri { get; set; }

    public int Nonce { get; set; }
}
