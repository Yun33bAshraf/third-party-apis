using System;
using System.ComponentModel.DataAnnotations;

namespace OpenProfileAPI.Frontend.Models.Assets
{
    public class CreateAssetRequest
    {
        public int AssetId { get; set; }

        [Required(ErrorMessage = "Name is required")]
       // [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
       //[RegularExpression(@"^[\p{L}\p{N} _-]+$", ErrorMessage = "Invalid asset name.")]

        public string Name { get; set; } = string.Empty;

        public bool IsActive { get; set; } = false;
        public string CreatedAt { get; set; } = string.Empty;

        [Required(ErrorMessage = "Purchase Date is required")]
        [DataType(DataType.Date)]
        public DateTime? PurchaseDate { get; set; }

        [Required(ErrorMessage = "Please select a Category")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Category")]
        public int CategoryId { get; set; }

        //[Required(ErrorMessage = "Please select a Sub-Category")]
        //[Range(1, int.MaxValue, ErrorMessage = "Please select a valid Sub-Category")]
        public int? SubCategoryId { get; set; }

        [Required(ErrorMessage = "Please select a Status.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Status")]
        public int StatusId { get; set; } // enum: operational, maintenance, retired

        [Required(ErrorMessage = "Purchase price is required.")]
        [Range(1, double.MaxValue, ErrorMessage = "Enter a valid purchase price")]
        public decimal? PurchasePrice { get; set; }
        public string? AssetNumber { get; set; }
        public string? Comment { get; set; }

    }
}