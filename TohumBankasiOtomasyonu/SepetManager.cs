using System.Collections.Generic;
using System.Linq;
using TohumBankasiOtomasyonu.Models;

namespace TohumBankasiOtomasyonu
{
    public static class SepetManager
    {
        // Sepetteki ürünlerin listesi
        private static List<SepetElemani> _sepetListesi = new List<SepetElemani>();

        // Sepete Ürün Ekleme
        // Geriye 'bool' dönecek: Başarılıysa true, stok yetersizse false
        public static bool Ekle(int id, string ad, double fiyat, int adet = 1)
        {
            using (var db = new TohumBankasiContext())
            {
                // 1. Veritabanından güncel stoğu çek
                var urunDB = db.Bitkilers.Find(id);
                if (urunDB == null) return false; // Ürün bulunamadı

                int stoktakiAdet = urunDB.StokAdedi;

                // 2. Sepette zaten var mı?
                var sepettekiUrun = _sepetListesi.FirstOrDefault(x => x.BitkiId == id);
                int sepettekiAdet = (sepettekiUrun != null) ? sepettekiUrun.Adet : 0;

                // 3. Kontrol: (Sepetteki + Eklenecek) > Stoktaki ?
                if (sepettekiAdet + adet > stoktakiAdet)
                {
                    // Stok yetersiz!
                    return false;
                }

                // 4. Ekleme İşlemi (Stok yeterliyse)
                if (sepettekiUrun != null)
                {
                    sepettekiUrun.Adet += adet;
                }
                else
                {
                    _sepetListesi.Add(new SepetElemani
                    {
                        BitkiId = id,
                        UrunAdi = ad,
                        BirimFiyat = fiyat,
                        Adet = adet
                    });
                }
                return true; // Başarılı
            }
        }

        // Sepetten Ürün Silme
        public static void Sil(int id)
        {
            var urun = _sepetListesi.FirstOrDefault(x => x.BitkiId == id);
            if (urun != null)
            {
                _sepetListesi.Remove(urun);
            }
        }

        // Sepeti Temizleme
        public static void Temizle()
        {
            _sepetListesi.Clear();
        }

        // Sepet Listesini Getir
        public static List<SepetElemani> ListeyiGetir()
        {
            return _sepetListesi;
        }

        // Genel Toplam
        public static double GenelToplam()
        {
            return _sepetListesi.Sum(x => x.ToplamTutar);
        }

        public static void AdetDusur(int id)
        {
            // Ürünü bul
            var urun = _sepetListesi.FirstOrDefault(x => x.BitkiId == id);

            if (urun != null)
            {
                // Eğer sepette 1'den fazla varsa, sayısını azalt
                if (urun.Adet > 1)
                {
                    urun.Adet--;
                }
                else
                {
                    // Eğer sadece 1 tane kaldıysa ve azaltılıyorsa, tamamen sil
                    _sepetListesi.Remove(urun);
                }
            }
        }
    }
}