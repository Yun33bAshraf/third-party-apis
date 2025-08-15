using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IApply.Frontend.Common.CustomAttributes;
using IApply.Frontend.Common.Utilities;
using IApply.Frontend.Models.Employee;

namespace IApply.Frontend.Models.Assets;

public class Assets
{
    [Display(Name = "Id")]
    [ColumnWidth("w-1")]
    public int AssetId { get; set; }

    [Display(Name = "Asset Name")]
    public string? Name { get; set; }

    [IgnoreInTable]
    public DateTime? PurchaseDate { get; set; }

    [IgnoreInTable]
    public int CategoryId { get; set; }

    [Display(Name = "Category")]
    public string Category { get; set; } = string.Empty;
    [IgnoreInTable]
    public int? SubCategoryId { get; set; } = 0;
    [Display(Name = "Sub-Category")]
    public string SubCategory { get; set; } = string.Empty;
    [IgnoreInTable]
    public int StatusId { get; set; } // enum: operational, maintenance, retired

    [Display(Name = "Status")]
    public string Status { get; set; } = string.Empty;

    [Display(Name = "Purchase Price")]
    public decimal? PurchasePrice { get; set; }

    [IgnoreInTable]
    public string? AssetNumber { get; set; }

    [IgnoreInTable]
    public string? Comment { get; set; }

    [Display(Name = "Created Date")]
    public string CreatedAt { get; set; }
    [IgnoreInTable]

    public string? CreatedBy { get; set; } = string.Empty;
    [IgnoreInTable]

    public string? UpdatedBy { get; set; } = string.Empty;
    [IgnoreInTable]

    public string? UpdatedDate { get; set; }

    [IgnoreInTable]
    public List<AssetFileGet> Files { get; set; } = [];
    //public string? FileUrl { get; set; }

    // [Display(Name = "Created At")]
    //  public string? CreatedAtFormatted { get; set; } = string.Empty;

}

public class AssetFileGet
{
    public int AssetFileId { get; set; }
    public int FileStoreId { get; set; }
    public int CategoryId { get; set; }
    public string FileUrl { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string UploadedBy { get; set; } = string.Empty;
    public string? UploadedDate { get; set; } = string.Empty;
}



