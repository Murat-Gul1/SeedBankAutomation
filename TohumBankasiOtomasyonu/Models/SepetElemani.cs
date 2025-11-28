namespace TohumBankasiOtomasyonu.Models
{
    public class SepetElemani
    {
        public int BitkiId { get; set; }
        public string UrunAdi { get; set; }
        public double BirimFiyat { get; set; }
        public int Adet { get; set; }

        public double ToplamTutar
        {
            get { return BirimFiyat * Adet; }
        }
    }
}