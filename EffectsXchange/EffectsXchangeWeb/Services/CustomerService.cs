using EffectsXchangeData.Models.Customer;

namespace EffectsXchangeWeb.Services;

interface ICustomerService {
    public Task<IEnumerable<CustomerModel>> CustomerList();
}



public class CustomerService : ICustomerService {
    private readonly HttpClient client;

    public CustomerService(HttpClient httpClient) {
        client = httpClient;
    }

    public async Task<IEnumerable<CustomerModel>> CustomerList() {
        IEnumerable<CustomerModel> customerList = Enumerable.Empty<CustomerModel>();

        try {
#pragma warning disable CS8600
            customerList = await client.GetFromJsonAsync<IEnumerable<CustomerModel>>("list");
#pragma warning restore CS8600
        } catch (Exception ex) {
            Exception _ex = ex;
        }

#pragma warning disable CS8603
        return customerList;
#pragma warning restore CS8603
    }
}
