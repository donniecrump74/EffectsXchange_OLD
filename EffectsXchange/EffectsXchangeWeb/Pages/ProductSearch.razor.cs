using EffectsXchangeData.Models.Store;
using EffectsXchangeWeb.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace EffectsXchangeWeb.Pages;


public partial class ProductSearch {
    #region Properties
    protected IEnumerable<ProductListModel> ProductList { get; set; } = Enumerable.Empty<ProductListModel>();
    protected int ProductCountStart = 1;
    protected int ProductCountEnd = 55;
    protected int PageCountStart = 3;
    protected int PageCountEnd = 5;
    protected int TotalProducts = 0;
    protected int SortBy = 1;

    private string searchString = string.Empty;
    #endregion

    #region Parameters
    [Parameter] public string SearchString { get; set; } = default!;
    #endregion

    #region Injectables
    [Inject] IStoreService StoreService { get; set; } = default!;

    [Inject] NavigationManager Navigation { get; set; } = default!;
    #endregion


    #region Protected Methods
    protected override async Task OnInitializedAsync() {
        if (string.IsNullOrEmpty(SearchString)) {
            Navigation.NavigateTo($"/");
        } else {
            searchString = EffectsXchangeShared.Utility.ParameterDecode(SearchString);
            await LoadGrid();
        }

        _ = base.OnInitializedAsync();
    }

    protected void GoToProduct(long id) {
        Navigation.NavigateTo($"/ProductDetail/{id}");
    }
    #endregion


    #region Private Methods
    private async Task LoadGrid() {
        try {
            ProductList = await StoreService.ProductSearch(Program.OrgId, searchString);
            TotalProducts = ProductList.Select(p => p.TotalProducts).FirstOrDefault();
        } catch (Exception ex) {
            string sts = string.Empty;
        }
    }
    #endregion


}
