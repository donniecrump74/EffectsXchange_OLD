using EffectsXchangeData.Models.Application;
using System.Web;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace EffectsXchangeWeb.Services;

interface ISecurityService {
    public Task<UserModel> Login(int organizationId, string emailAddress, string password);
    public Task<bool> Logout();
}



public class SecurityService : ISecurityService {
    private readonly HttpClient client;
    private readonly ProtectedLocalStorage Storage;

    public SecurityService(HttpClient httpClient, ProtectedLocalStorage storage) {
        client = httpClient;
        Storage = storage;
    }

    public async Task<UserModel> Login(int organizationId, string emailAddress, string password) {
        UserModel userLogin = new();

        try {
            string url = $"login/{organizationId}/{HttpUtility.UrlEncode(emailAddress)}/{HttpUtility.UrlEncode(password)}";

#pragma warning disable CS8600
            userLogin = await client.GetFromJsonAsync<UserModel>(url);
#pragma warning restore CS8600
        } catch (Exception ex) {
            Exception _ex = ex;
        }

#pragma warning disable CS8603
        return userLogin;
#pragma warning restore CS8603
    }

    public async Task<bool> Logout() {
        await Storage.DeleteAsync("userId");
        await Storage.DeleteAsync("userFirstName");
        await Storage.DeleteAsync("userLastName");
        await Storage.DeleteAsync("userFullName");
        await Storage.DeleteAsync("userEmailAddress");

        return true;
    }
}
