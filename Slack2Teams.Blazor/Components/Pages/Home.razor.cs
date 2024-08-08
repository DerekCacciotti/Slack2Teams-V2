using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Slack2Teams.Shared.Settings;

namespace Slack2Teams.Blazor.Components.Pages;

public partial class Home
{
   [Inject]
   public IOptions<BlazorSettings> BlazorSettings { get; set; }
   [Inject]
   public NavigationManager NavigationManager { get; set; }
   
   private void Callback()
   {
     NavigationManager.NavigateTo(BlazorSettings.Value.Urls.SlackAuthorizeUrl);
   }
}