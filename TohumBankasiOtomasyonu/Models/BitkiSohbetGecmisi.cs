using System;
using System.Collections.Generic;

namespace TohumBankasiOtomasyonu.Models;

public partial class BitkiSohbetGecmisi
{
    public int MesajId { get; set; }

    public int KullaniciBitkiId { get; set; }

    public string Gonderen { get; set; }

    public string Mesaj { get; set; }

    public string Tarih { get; set; }

    public string ResimYolu { get; set; }

    public virtual KullaniciBitkileri KullaniciBitki { get; set; }
}
