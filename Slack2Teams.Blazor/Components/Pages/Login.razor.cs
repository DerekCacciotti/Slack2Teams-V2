using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using Slack2Teams.Shared.Interfaces;
using Slack2Teams.Shared.Models;

namespace Slack2Teams.Blazor.Components.Pages;

public partial class Login : ComponentBase
{
    [Inject]
    private  IAuthService _authService { get; set; }
    [Inject]
    private NavigationManager _navigationManager { get; set; }

    private LoginModel _model = new LoginModel();
    private RadzenLogin _loginForm;
    private IEnumerable<string> errors;
    private bool hasError;

    private async Task LoginUser(LoginArgs obj)
    {
        _model.UserName = obj.Username;
        _model.Password = obj.Password;
        var response = await _authService.Login(_model);
        if (response.IsSuccessful)
        {
            _navigationManager.NavigateTo("/");
        }
    }
}