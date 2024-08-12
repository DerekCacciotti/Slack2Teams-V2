using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Newtonsoft.Json;
using Slack2Teams.Blazor.Interfaces;
using Slack2Teams.Shared.Models;
using Slack2Teams.Shared.Models.Responses;

namespace Slack2Teams.Blazor.Services;

public class UserTenantService: IUserTenantService
{
    private IHttpClientFactory _http;
    private ILocalStorageService _localStorage;

    public UserTenantService(IHttpClientFactory http, ILocalStorageService localStorage)
    {
        _http = http;
        _localStorage = localStorage;
    }


    public async Task<Guid> GetTenantIdForUser()
    {
        var token = await _localStorage.GetItemAsStringAsync("authToken");
        var clinet = _http.CreateClient("Slack2TeamsApi");
        clinet.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await clinet.GetAsync("Tenant/GetTenantForUser");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var tenant = JsonConvert.DeserializeObject<GetTenantResponse>(content);
            return tenant.TenantPK;
        }
        else
        {
            return Guid.Empty;
        }
    }

    public async Task SaveSlackTokenToTenant(AddSlackTokenModel model)
    {
        var token = await _localStorage.GetItemAsStringAsync("authToken");
        var client = _http.CreateClient("Slack2TeamsApi");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var json = JsonConvert.SerializeObject(model);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var response = await client.PostAsync("Tenant/SaveSlackToken", content);
        if (!response.IsSuccessStatusCode)
        {
            throw new ApplicationException("Failed to save token");
        }
    }
}