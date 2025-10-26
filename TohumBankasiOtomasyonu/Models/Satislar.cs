using System;
using System.Collections.Generic;

namespace TohumBankasiOtomasyonu.Models;

public partial class Satislar
{
    public int SatisId { get; set; }

    public int KullaniciId { get; set; }

    public string SatisTarihi { get; set; }

    public double ToplamTutar { get; set; }

    public string MakbuzNo { get; set; }

    public virtual Kullanicilar Kullanici { get; set; }

    public virtual ICollection<SatisDetaylari> SatisDetaylaris { get; set; } = new List<SatisDetaylari>();
}
