using EffectsXchangeApi.Delegate;
using EffectsXchangeData;
using EffectsXchangeData.Models.Application;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

Initialize(builder.Configuration);
ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");


// ----------------    Customer     ---------------------------
app.MapGet("/customer/list", async (ICustomer customerService) => await customerService.CustomerList());
// ------------------------------------------------------------


// ----------------    Security     ---------------------------
app.MapGet("/security/login/{organizationId}/{emailAddress}/{password}", async (int organizationId,
                                                                         string emailAddress,
                                                                         string password,
                                                                         ISecurity securityService) => await securityService.Login(organizationId, emailAddress, password));
// ------------------------------------------------------------



// ----------------    Application     ---------------------------
app.MapGet("/application/CategoryList/{organizationId}", async (int organizationId, IApplication applicationService) => await applicationService.CategoryNavigationList(organizationId));

app.MapPut("/application/FileUpload", async (FileUploadModel fileUpload, IApplication applicationService) => await applicationService.FileUpload(fileUpload));
// ------------------------------------------------------------



// ----------------    Store     ---------------------------
app.MapGet("/store/ProductDetail/{organizationId}/{id}", async (int organizationId, long Id, IStore storeService) => await storeService.ProductDetail(organizationId, Id));

app.MapGet("/store/ProductList/{organizationId}/{categoryType}/{category}", async (int organizationId, Int16 categoryType, long category, IStore storeService) => await storeService.ProductList(organizationId, categoryType, category));

app.MapGet("/store/ProductSearch/{organizationId}/{searchString}", async (int organizationId, string searchString, IStore storeService) => await storeService.ProductSearch(organizationId, searchString));

app.MapGet("/store/ProductTrendingList/{organizationId}", async (int organizationId, IStore storeService) => await storeService.ProductTrendingList(organizationId));

app.MapGet("/store/ProductViewList/{organizationId}/{customerId}", async (int organizationId, long customerId, IStore storeService) => await storeService.ProductViewList(organizationId, customerId));

app.MapGet("/store/ProductImageList/{organizationId}/{id}", async (int organizationId, long id, IStore storeService) => await storeService.ProductImageList(organizationId, id));
// ------------------------------------------------------------



//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();

app.Run();


void Initialize(IConfiguration config) {
#pragma warning disable CS8601
    DatabaseConnection = config.GetConnectionString("DefaultConnection");
#pragma warning restore CS8601
}



void ConfigureServices(IServiceCollection services) {
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddCors(policy =>
    {
        policy.AddPolicy("CorsPolicy", opt => opt
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
    });

    services.AddDbContextFactory<DatabaseContext>(options => options.UseSqlServer(DatabaseConnection));

    services.AddScoped<ICustomer, Customer>();
    services.AddScoped<ISecurity, Security>();
    services.AddScoped<IApplication, Application>();
    services.AddScoped<IStore, Store>();
}


partial class Program {
    public static string DatabaseConnection { get; set; } = string.Empty;
}