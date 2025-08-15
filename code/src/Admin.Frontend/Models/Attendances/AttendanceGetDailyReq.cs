namespace IApply.Frontend.Models.Attendances
{
    public class AttendanceGetDailyReq
    { 
        public int? EmployeeId { get; set; }

        public DateOnly? StartDate {  get; set; }

        public DateOnly? EndDate { get; set; }
    }
}
