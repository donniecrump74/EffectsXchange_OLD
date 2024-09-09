using EffectsXchangeData.Models.Application;
using EffectsXchangeWeb.Components;
using EffectsXchangeWeb.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MudBlazor;
using MudBlazor.Services;

namespace EffectsXchangeWeb.Shared;

public partial class MainLayout {
    protected long? UserId { get; set; }
    protected string? UserFirstName { get; set; }
    protected string? UserMiddleName { get; set; }
    protected string? UserLastName { get; set; }
    protected string? UserEmail { get; set; }
    protected bool SideBarOpen { get; set; } = false;
    protected int BrowserHeight { get; set; } = 0;
    protected int BrowserWidth { get; set; } = 0;
    protected string? ContainerHeight { get; set; }
    protected string? SearchString { get; set; }

    IEnumerable<CategoryNavigationModel> CategoryList = Enumerable.Empty<CategoryNavigationModel>();

    [Inject]
    BrowserService BrowserService { get; set; } = default!;

    [Inject]
    IApplicationService ApplicationService { get; set; } = default!;


    protected override async Task OnInitializedAsync() {
        CategoryList = await ApplicationService.CategoryList(Program.OrgId);

        var usrId = await Storage.GetAsync<long>("userId");
        UserId = usrId.Success ? usrId.Value : null;

        var usrFirstName = await Storage.GetAsync<string>("userFirstName");
        UserFirstName = usrFirstName.Success ? usrFirstName.Value : null;

        var usrMiddleName = await Storage.GetAsync<string>("userMiddleName");
        UserMiddleName = usrMiddleName.Success ? usrMiddleName.Value : null;

        var usrLastName = await Storage.GetAsync<string>("userLastName");
        UserLastName = usrLastName.Success ? usrLastName.Value : null;

        var usrEmail = await Storage.GetAsync<string>("userEmailAddress");
        UserEmail = usrEmail.Success ? usrEmail.Value : null;

        var sideBarOpen = await Storage.GetAsync<bool>("sideBarOpen");
        SideBarOpen = sideBarOpen.Success ? sideBarOpen.Value : false;

        await GetDimensions();
    }



    protected async Task ToggleDrawer() {
        var sideBarStatus = await Storage.GetAsync<bool>("sideBarOpen");
        bool SideBarStatus = sideBarStatus.Success ? sideBarStatus.Value : false;

        if (SideBarStatus) {
            SideBarOpen = false;
            await Storage.SetAsync("sideBarOpen", false);
        } else {
            SideBarOpen = true;
            await Storage.SetAsync("sideBarOpen", true);
        }

        StateHasChanged();
    }

    protected async Task Logout() {
        var dialog = Dialog.Show<LoadingModal>("Custom Options Dialog", loadingOptions);

        bool resp = await SecurityService.Logout();

        await Task.Delay(2000);
        dialog.Close();
        await Task.Delay(1000);

        Navigation.NavigateTo("/", true);
    }

    private async Task GetDimensions() {
        var dimension = await BrowserService.GetDimensions();
        BrowserHeight = dimension.Height;
        BrowserWidth = dimension.Width;

        if (BrowserHeight > 0) {
            var bh = (BrowserHeight + 600);
            ContainerHeight = bh.ToString() + ".px";
        }
    }



    // ----------------     Dialog      ---------------------------
    DialogOptions loadingOptions = new() {
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
