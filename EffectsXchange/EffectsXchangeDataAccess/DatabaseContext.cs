using EffectsXchangeData.Models.Application;
using EffectsXchangeData.Models.Customer;
using EffectsXchangeData.Models.Store;
using Microsoft.EntityFrameworkCore;

namespace EffectsXchangeData;

public class DatabaseContext : DbContext {
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    #region DbSets
    // ------------     Customer    -----------------------------------
    public virtual DbSet<CustomerModel> CustomerList { get; set; }
    // ----------------------------------------------------------------

    // ------------     Security    -----------------------------------
    public virtual DbSet<UserModel> SecurityLogin { get; set; }
    // ----------------------------------------------------------------

    // ------------     Application    --------------------------------
    public virtual DbSet<CategoryNavigationModel> CategoryNavigation { get; set; }
    // ----------------------------------------------------------------

    // ------------     Store           -------------------------------
    public virtual DbSet<ProductListModel> ProductList { get; set; }
    public virtual DbSet<ProductListModel> ProductSearch { get; set; }
    public virtual DbSet<ProductTrendingModel> ProductTrendingList { get; set; }
    public virtual DbSet<ProductViewModel> ProductViewList { get; set; }
    public virtual DbSet<ProductDetailModel> ProductDetail { get; set; }
    public virtual DbSet<ProductImageListModel> ProductImageList { get; set; }
    // ----------------------------------------------------------------
    #endregion



    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        // ---------    Customer    ---------------------------
        //modelBuilder.Entity<CustomerModel>(entity => {
        //    entity.HasKey(c => new { c.Id });
        //    entity.Property(c => c.Id)
        //        .HasColumnName("Id")
        //        .IsUnicode(false);
        //    entity.Property(c => c.FirstName)
        //        .HasColumnName("FirstName")
        //        .IsUnicode(false);
        //});
        // ----------------------------------------------------

        base.OnModelCreating(modelBuilder);
    }
}