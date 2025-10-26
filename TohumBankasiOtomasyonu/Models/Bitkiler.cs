using System;
using System.Collections.Generic;

namespace TohumBankasiOtomasyonu.Models;

public partial class Bitkiler
{
    public int BitkiId { get; set; }

    public int StokAdedi { get; set; }

    public double Fiyat { get; set; }

    public int Aktif { get; set; }

    public virtual ICollection<BitkiCevirileri> BitkiCevirileris { get; set; } = new List<BitkiCevirileri>();

    public virtual ICollection<BitkiGorselleri> BitkiGorselleris { get; set; } = new List<BitkiGorselleri>();

    public virtual ICollection<SatisDetaylari> SatisDetaylaris { get; set; } = new List<SatisDetaylari>();
}
