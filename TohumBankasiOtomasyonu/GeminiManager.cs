using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
namespace TohumBankasiOtomasyonu
{
    public static class GeminiManager
    {

        

        // Kullanacağımız model (Gemini 2.5 Flash - hızlı ve resim destekli)
        private const string ApiUrl =
            "https://generativelanguage.googleapis.com/v1/models/gemini-2.5-flash:generateContent";

        /// <summary>
        /// Bitki resmini ve soruyu Gemini'ye gönderip analiz cevabını döner.
        /// </summary>
        public static async Task<string> BitkiAnalizEt(string soru, Image bitkiResmi, string kullaniciApiKey)
        {
            // Sadece API Key kontrolü kalsın, resim zorunluluğunu kaldırdık.
            if (string.IsNullOrEmpty(kullaniciApiKey)) return "Hata: API Anahtarı eksik.";

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("x-goog-api-key", kullaniciApiKey);

                    // --- DİNAMİK İÇERİK OLUŞTURMA ---
                    // Google'a göndereceğimiz parça listesi (Soru kesin var, resim opsiyonel)
                    var parcalar = new List<object>();

                    // 1. Soruyu ekle
                    parcalar.Add(new { text = soru });

                    // 2. Resim varsa, onu da listeye ekle
                    if (bitkiResmi != null)
                    {
                        string base64Image = ResimToBase64(bitkiResmi);
                        parcalar.Add(new
                        {
                            inline_data = new
                            {
                                mime_type = "image/jpeg",
                                data = base64Image
                            }
                        });
                    }

                    // JSON Gövdesini oluştur
                    var requestBody = new
                    {
                        contents = new[]
                        {
                    new { parts = parcalar }
                }
                    };

                    string jsonContent = JsonConvert.SerializeObject(requestBody);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(ApiUrl, content);
                    string responseString = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = JObject.Parse(responseString);
                        string cevap = jsonResponse["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]?.ToString();
                        return cevap?.Trim() ?? "Cevap alınamadı.";
                    }
                    else
                    {
                        return $"Hata: {response.StatusCode} - {responseString}";
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
