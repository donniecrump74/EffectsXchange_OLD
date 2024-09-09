using EffectsXchangeData.Models.Store;

namespace EffectsXchangeWeb.Services;

interface IStoreService {
    public Task<ProductDetailModel> ProductDetail(int organizationId, long id);
    public Task<IEnumerable<ProductListModel>> ProductList(int organizationId, int categoryType, long category);
    public Task<IEnumerable<ProductListModel>> ProductSearch(int organizationId, string searchString);
    public Task<IEnumerable<ProductTrendingModel>> ProductTrendingList(int organizationId);
    public Task<IEnumerable<ProductViewModel>> ProductViewList(int organizationId, long customerId);
    public Task<IEnumerable<ProductImageListModel>> ProductImageList(int organizationId, long id);
}

public class StoreService : IStoreService {
    private readonly HttpClient client;


    public StoreService(HttpClient httpClient) {
        client = httpClient;
    }

    public async Task<ProductDetailModel> ProductDetail(int organizationId, long id) {
        ProductDetailModel productDetail = new();

        try {
            string url = $"productdetail/{organizationId}/{id}";

#pragma warning disable CS8600
            productDetail = await client.GetFromJsonAsync<ProductDetailModel>(url);
#pragma warning restore CS8600
        } catch (Exception ex) {
            Exception _ex = ex;
        }

#pragma warning disable CS8603
        return productDetail;
#pragma warning restore CS8603
    }

    public async Task<IEnumerable<ProductListModel>> ProductList(int organizationId, int categoryType, long category) {
        IEnumerable<ProductListModel> productList = Enumerable.Empty<ProductListModel>();

        try {
            string url = $"productlist/{organizationId}/{categoryType}/{category}";

#pragma warning disable CS8600
            productList = await client.GetFromJsonAsync<IEnumerable<ProductListModel>>(url);
#pragma warning restore CS8600
        } catch (Exception ex) {
            Exception _ex = ex;
        }

#pragma warning disable CS8603
        return productList;
#pragma warning restore CS8603
    }

    public async Task<IEnumerable<ProductListModel>> ProductSearch(int organizationId, string searchString) {
        IEnumerable<ProductListModel> productList = Enumerable.Empty<ProductListModel>();

        try {
            string url = $"productsearch/{organizationId}/{EffectsXchangeShared.Utility.ParameterEncode(searchString)}";

#pragma warning disable CS8600
            productList = await client.GetFromJsonAsync<IEnumerable<ProductListModel>>(url);
#pragma warning restore CS8600
        } catch (Exception ex) {
            Exception _ex = ex;
        }

#pragma warning disable CS8603
        return productList;
#pragma warning restore CS8603
    }

    public async Task<IEnumerable<ProductTrendingModel>> ProductTrendingList(int organizationId) {
        IEnumerable<ProductTrendingModel> trendingList = Enumerable.Empty<ProductTrendingModel>();

        try {
            string url = $"producttrendinglist/{organizationId}";

#pragma warning disable CS8600
            trendingList = await client.GetFromJsonAsync<IEnumerable<ProductTrendingModel>>(url);
#pragma warning restore CS8600
        } catch (Exception ex) {
            Exception _ex = ex;
        }

#pragma warning disable CS8603
        return trendingList;
#pragma warning restore CS8603
    }

    public async Task<IEnumerable<ProductViewModel>> ProductViewList(int organizationId, long customerId) {
        IEnumerable<ProductViewModel> viewList = Enumerable.Empty<ProductViewModel>();

        try {
            string url = $"productviewlist/{organizationId}/{customerId}";

            viewList = await client.GetFromJsonAsync<IEnumerable<ProductViewModel>>(url);
        } catch (Exception ex) {
            Exception _ex = ex;
        }

        return viewList;
    }

    public async Task<IEnumerable<ProductImageListModel>> ProductImageList(int organizationId, long id) {
        IEnumerable<ProductImageListModel> imageList = Enumerable.Empty<ProductImageListModel>();

        try {
            string url = $"ProductImageList/{organizationId}/{id}";

#pragma warning disable CS8600
            imageList = await client.GetFromJsonAsync<IEnumerable<ProductImageListModel>>(url);
#pragma warning restore CS8600
        } catch (Exception ex) {
            Exception _ex = ex;
        }

#pragma warning disable CS8603
        return imageList;
#pragma warning restore CS8603
    }
}
