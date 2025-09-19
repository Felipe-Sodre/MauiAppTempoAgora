using MauiAppTempoAgora.Models;
using Newtonsoft.Json.Linq;

namespace MauiAppTempoAgora.Services
{
    internal static class DataServiceHelpers
    {
        public static async Task<Tempo?> GetPrevisao(string cidade, HttpRequestMessage resp)
        {
            Tempo? t = null;

            string chave = "6135072afe7f6cec1537d5cb08a5a1a2";

            string url = $"https://api.openweathermap.org/data/2.5/weather?" +
                            $"q={cidade}Jau&units=metric&appid={chave}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);
                if (!resp.IsSuccessStatusCode)
                {
                    return t;
                }
                string json = await resp.Content.ReadAsStringAsync();

                var rascunho = JObject.Parse(json);

                DateTime time = new();
                DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                DateTime sunset = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();

                t = new()
                {

                    lat = (double)rascunho["coord"]["lat"],
                    lon = (double)rascunho["coord"]["lon"],
                    temp_min = (double)rascunho["main"]["temp_min"],
                    temp_max = (double)rascunho["main"]["temp_max"],
                    visibility = (int)rascunho["visibility"],
                    speed = (double)rascunho["wind"]["speed"],
                    main = (string)rascunho["weather"][0]["main"],
                    description = (string)rascunho["weather"][0]["description"],
                    sunrise = sunrise.ToString(),
                    sunset = sunset.ToString(),
                };

                return t;
            }

            return t;
        }
    }
}