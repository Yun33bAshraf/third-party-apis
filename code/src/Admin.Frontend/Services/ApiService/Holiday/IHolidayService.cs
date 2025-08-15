using IApply.Frontend.Models;
using IApply.Frontend.Models.PublicHoliday;

namespace IApply.Frontend.Services.ApiService.Holiday
{
    public interface IHolidayService
    {
        Task<ListingBaseResponse<Holidays>?> GetHolidays(HolidayGet request);
        Task<BaseResponse?> UpdateHoliday(HolidayCreate request);
        Task<BaseResponse?> CreateHoliday(HolidayCreate request);
    }
}
