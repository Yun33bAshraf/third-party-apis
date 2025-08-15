namespace IApply.Frontend.Models.User.GetUsers
{
    public class GetUsersRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int FilterBy { get; set; } = 0;
        public string? OrderBy { get; set; }
        public bool Ascending { get; set; } = false;
        public string SearchQuery { get; set; } = string.Empty;
    }
}
