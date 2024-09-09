using EffectsXchangeData.Models.Customer;
using EffectsXchangeWeb.Components;
using EffectsXchangeWeb.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace EffectsXchangeWeb.Pages.Customer;

public partial class List {
    #region Properties
    protected IEnumerable<CustomerModel> CustomerList { get; set; } = Enumerable.Empty<CustomerModel>();

    protected bool HasLoaded = false;
    #endregion


    #region Inject
    [Inject]
    private ICustomerService CustomerService { get; set; } = default!;

    [Inject]
    private IDialogService Dialog { get; set; } = default!;
    #endregion



    // ----------------     Init --------------------------------
    protected override async Task OnInitializedAsync() {
        await Init();
        await base.OnInitializedAsync();
    }

    private async Task Init() {
        await LoadGrid();
        await Task.Delay(250);
        HasLoaded = true;
    }
    // ------------------------------------------------------------



    // ----------------     Grid        ---------------------------
    private async Task LoadGrid() {
        var dialog = Dialog.Show<LoadingModal>("Custom Options Dialog", loadingOptions);

        CustomerList = await CustomerService.CustomerList();

        await Task.Delay(500);
        dialog.Close();
    }
    // ------------------------------------------------------------


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
