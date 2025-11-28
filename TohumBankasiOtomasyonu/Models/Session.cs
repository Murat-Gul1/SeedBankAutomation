using TohumBankasiOtomasyonu.Models;

namespace TohumBankasiOtomasyonu
{
    public static class Session
    {
        // Şu an giriş yapmış kullanıcıyı burada tutacağız.
        // Static olduğu için her yerden (Form1, UcUrunKarti, Sepet) erişilebilir.
        public static Kullanicilar AktifKullanici { get; set; } = null;
    }
}