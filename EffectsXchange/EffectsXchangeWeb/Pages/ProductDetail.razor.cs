using EffectsXchangeData.Models.Store;
using EffectsXchangeWeb.Services;
using Microsoft.AspNetCore.Components;

namespace EffectsXchangeWeb.Pages; 

public partial class ProductDetail {
    #region Properties
    protected ProductDetailModel ProductDetails { get; set; } = new ProductDetailModel();
    protected IEnumerable<ProductImageListModel> ProductImages { get; set; } = Enumerable.Empty<ProductImageListModel>();
    string CurrentImage = string.Empty;
    int ImageCount = 0;
    bool IsLoading = true;
    #endregion


    #region Parameters
    [Parameter] public long Id { get; set; }
    #endregion


    #region Injectables
    [Inject] IStoreService StoreService { get; set; } = default!;
    #endregion




    #region Protected Methods
    protected override async Task OnInitializedAsync() {
        await LoadData();

        _ = base.OnInitializedAsync();
    }

    protected void LoadImage(string image) {
        CurrentImage = image;
    }
    #endregion


    #region Private Methods
    private async Task LoadData() {
        try {
            ProductDetails = await StoreService.ProductDetail(Program.OrgId, Id);

            if (ProductDetails != null) {
                ProductImages = await StoreService.ProductImageList(Program.OrgId, Id);
                var currentImage = ProductImages.Where(p => p.IsFirst).FirstOrDefault();

                if (currentImage != null) {
                    CurrentImage = Program.ImageAsset + currentImage.Folder + "/" + currentImage.Image + currentImage.ImageExtension;
                }
            }

            await Task.Delay(1000);
            IsLoading = false;
        } catch (Exception ex) {
        }
    }
    #endregion

}
