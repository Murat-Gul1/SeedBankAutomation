using System.Collections.Generic;
using System.Linq;
using TohumBankasiOtomasyonu.Models;

namespace TohumBankasiOtomasyonu
{
    public static class SepetManager
    {
        // Sepetteki ürünlerin listesi
        // List of products in the cart
        private static List<SepetElemani> _sepetListesi = new List<SepetElemani>();

        // Sepete Ürün Ekleme
        // Geriye 'bool' dönecek: Başarılıysa true, stok yetersizse false
        // Adding Product to Cart
        // Will return 'bool': true if successful, false if insufficient stock
        public static bool Ekle(int id, string ad, double fiyat, int adet = 1)
        {
            using (var db = new TohumBankasiContext())
            {
                // 1. Veritabanından güncel stoğu çek
                // 1. Fetch current stock from database
                var urunDB = db.Bitkilers.Find(id);
                if (urunDB == null) return false; // Ürün bulunamadı (Product not found)

                int stoktakiAdet = urunDB.StokAdedi;

                // 2. Sepette zaten var mı?
                // 2. Is it already in the cart?
                var sepettekiUrun = _sepetListesi.FirstOrDefault(x => x.BitkiId == id);
                int sepettekiAdet = (sepettekiUrun != null) ? sepettekiUrun.Adet : 0;

                // 3. Kontrol: (Sepetteki + Eklenecek) > Stoktaki ?
                // 3. Check: (In Cart + To Add) > In Stock ?
                if (sepettekiAdet + adet > stoktakiAdet)
                {
                    // Stok yetersiz!
                    // Insufficient stock!
                    return false;
                }

                // 4. Ekleme İşlemi (Stok yeterliyse)
                // 4. Addition Operation (If stock is sufficient)
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
                return true; // Başarılı (Successful)
            }
        }

        // Sepetten Ürün Silme
        // Deleting Product from Cart
        public static void Sil(int id)
        {
            var urun = _sepetListesi.FirstOrDefault(x => x.BitkiId == id);
            if (urun != null)
            {
                _sepetListesi.Remove(urun);
            }
        }

        // Sepeti Temizleme
        // Clearing Cart
        public static void Temizle()
        {
            _sepetListesi.Clear();
        }

        // Sepet Listesini Getir
        // Get Cart List
        public static List<SepetElemani> ListeyiGetir()
        {
            return _sepetListesi;
        }

        // Genel Toplam
        // Grand Total
        public static double GenelToplam()
        {
            return _sepetListesi.Sum(x => x.ToplamTutar);
        }

        public static void AdetDusur(int id)
        {
            // Ürünü bul
            // Find product
            var urun = _sepetListesi.FirstOrDefault(x => x.BitkiId == id);

            if (urun != null)
            {
                // Eğer sepette 1'den fazla varsa, sayısını azalt
                // If there is more than 1 in the cart, decrease its count
                if (urun.Adet > 1)
                {
                    urun.Adet--;
                }
                else
                {
                    // Eğer sadece 1 tane kaldıysa ve azaltılıyorsa, tamamen sil
                    // If only 1 is left and it is being decreased, delete completely
                    _sepetListesi.Remove(urun);
                }
            }
        }
    }
}