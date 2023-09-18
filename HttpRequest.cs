using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Data.Analysis;
using System.Net.Http;
using Microsoft.VisualBasic;

namespace HttpRequest
{

    public class PianoioUser
    {
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? personal_name { get; set; }
        public string? email { get; set; }
        public string? uid { get; set; }
        public string? image1 { get; set; }
        public int create_date { get; set; }
        public bool reset_password_email_sent { get; set; }
        public List<string>? custom_fields { get; set; }

    }

    public class PianoioSearch
    {
        public int code { get; set; }
        public int ts { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public int total { get; set; }
        public int count { get; set; }
        public List<PianoioUser>? users { get; set; }
    }
    class Requests
    {
       
        public static async Task<PianoioSearch> GetSearch(string q)
        {
            string url = "https://sandbox.piano.io/api/v3/publisher/user/list";
            
            var values = new Dictionary<string, string>
            {
                {"api_token", "xeYjNEhmutkgkqCZyhBn6DErVntAKDx30FqFOS6D"},
                {"aid", "o1sRRZSLlw"},
                {"offset", "0"},
                {"q", q}
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync(url, content);

            var responseString = await response.Content.ReadAsStringAsync();

            PianoioSearch replies = JsonSerializer.Deserialize<PianoioSearch>(responseString);

            return replies;

        }

        private static readonly HttpClient client = new HttpClient();

    }

}
