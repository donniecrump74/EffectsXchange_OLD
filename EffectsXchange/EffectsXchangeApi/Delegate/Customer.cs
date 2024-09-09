using EffectsXchangeData;
using EffectsXchangeData.Models.Customer;
using Microsoft.EntityFrameworkCore;

namespace EffectsXchangeApi.Delegate;

// -------------    Interface Definitions   ----------------------
interface ICustomer {
    public Task<IEnumerable<CustomerModel>> CustomerList();
}
// ---------------------------------------------------------------



public class Customer : ICustomer {
    private readonly IDbContextFactory<DatabaseContext> dbCtx;

    public Customer(IDbContextFactory<DatabaseContext> contextFactory) {
        dbCtx = contextFactory;
    }

    public async Task<IEnumerable<CustomerModel>> CustomerList() {
        IEnumerable<CustomerModel> customerList = Enumerable.Empty<CustomerModel>();

        try {
            string sql = "EXEC customer.pList";
            using var ctx = dbCtx.CreateDbContext();
            var rtn = await ctx.CustomerList.FromSqlRaw(sql).ToListAsync();

            if (rtn != null) {
                customerList = await ctx.CustomerList.FromSqlRaw(sql).ToListAsync();
            }
        } catch (Exception ex) {
            Exception tst = ex;
        }

        return customerList;
    }
}
