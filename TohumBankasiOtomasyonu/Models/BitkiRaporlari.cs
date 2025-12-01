using System;
using System.Collections.Generic;

namespace TohumBankasiOtomasyonu.Models;

public partial class BitkiRaporlari
{
    public int RaporId { get; set; }

    public int KullaniciBitkiId { get; set; }

    public string RaporTarihi { get; set; }

    public string RaporMetni { get; set; }

    public byte[] PdfDosyasi { get; set; }

    public virtual KullaniciBitkileri KullaniciBitki { get; set; }
}
