using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Radzen;
using Radzen.Blazor;
using Slack2Teams.Shared.Interfaces;
using Slack2Teams.Shared.Models;

namespace Slack2Teams.Blazor.Components.Pages;

public partial class Register : ComponentBase
{
    [Inject]
    private NavigationManager _navigationManager { get; set; }
    [Inject]
    private IAuthService _authService { get; set; }
    [Inject]
    private NotificationService _notificationService { get; set; }

    private CreateAccountModel _model = new CreateAccountModel();
    private RadzenTemplateForm<CreateAccountModel> _form { get; set; }
    private List<string> errors = new List<string>();
    private bool hasError;
    
    private async Task RegisterAccount()
    {
        _form.EditContext.Validate();

        if (_form.IsValid)
        {
            if (string.IsNullOrEmpty(_model.TenantName))
            {
                _model.TenantName = "";
            }
            var response =  await _authService.CreateAccount(_model);
            if (response.IsSuccessful)
            {
               _notificationService.Notify(NotificationSeverity.Success, "Account Created", "Account Created Successfully. You can now login with your username and password.");
                _navigationManager.NavigateTo("/Login");
            }
            else
            {
                hasError = true;
                foreach (var error in response.Errors)
                {
                    
                    errors.Add(error);
                }
            }
        }
    }

    private async Task Cancel()
    {
        _navigationManager.NavigateTo("/");
    }
    
}