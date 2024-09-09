using EffectsXchangeWeb.Services;
using Microsoft.Extensions.FileProviders;
using MudBlazor.Services;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

Initialize(builder.Configuration);
ConfigureServices(builder.Services);

var app = builder.Build();

ConfigureApplication(app);
app.Run();



// --------     Initialize Variables    -----------
void Initialize(IConfiguration config) {
#pragma warning disable CS8601
    ImageAsset = config.GetValue<string>("ImageAsset");
    ImageAssetPath = config.GetValue<string>("ImageAssetPath");
    CustomerAddress = config.GetValue<string>("CustomerAddress");
    SecurityAddress = config.GetValue<string>("SecurityAddress");
    ApplicationAddress = config.GetValue<string>("ApplicationAddress");
    StoreAddress = config.GetValue<string>("StoreAddress");
#pragma warning restore CS8601

    CustomerAddress = (ApiAddress + CustomerAddress);
    SecurityAddress = (ApiAddress + SecurityAddress);
    ApplicationAddress = (ApiAddress + ApplicationAddress);
    StoreAddress = (ApiAddress + StoreAddress);

    if (ImageAssetPath != null) {
        if (!Directory.Exists(ImageAssetPath)) {
            Directory.CreateDirectory(ImageAssetPath);
        }
    }
}


// --------     Configure Services    -----------
void ConfigureServices(IServiceCollection services) {
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();
    builder.Services.AddMudServices();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddScoped<BrowserService>();

    services.AddHttpClient<ICustomerService, CustomerService>().ConfigureHttpClient((serviceProvider, httpClient) => {
        httpClient.BaseAddress = new Uri(CustomerAddress);
        httpClient.Timeout = TimeSpan.FromMinutes(5);
        httpClient.DefaultRequestHeaders.Add("User-Agent", "EffectsXchange");
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    })
        .SetHandlerLifetime(TimeSpan.FromMinutes(5))
        .ConfigurePrimaryHttpMessageHandler(x => new HttpClientHandler {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                UseCookies = false,
                AllowAutoRedirect = false,
                UseDefaultCredentials = true,
        });

    services.AddHttpClient<ISecurityService, SecurityService>().ConfigureHttpClient((serviceProvider, httpClient) => {
        httpClient.BaseAddress = new Uri(SecurityAddress);
        httpClient.Timeout = TimeSpan.FromMinutes(5);
        httpClient.DefaultRequestHeaders.Add("User-Agent", "EffectsXchange");
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    })
        .SetHandlerLifetime(TimeSpan.FromMinutes(5))
        .ConfigurePrimaryHttpMessageHandler(x => new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            UseCookies = false,
            AllowAutoRedirect = false,
            UseDefaultCredentials = true,
        });

    services.AddHttpClient<IApplicationService, ApplicationService>().ConfigureHttpClient((serviceProvider, httpClient) => {
        httpClient.BaseAddress = new Uri(ApplicationAddress);
        httpClient.Timeout = TimeSpan.FromMinutes(5);
        httpClient.DefaultRequestHeaders.Add("User-Agent", "EffectsXchange");
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    })
        .SetHandlerLifetime(TimeSpan.FromMinutes(5))
        .ConfigurePrimaryHttpMessageHandler(x => new HttpClientHandler {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            UseCookies = false,
            AllowAutoRedirect = false,
            UseDefaultCredentials = true,
        });

    services.AddHttpClient<IStoreService, StoreService>().ConfigureHttpClient((serviceProvider, httpClient) => {
        httpClient.BaseAddress = new Uri(StoreAddress);
        httpClient.Timeout = TimeSpan.FromMinutes(5);
        httpClient.DefaultRequestHeaders.Add("User-Agent", "EffectsXchange");
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    })
        .SetHandlerLifetime(TimeSpan.FromMinutes(5))
        .ConfigurePrimaryHttpMessageHandler(x => new HttpClientHandler {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            UseCookies = false,
            AllowAutoRedirect = false,
            UseDefaultCredentials = true,
        });
}


// --------     Configure App           -----------
void ConfigureApplication(WebApplication app) {
    if (!app.Environment.IsDevelopment()) {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseStaticFiles(new StaticFileOptions {
        FileProvider = new PhysicalFileProvider(ImageAssetPath),
        RequestPath = "/ImageAssets"
    });
    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");
}



partial class Program {
    public static int OrgId = 1111;
    public static int CustomerId = 1;
    public static string ApiAddress { get; set; } = "https://localhost:7135/";
    public static string CustomerAddress { get; set; } = "customer/";
    public static string SecurityAddress { get; set; } = "security/";
    public static string ApplicationAddress { get; set; } = "application/";
    public static string StoreAddress { get; set; } = "store/";
    public static string ImageAsset { get; set; } = default!;
    public static string ImageAssetPath { get; set; } = default!;
}