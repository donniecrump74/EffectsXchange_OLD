using EffectsXchangeData;
using EffectsXchangeData.Models.Store;
using EffectsXchangeShared;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Numerics;

namespace EffectsXchangeApi.Delegate;

// -------------    Interface Definitions   ----------------------
interface IStore {
    public Task<IEnumerable<ProductListModel>> ProductList(int organizationId, Int16 categoryType, long category);
    public Task<IEnumerable<ProductListModel>> ProductSearch(int organizationId, string searchString);
    public Task<IEnumerable<ProductTrendingModel>> ProductTrendingList(int organizationId);
    public Task<IEnumerable<ProductViewModel>> ProductViewList(int organizationId, long customerId);
    public Task<ProductDetailModel> ProductDetail(int organizationId, long id);
    public Task<IEnumerable<ProductImageListModel>> ProductImageList(int organizationId, long id);
}
// ---------------------------------------------------------------



public class Store : IStore {
    private readonly IDbContextFactory<DatabaseContext> dbCtx;

    public Store(IDbContextFactory<DatabaseContext> contextFactory) {
        dbCtx = contextFactory;
    }


    public async Task<IEnumerable<ProductListModel>> ProductList(int organizationId, Int16 categoryType, long category) {
        IEnumerable<ProductListModel> productTrending = Enumerable.Empty<ProductListModel>();

        try {
            using var ctx = dbCtx.CreateDbContext();
            SqlParameter pOrganizationId = new("@OrganizationId", SqlDbType.Int) { Value = organizationId };
            SqlParameter pCategoryType = new("@CategoryType", SqlDbType.SmallInt) { Value = categoryType };
            SqlParameter pCategory = new("@Category", SqlDbType.BigInt) { Value = category };
            string sql = "EXEC [store].pProductList @OrganizationId, @CategoryType, @Category";
            productTrending = await ctx.ProductList.FromSqlRaw(sql, pOrganizationId, pCategoryType, pCategory).ToListAsync();
        } catch (Exception ex) {
            Exception tst = ex;
        }

        return productTrending;
    }

    public async Task<IEnumerable<ProductListModel>> ProductSearch(int organizationId, string searchString) {
        IEnumerable<ProductListModel> productTrending = Enumerable.Empty<ProductListModel>();

        try {
            using var ctx = dbCtx.CreateDbContext();
            SqlParameter pOrganizationId = new("@OrganizationId", SqlDbType.Int) { Value = organizationId };
            SqlParameter pSearchString = new("@SearchString", SqlDbType.VarChar) { Value = Utility.ParameterDecode(searchString) };
            string sql = "EXEC [store].pProductSearch @OrganizationId, @SearchString";
            productTrending = await ctx.ProductSearch.FromSqlRaw(sql, pOrganizationId, pSearchString).ToListAsync();
        } catch (Exception ex) {
            Exception tst = ex;
        }

        return productTrending;
    }

    public async Task<IEnumerable<ProductTrendingModel>> ProductTrendingList(int organizationId) {
        IEnumerable<ProductTrendingModel> productTrending = Enumerable.Empty<ProductTrendingModel>();

        try {
            using var ctx = dbCtx.CreateDbContext();
            SqlParameter pOrganizationId = new("@OrganizationId", SqlDbType.Int) { Value = organizationId };
            string sql = "EXEC [store].pProductTrending @OrganizationId";
            productTrending = await ctx.ProductTrendingList.FromSqlRaw(sql, pOrganizationId).ToListAsync();
        } catch (Exception ex) {
             Exception tst = ex;
        }

        return productTrending;
    }

    public async Task<IEnumerable<ProductViewModel>> ProductViewList(int organizationId, long customerId) {
        IEnumerable<ProductViewModel> productView = Enumerable.Empty<ProductViewModel>();

        try {
            using var ctx = dbCtx.CreateDbContext();
            SqlParameter pOrganizationId = new("@OrganizationId", SqlDbType.Int) { Value = organizationId };
            SqlParameter pCustomerId = new("@CustomerId", SqlDbType.BigInt) { Value = customerId };
            string sql = "EXEC [store].pProductView @OrganizationId, @CustomerId";
            productView = await ctx.ProductViewList.FromSqlRaw(sql, pOrganizationId, pCustomerId).ToListAsync();
        } catch (Exception ex) {
            Exception tst = ex;
        }

        return productView;
    }

    public async Task<ProductDetailModel> ProductDetail(int organizationId, long id) {
        ProductDetailModel productDetail = new();

        try {
            using var ctx = dbCtx.CreateDbContext();
            SqlParameter pOrganizationId = new("@OrganizationId", SqlDbType.Int) { Value = organizationId };
            SqlParameter pId = new("@Id", SqlDbType.BigInt) { Value = id };
            string sql = "EXEC [store].pProductDetail @OrganizationId, @Id";

            var resp = await ctx.ProductDetail.FromSqlRaw(sql, pOrganizationId, pId).ToListAsync();

            if (resp != null) {
#pragma warning disable CS8600
                productDetail = resp.FirstOrDefault();
#pragma warning restore CS8600
            }
        } catch (Exception ex) {
            Exception tst = ex;
        }

#pragma warning disable CS8603
        return productDetail;
#pragma warning restore CS8603
    }

    public async Task<IEnumerable<ProductImageListModel>> ProductImageList(int organizationId, long id) {
        IEnumerable<ProductImageListModel> productImageList = Enumerable.Empty<ProductImageListModel>();

        try {
            using var ctx = dbCtx.CreateDbContext();
            SqlParameter pOrganizationId = new("@OrganizationId", SqlDbType.Int) { Value = organizationId };
            SqlParameter pProductId = new("@ProductId", SqlDbType.BigInt) { Value = id };
            string sql = "EXEC [store].pProductImageList @OrganizationId, @ProductId";
            productImageList = await ctx.ProductImageList.FromSqlRaw(sql, pOrganizationId, pProductId).ToListAsync();
        } catch (Exception ex) {
            Exception tst = ex;
        }

        return productImageList;
    }
}
