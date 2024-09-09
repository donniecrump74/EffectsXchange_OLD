using EffectsXchangeData;
using EffectsXchangeData.Models.Application;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Web;

namespace EffectsXchangeApi.Delegate;

// -------------    Interface Definitions   ----------------------
interface IApplication {
    public Task<IEnumerable<CategoryNavigationModel>> CategoryNavigationList(int organizationId);
    public Task<bool> FileUpload(FileUploadModel fileUpload);
}
// ---------------------------------------------------------------



public class Application : IApplication {
    private readonly IDbContextFactory<DatabaseContext> dbCtx;

    public Application(IDbContextFactory<DatabaseContext> contextFactory) {
        dbCtx = contextFactory;
    }

    public async Task<IEnumerable<CategoryNavigationModel>> CategoryNavigationList(int organizationId) {
        IEnumerable<CategoryNavigationModel> catNav = Enumerable.Empty<CategoryNavigationModel>();

        try {
            using var ctx = dbCtx.CreateDbContext();
            SqlParameter pOrganizationId = new("@OrganizationId", SqlDbType.Int) { Value = organizationId };
            string sql = "EXEC [store].pCategoryNavigationList @OrganizationId";
            catNav = await ctx.CategoryNavigation.FromSqlRaw(sql, pOrganizationId).ToListAsync();
        } catch (Exception ex) {
             Exception tst = ex;
        }

        return catNav;
    }


    public async Task<bool> FileUpload(FileUploadModel fileUpload) {
        try {
            using var ctx = dbCtx.CreateDbContext();
            SqlParameter pFileName = new("@FileName", SqlDbType.VarChar) { Value = fileUpload.Name };
            SqlParameter pFileSize = new("@FileSize", SqlDbType.BigInt) { Value = fileUpload.Size };
            SqlParameter pFile = new("@File", SqlDbType.VarBinary, -1) { Value = fileUpload.File };

            string sql = "EXEC [store].pFileUpload @FileName, @FileSize, @File";
            await ctx.Database.ExecuteSqlRawAsync(sql, pFileName, pFileSize, pFile);
        } catch (Exception ex) {
            Exception tst = ex;
        }

        return true;
    }


}
