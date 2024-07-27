using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Slack2Teams.Shared.Interfaces;
using Slack2Teams.Shared.Models;
using Slack2Teams.Shared.Models.Responses;
using Slack2Teams.Shared.Providers;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Slack2Teams.Shared.Services;

public class AuthService : IAuthService
{
    private readonly IHttpClientFactory _http;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ILocalStorageService _localStorage;

    public AuthService(IHttpClientFactory http, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorage)
    {
        _http = http;
        _authenticationStateProvider = authenticationStateProvider;
        _localStorage = localStorage;
    }

    public async Task<LoginResponse> Login(LoginModel model)
    {
        var user = JsonSerializer.Serialize(model);
        var clinet = _http.CreateClient("Slack2TeamsApi");
        var response = await clinet.PostAsync("Account/Login", new StringContent(user, Encoding.UTF8, "application/json"));
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LoginResponse>(content);
            await _localStorage.SetItemAsStringAsync("authToken", result.Token);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(model.UserName);
            clinet.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
            return result;
        }
        else
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LoginResponse>(content);
            return result;
        }
    }

    public async Task LogOut()
    {
        var clinet = _http.CreateClient("Slack2TeamsApi");
        await _localStorage.RemoveItemAsync("authToken");
        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
        clinet.DefaultRequestHeaders.Authorization = null;
    }

    public async Task<CreateAccountResponse> CreateAccount(CreateAccountModel model)
    {
        var clinet = _http.CreateClient("Slack2TeamsApi");
        var json = JsonSerializer.Serialize(model);
        var response = await clinet.PostAsync("Account/CreateAccount",
            new StringContent(json, Encoding.UTF8, "application/json"));
        var result = JsonConvert.DeserializeObject<CreateAccountResponse>(await response.Content.ReadAsStringAsync());
        return result;
    }
}