using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TohumBankasiOtomasyonu
{
    public static class GeminiManager
    {
        // Google AI Studio'dan aldığın yeni API anahtarı
        // (BURAYA KENDİ KEY'İNİ YAZ)
        private const string ApiKey = "AIzaSyCrieAFLw3VR0QfRJK2rD2dmzEpUeRN2_E";

        // Kullanacağımız model (Gemini 2.5 Flash - hızlı ve resim destekli)
        private const string ApiUrl =
            "https://generativelanguage.googleapis.com/v1/models/gemini-2.5-flash:generateContent";

        /// <summary>
        /// Bitki resmini ve soruyu Gemini'ye gönderip analiz cevabını döner.
        /// </summary>
        public static async Task<string> BitkiAnalizEt(string soru, Image bitkiResmi)
        {
            if (bitkiResmi == null)
                return "Hata: Analiz için bir resim alınamadı.";

            try
            {
                using (var client = new HttpClient())
                {
                    // API anahtarını header olarak ekle
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("x-goog-api-key", ApiKey);

                    // 1. Resmi Base64 formatına çevir
                    string base64Image = ResimToBase64(bitkiResmi);

                    // 2. Google'ın istediği JSON formatını hazırla
                    var requestBody = new
                    {
                        contents = new[]
                        {
                            new
                            {
                                parts = new object[]
                                {
                                    new { text = soru }, // Kullanıcının sorusu
                                    new
                                    {
                                        inline_data = new
                                        {
                                            mime_type = "image/jpeg",
                                            data = base64Image
                                        }
                                    }
                                }
                            }
                        }
                    };

                    // 3. Veriyi JSON'a çevir
                    string jsonContent = JsonConvert.SerializeObject(requestBody);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    // 4. İsteği gönder
                    var response = await client.PostAsync(ApiUrl, content);

                    // 5. Cevabı oku
                    string responseString = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        // JSON içinden sadece metin cevabını çek
                        var jsonResponse = JObject.Parse(responseString);

                        string cevap = jsonResponse["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]?.ToString();

                        if (string.IsNullOrWhiteSpace(cevap))
                            return "Yapay zeka boş bir cevap döndürdü.";

                        return cevap.Trim();
                    }
                    else
                    {
                        return $"Hata oluştu! Kod: {response.StatusCode}\nDetay: {responseString}";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Bağlantı hatası: " + ex.Message;
            }
        }

        /// <summary>
        /// System.Drawing.Image'i Base64 string'e çevirir.
        /// </summary>
        private static string ResimToBase64(Image image)
        {
            using (MemoryStream m = new MemoryStream())
            {
                // GDI+ hatalarını azaltmak için yeni bir Bitmap üzerinden kaydediyoruz
                using (Bitmap bmp = new Bitmap(image))
                {
                    bmp.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg);
                }

                byte[] imageBytes = m.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }
    }
}
