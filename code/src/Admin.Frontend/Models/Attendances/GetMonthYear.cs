using System.Globalization;

namespace IApply.Frontend.Models.Attendances;

public class GetMonthYear
{
    public string? MonthYear { get; set; }
    public DateTime? MonthYearDateTime
    {
        get
        {
            if (!string.IsNullOrWhiteSpace(MonthYear))
            {
                if (DateTime.TryParseExact(MonthYear, "MMMM yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
                {
                    return parsedDate;
                }
            }
            return null;
        }
    }
}
