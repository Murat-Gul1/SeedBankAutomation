using System;
using System.Collections.Generic;

namespace TohumBankasiOtomasyonu.Models;

public partial class KullaniciBitkileri
{
    public int Id { get; set; }

    public int KullaniciId { get; set; }

    public string BitkiAdi { get; set; }

    public string GorselYolu { get; set; }

    public string OlusturmaTarihi { get; set; }

    public virtual ICollection<BitkiSohbetGecmisi> BitkiSohbetGecmisis { get; set; } = new List<BitkiSohbetGecmisi>();

    public virtual Kullanicilar Kullanici { get; set; }
}
