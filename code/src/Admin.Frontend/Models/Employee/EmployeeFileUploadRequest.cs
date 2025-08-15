using Microsoft.AspNetCore.Components.Forms;

namespace IApply.Frontend.Models.Employee;

public class EmployeeFileUploadRequest
{
    public int EmployeeId { get; set; }
    public List<EmployeeFileModel>? EmployeeFile { get; set; } = [];
}

public class EmployeeFileModel
{
    public int CategoryId { get; set; }
    public int? SubCategoryId { get; set; }
    public IBrowserFile Files { get; set; }

}
