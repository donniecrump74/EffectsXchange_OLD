
using Microsoft.EntityFrameworkCore;

namespace EffectsXchangeData.Models.Store; 

[Keyless]
public class ProductImageListModel {
    public long Id { get; set; }
    public long ImageId { get; set; }
    public string ImageName { get; set; } = default!;
    public string ImageExtension { get; set; } = default!;
    public long ImageSize { get; set; }
    public string Image { get; set; } = default!;
    public string Folder { get; set; } = default!;
    public bool IsFirst { get; set; } = false;
}
