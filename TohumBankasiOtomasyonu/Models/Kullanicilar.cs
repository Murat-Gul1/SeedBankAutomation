using System;
using System.Collections.Generic;

namespace TohumBankasiOtomasyonu.Models;

public partial class Kullanicilar
{
    public int KullaniciId { get; set; }

    public string KullaniciAdi { get; set; }

    public string SifreHash { get; set; }

    public string Email { get; set; }

    public string KullaniciTipi { get; set; }

    public string Ad { get; set; }

    public string Soyad { get; set; }

    public virtual ICollection<Satislar> Satislars { get; set; } = new List<Satislar>();
}
