
using System.ComponentModel.DataAnnotations;

namespace EffectsXchangeData.Models.Store; 

public class ProductDetailModel {
    [Key]
    public long Id { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Views { get; set; }
    public string ConditionDescription { get; set; } = default!;
    public string GradeDescription { get; set; } = default!;
    public string SKU { get; set; } = default!;
    public string ItemId { get; set; } = default!;
    public string UPC { get; set; } = default!;
}
