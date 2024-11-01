using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using Models;
using System.Security.Claims;
using System.Text.Json;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

       public async Task<RegistrationResult> Register(RegisterDto registerDto)
    {
        var response = await _httpClient.PostAsJsonAsync("Auth/register", registerDto);
        
        if (response.IsSuccessStatusCode)
        {
            return new RegistrationResult { IsSuccess = true };
        }

        // If the response is not successful, read the error message
        var errorMessage = await response.Content.ReadAsStringAsync();
        return new RegistrationResult { IsSuccess = false, ErrorMessage = errorMessage };
    }

    public async Task<string> Login(LoginDto loginDto)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/Auth/login", loginDto);
        if (response.IsSuccessStatusCode)
        {
            var token = await response.Content.ReadAsStringAsync();
            await _localStorage.SetItemAsync("authToken", token);
            return token;
        }
        return null;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
    }
    public async Task<OperationResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
    {
        var response = await _httpClient.PostAsJsonAsync("Auth/forgot-password", forgotPasswordDto);
        if (response.IsSuccessStatusCode)
        {
            return new OperationResult { IsSuccess = true };
        }
        var errorMessage = await response.Content.ReadAsStringAsync();
        return new OperationResult { IsSuccess = false, ErrorMessage = errorMessage };
    }

    

    public async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        var identity = string.IsNullOrWhiteSpace(token) ? 
            new ClaimsIdentity() : 
            new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
        var user = new ClaimsPrincipal(identity);
        return new AuthenticationState(user);
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
    }

    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}

public class OperationResult
{
    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; }
}

public class RegistrationResult
{
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
}