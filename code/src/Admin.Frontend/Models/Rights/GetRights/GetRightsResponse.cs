using IApply.Frontend.Models;
using IApply.Frontend.Models.Rights;

namespace IApply.Frontend.Models.Rights.GetRights
{
    public class GetRightsResponse : BaseResponse
    {
        public List<Right> Rights { get; set; }
    }
}
