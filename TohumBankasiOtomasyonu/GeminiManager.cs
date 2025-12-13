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
        // The model we will use (Gemini 2.5 Flash - fast and supports images)
        private const string ApiUrl =
            "https://generativelanguage.googleapis.com/v1/models/gemini-2.5-flash:generateContent";

        /// <summary>
        /// Bitki resmini ve soruyu Gemini'ye gönderip analiz cevabını döner.
        /// Sends plant image and question to Gemini and returns analysis answer.
        /// </summary>
        // Parametre değişti: 'Image bitkiResmi' -> 'List<Image> resimler'
        // Parameter changed: 'Image bitkiResmi' -> 'List<Image> resimler'
        public static async Task<string> BitkiAnalizEt(string soru, List<Image> resimler, string kullaniciApiKey)
        {
            if (string.IsNullOrEmpty(kullaniciApiKey)) return "Hata: API Anahtarı eksik.";

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("x-goog-api-key", kullaniciApiKey);

                    // --- İÇERİK PARÇALARINI HAZIRLA ---
                    // --- PREPARE CONTENT PARTS ---
                    var parcalar = new List<object>();

                    // 1. Soruyu ekle
                    // 1. Add Question
                    parcalar.Add(new { text = soru });

                    // 2. Resim listesini döngüyle ekle
                    // 2. Add image list with loop
                    if (resimler != null && resimler.Count > 0)
                    {
                        foreach (var img in resimler)
                        {
                            string base64Image = ResimToBase64(img);
                            parcalar.Add(new
                            {
                                inline_data = new
                                {
                                    mime_type = "image/jpeg",
                                    data = base64Image
                                }
                            });
                        }
                    }

                    // JSON Gövdesi
                    // JSON Body
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
        /// Converts System.Drawing.Image to Base64 string.
        /// </summary>
        private static string ResimToBase64(Image image)
        {
            using (MemoryStream m = new MemoryStream())
            {
                // GDI+ hatalarını azaltmak için yeni bir Bitmap üzerinden kaydediyoruz
                // We save over a new Bitmap to reduce GDI+ errors
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
