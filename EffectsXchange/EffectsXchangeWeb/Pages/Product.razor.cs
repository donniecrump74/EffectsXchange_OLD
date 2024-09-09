using EffectsXchangeData.Models.Store;
using EffectsXchangeWeb.Services;
using Microsoft.AspNetCore.Components;

namespace EffectsXchangeWeb.Pages;


public partial class Product {
    #region Properties
    protected IEnumerable<ProductListModel> ProductList { get; set; } = Enumerable.Empty<ProductListModel>();
    protected int ProductCountStart = 1;
    protected int ProductCountEnd = 55;
    protected int PageCountStart = 3;
    protected int PageCountEnd = 5;
    protected int TotalProducts = 0;
    protected int SortBy = 1;
    protected bool NoResults = true;
    #endregion

    #region Parameters
    [Parameter] public int Type { get; set; }
    [Parameter] public long Id { get; set; }
    #endregion

    #region Injectables
    [Inject] IStoreService StoreService { get; set; } = default!;

    [Inject] NavigationManager Navigation { get; set; } = default!;
    #endregion




    #region Protected Methods
    protected override async Task OnParametersSetAsync() {
        await LoadGrid();
    }


    //protected override async Task OnInitializedAsync() {
    //    await LoadGrid();

    //    _ = base.OnInitializedAsync();
    //}

    protected void GoToProduct(long id) {
        Navigation.NavigateTo($"/ProductDetail/{id}");
    }
    #endregion


    #region Private Methods
    private async Task LoadGrid() {
        try {
            NoResults = true;
            ProductList = await StoreService.ProductList(Program.OrgId, Type, Id);

            if (ProductList != null) {
                if (ProductList.Count() > 0) {
                    NoResults = false;
                    TotalProducts = ProductList.Select(p => p.TotalProducts).FirstOrDefault();
                }
            }

            if (NoResults) {
                ProductList = Enumerable.Empty<ProductListModel>();
            }
        } catch (Exception ex) {
        }
    }
    #endregion


}
