

namespace EffectsXchangeData.Models.Application;
public class FileUploadModel {
    public string Name { get; set; } = default!;
    public long Size { get; set; }
    public byte[] File { get; set; } = default!;
}
