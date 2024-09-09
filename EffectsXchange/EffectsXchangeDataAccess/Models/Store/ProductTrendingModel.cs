using System.ComponentModel.DataAnnotations;

namespace EffectsXchangeData.Models.Store;

public class ProductTrendingModel {
    [Key]
    public long Id { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public long ImageId { get; set; }
    public string ImageName { get; set; } = default!;
    public string ImageExtension { get; set; } = default!;
    public long ImageSize { get; set; }
    public string Image { get; set; } = default!;
    public int Views { get; set; }
    public string Folder { get; set; } = default!;
    public string ConditionDescription { get; set; } = default!;
    public string GradeDescription { get; set; } = default!;
}
