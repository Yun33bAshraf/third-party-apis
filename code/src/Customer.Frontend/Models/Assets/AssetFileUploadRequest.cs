using Microsoft.AspNetCore.Components.Forms;

namespace OpenProfileAPI.Frontend.Models.Assets;

public class AssetFileUploadRequest
{
    public int AssetId { get; init; }
    public List<AssetFileModel>? AssetFile { get; set; } = [];

}

public class AssetFileModel
{
    public int CategoryId { get; init; }
    public int? SubCategoryId { get; init; }
    public IBrowserFile Files { get; set; }
}

