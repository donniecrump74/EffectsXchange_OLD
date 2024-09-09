using System.ComponentModel.DataAnnotations.Schema;

namespace EffectsXchangeData.Models.Customer;

public class CustomerModel {
    public long Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = default!;
    public DateTime CreatedDate { get; set; } = default!;
    public long CreatedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public long? ModifiedBy { get; set; }
    public Int16 Status { get; set; }



    [NotMapped]
    public List<AddressModel> Addresses { get; set; } = new List<AddressModel>();
    [NotMapped]
    public List<EmailAddressModel> EmailAddresses { get; set; } = new List<EmailAddressModel>();
}
