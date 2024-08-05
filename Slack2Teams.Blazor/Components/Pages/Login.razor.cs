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
    private List<string> errors = new List<string>();
    private bool hasError;
    private RadzenTemplateForm<LoginModel> _form { get; set; }

    private async Task LoginUser(LoginArgs obj)
    {
        hasError = false;
        errors.Clear();
        _model.UserName = obj.Username;
        _model.Password = obj.Password;
        var response = await _authService.Login(_model);
        if (response.IsSuccessful)
        {
            _navigationManager.NavigateTo("/");
        }
        else
        {
            if (response.Errors.Any())
            {
                foreach (var message in response.Errors)
                {
                    errors.Add(message);
                }

                hasError = true;
            }

           
        }
    }
}