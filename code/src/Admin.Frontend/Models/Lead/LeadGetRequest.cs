namespace IApply.Frontend.Models.Lead
{
    public class LeadGetRequest
    {
        public int LeadId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? CompanyName { get; set; } = string.Empty;
        public int LeadSourceId { get; set; }
        public int LeadStatusId { get; set; }
        public int IndustryId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
