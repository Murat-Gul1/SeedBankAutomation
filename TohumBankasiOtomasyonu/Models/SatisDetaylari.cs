using System;
using System.Collections.Generic;

namespace TohumBankasiOtomasyonu.Models;

public partial class SatisDetaylari
{
    public int SatisDetayId { get; set; }

    public int SatisId { get; set; }

    public int BitkiId { get; set; }

    public int Miktar { get; set; }

    public double SatisAnindakiFiyat { get; set; }

    public virtual Bitkiler Bitki { get; set; }

    public virtual Satislar Satis { get; set; }
}
