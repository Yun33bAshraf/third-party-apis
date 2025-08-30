using OpenProfileAPI.Frontend.Models;
using OpenProfileAPI.Frontend.Models.Rights;

namespace OpenProfileAPI.Frontend.Models.Rights.GetRights
{
    public class GetRightsResponse : BaseResponse
    {
        public List<Right> Rights { get; set; }
    }
}
