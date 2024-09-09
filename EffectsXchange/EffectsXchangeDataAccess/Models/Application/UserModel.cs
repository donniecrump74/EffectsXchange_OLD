
namespace EffectsXchangeData.Models.Application;


public class UserModel {
    public long Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string MiddleName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
}
