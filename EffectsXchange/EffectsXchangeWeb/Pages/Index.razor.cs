using EffectsXchangeData.Models.Store;
using EffectsXchangeShared;
using EffectsXchangeWeb.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MudBlazor.Services;

namespace EffectsXchangeWeb.Pages;

public partial class Index {
    // --------------------------------------------------------------------
    #region Properties
    long UserId = 0;
    string? SearchString { get; set; }

    IEnumerable<ProductTrendingModel> ProductTrending { get; set; } = Enumerable.Empty<ProductTrendingModel>();
    IEnumerable<ProductViewModel> ProductView { get; set; } = Enumerable.Empty<ProductViewModel>();
    #endregion


    #region Parameters
    [CascadingParameter(Name = "UserEmail")] string? UserEmail { get; set; }
    [CascadingParameter(Name = "UserFirstName")] string? UserFirstName { get; set; }
    [CascadingParameter(Name = "UserMiddleName")] string? UserMiddleName { get; set; }
    [CascadingParameter(Name = "UserLastName")] string? UserLastName { get; set; }

#pragma warning disable CS8625
    [Inject] private ProtectedLocalStorage Storage { get; set; } = default;
    [Inject] private IStoreService StoreService { get; set; } = default;
    [Inject] NavigationManager Navigation { get; set; } = default!;
#pragma warning restore CS8625
    #endregion
    // --------------------------------------------------------------------


    protected override async Task OnInitializedAsync() {
        _ = GetCookieInformation();

        await LoadTrending();
        await base.OnInitializedAsync();
    }

    protected async Task LoadTrending() {
        try {
            ProductTrending = await StoreService.ProductTrendingList(Program.OrgId);
            ProductView = await StoreService.ProductViewList(Program.OrgId, Program.CustomerId);
        } catch (Exception ex) { }
    }


    private async Task GetCookieInformation() {
        var result = await Storage.GetAsync<long>("userId");
        UserId = result.Success ? result.Value : 0;
        StateHasChanged();
    }

    protected async Task SearchProducts() {
        if (!string.IsNullOrEmpty(SearchString)) {
            if (SearchString.Length > 2) {
                Navigation.NavigateTo($"/ProductSearch/{EffectsXchangeShared.Utility.ParameterEncode(SearchString)}");
            }
        }
    }

    protected void SearchChanged(KeyboardEventArgs args) {
        if (args.Key == "Enter") {
            _ = SearchProducts();
        }
    }

    protected void GoToProduct(long id) {
        Navigation.NavigateTo($"/ProductDetail/{id}");
    }
}
