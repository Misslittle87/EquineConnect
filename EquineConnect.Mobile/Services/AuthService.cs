using System.Net.Http.Json;
using System.Net.Http.Headers;
using Microsoft.Maui.Storage;
using System.Text.Json;
using System.Diagnostics;

namespace EquineConnect.Mobile.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            var existing = Preferences.Get("AuthToken", null as string);
            if (!string.IsNullOrEmpty(existing))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", existing);
        }

        public async Task<string?> Login(string email, string password)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/auth/login",
                new
                {
                    email,
                    password,
                    rememberMe = true
                });

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            string? token = null;
            try
            {
                using var doc = JsonDocument.Parse(content);
                if (doc.RootElement.TryGetProperty("token", out var t))
                    token = t.GetString();
            }
            catch (JsonException)
            {
                // ignore
            }

            if (string.IsNullOrEmpty(token))
            {
                var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
                token = result?.Token;
            }

            if (!string.IsNullOrEmpty(token))
            {
                Preferences.Set("AuthToken", token);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return token;
            }

            return null;
        }

        public void Logout()
        {
            Preferences.Remove("AuthToken");
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        // Debug helper: log current Authorization header
        public void LogAuthHeader()
        {
            var auth = _httpClient.DefaultRequestHeaders.Authorization?.ToString() ?? "<none>";
            Debug.WriteLine($"AuthService: DefaultRequestHeaders.Authorization = {auth}");
        }

        // Helper to POST with logging of Authorization header (use for calling protected endpoints)
        public async Task<HttpResponseMessage> PostAuthorizedAsync(string uri, object body)
        {
            LogAuthHeader();
            var response = await _httpClient.PostAsJsonAsync(uri, body);
            Debug.WriteLine($"AuthService: POST {uri} returned {(int)response.StatusCode} {response.ReasonPhrase}");
            return response;
        }

        // Helper to GET with logging of Authorization header
        public async Task<HttpResponseMessage> GetAuthorizedAsync(string uri)
        {
            LogAuthHeader();
            var response = await _httpClient.GetAsync(uri);
            Debug.WriteLine($"AuthService: GET {uri} returned {(int)response.StatusCode} {response.ReasonPhrase}");
            return response;
        }
    }
}
