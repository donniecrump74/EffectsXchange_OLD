using EffectsXchangeData.Models.Application;
using EffectsXchangeWeb.Components;
using EffectsXchangeWeb.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace EffectsXchangeWeb.Pages;

public partial class Login {
    LoginModel login = new LoginModel();

    [Inject]
    private ISecurityService SecurityService { get; set; } = default!;

    [Inject]
    private ProtectedLocalStorage Storage { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    [Inject]
    private IDialogService Dialog { get; set; } = default!;

    [Inject]
    private ISnackbar Snackbar { get; set; } = default!;



    protected override Task OnInitializedAsync() {
        return base.OnInitializedAsync();
    }



    private void OnValidSubmit(EditContext context) {
        try {
            _ = UserLogin();
        } catch (Exception ex) {
            string message = ex.Message;
        }
    }

    private async Task UserLogin() {
        if (login.Password.Length > 7) {
            var dialog = Dialog.Show<LoadingModal>("Custom Options Dialog", loadingOptions);

            UserModel usr = await SecurityService.Login(1111, login.EmailAddress, login.Password);

            if (usr != null) {
                await Storage.SetAsync("userId", usr.Id);
                await Storage.SetAsync("userFirstName", usr.FirstName);
                await Storage.SetAsync("userLastName", usr.LastName);
                await Storage.SetAsync("userFullName", usr.LastName + ", " + usr.FirstName);
                await Storage.SetAsync("userEmailAddress", usr.EmailAddress);

                await Task.Delay(2000);
                dialog.Close();
                await Task.Delay(1000);

                Navigation.NavigateTo("/", true);
            } else {
                await Task.Delay(2000);
                dialog.Close();

                Snackbar.Clear();
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
                Snackbar.Configuration.VisibleStateDuration = 2000;
                Snackbar.Add("Login Incorrect!", Severity.Error, config =>
                {
                    config.ShowTransitionDuration = 500;
                    config.ShowCloseIcon = false;
                });
            }
        }
    }


    protected void PasswordChanged(KeyboardEventArgs args) {
        if (args.Key == "Enter") {
            _ = UserLogin();
        }
    }


    // ----------------     Dialog      ---------------------------
    DialogOptions loadingOptions = new DialogOptions() {
        Position = DialogPosition.Center,
        DisableBackdropClick = true,
        CloseButton = false,
        NoHeader = true
    };

    private void LoadingDialog() {
        Dialog.Show<LoadingModal>("Custom Options Dialog", loadingOptions);
    }
    // ------------------------------------------------------------


}
