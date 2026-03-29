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
        private AuthState? _authState;

        public AuthState CurrentUser => _authState ?? new AuthState();

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            var existing = Preferences.Get("AuthToken", null as string);
            if (!string.IsNullOrEmpty(existing) && TokenService.IsTokenValid(existing))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", existing);
                LoadAuthState(existing);
            }
            else if (!string.IsNullOrEmpty(existing))
            {
                // Token är utgången, ta bort den
                Preferences.Remove("AuthToken");
            }
        }

        private void LoadAuthState(string token)
        {
            _authState = new AuthState
            {
                Token = token,
                UserId = TokenService.GetUserId(token),
                Email = TokenService.GetEmail(token),
                Roles = TokenService.GetRoles(token)
            };
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

            if (!string.IsNullOrEmpty(token) && TokenService.IsTokenValid(token))
            {
                Preferences.Set("AuthToken", token);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                LoadAuthState(token);
                return token;
            }

            return null;
        }

        public async Task<string?> Register(string username, string email, string password)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/auth/register",
                new
                {
                    userName = username,
                    email,
                    password
                });

            if (!response.IsSuccessStatusCode)
                return null;

            // Efter registrering, logga in automatiskt
            return await Login(email, password);
        }

        public void Logout()
        {
            Preferences.Remove("AuthToken");
            _httpClient.DefaultRequestHeaders.Authorization = null;
            _authState = null;
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
