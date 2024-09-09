using EffectsXchangeData.Models.Application;

namespace EffectsXchangeWeb.Services;

interface IApplicationService {
    public Task<IEnumerable<CategoryNavigationModel>> CategoryList(int organizationId);
    public Task<bool> FileUpload(FileUploadModel fileUpload);
}



public class ApplicationService : IApplicationService {
    private readonly HttpClient client;

    public ApplicationService(HttpClient httpClient) {
        client = httpClient;
    }

    public async Task<IEnumerable<CategoryNavigationModel>> CategoryList(int organizationId) {
        IEnumerable<CategoryNavigationModel> categoryList = Enumerable.Empty<CategoryNavigationModel>();

        try {
            string url = $"categorylist/{organizationId}";

#pragma warning disable CS8600
            categoryList = await client.GetFromJsonAsync<IEnumerable<CategoryNavigationModel>>(url);
#pragma warning restore CS8600
        } catch (Exception ex) {
            Exception _ex = ex;
        }

#pragma warning disable CS8603
        return categoryList;
#pragma warning restore CS8603
    }


    public async Task<bool> FileUpload(FileUploadModel fileUpload) {
        try {
            string url = $"fileupload/";

            await client.PutAsJsonAsync(url, fileUpload);
        } catch (Exception ex) {
            Exception _ex = ex;
        }

        return true;
    }
}
