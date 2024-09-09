using EffectsXchangeData;
using EffectsXchangeData.Models.Application;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Web;

namespace EffectsXchangeApi.Delegate;

// -------------    Interface Definitions   ----------------------
interface ISecurity {
    public Task<UserModel> Login(int organizationId, string emailAddress, string password);
}
// ---------------------------------------------------------------



public class Security : ISecurity {
    private readonly IDbContextFactory<DatabaseContext> dbCtx;

    public Security(IDbContextFactory<DatabaseContext> contextFactory) {
        dbCtx = contextFactory;
    }

    public async Task<UserModel> Login(int organizationId, string emailAddress, string password) {
        UserModel userLogin = new();

        try {
            using var ctx = dbCtx.CreateDbContext();
            SqlParameter pOrganizationId = new("@OrganizationId", SqlDbType.Int) { Value = organizationId };
            SqlParameter pEmailAddress = new("@EmailAddress", SqlDbType.VarChar) { Value = HttpUtility.UrlDecode(emailAddress) };
            SqlParameter pPassword = new("@Password", SqlDbType.VarChar) { Value = HttpUtility.UrlDecode(password) };

            string sql = "EXEC [user].pLogin @OrganizationId, @EmailAddress, @Password";

            var rtn = await ctx.SecurityLogin.FromSqlRaw(sql, pOrganizationId, pEmailAddress, pPassword).ToListAsync();

            if (rtn != null) {
#pragma warning disable CS8600
                userLogin = rtn.FirstOrDefault();
#pragma warning restore CS8600
            }
        } catch (Exception ex) {
             Exception tst = ex;
        }

#pragma warning disable CS8603
        return userLogin;
#pragma warning restore CS8603
    }
}
