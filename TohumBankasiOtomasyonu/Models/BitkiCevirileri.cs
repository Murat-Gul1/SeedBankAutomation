using System;
using System.Collections.Generic;

namespace TohumBankasiOtomasyonu.Models;

public partial class BitkiCevirileri
{
    public int CeviriId { get; set; }

    public int BitkiId { get; set; }

    public string DilKodu { get; set; }

    public string BitkiAdi { get; set; }

    public string BilimselAd { get; set; }

    public string Aciklama { get; set; }

    public string YetistirmeKosullari { get; set; }

    public string SaklamaKosullari { get; set; }

    public virtual Bitkiler Bitki { get; set; }
}
