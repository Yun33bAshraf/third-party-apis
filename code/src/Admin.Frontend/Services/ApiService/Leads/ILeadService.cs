using IApply.Frontend.Models;
using IApply.Frontend.Models.Lead;

namespace IApply.Frontend.Services.ApiService.Leads
{
    public interface ILeadService
    {
        Task<ListingBaseResponse<Lead>?> GetLead(LeadGetRequest request);
        Task<BaseResponse?> UpdateLead(LeadCreateRequest request);
        Task<BaseResponse?> CreateLead(LeadCreateRequest request);

    }
}
