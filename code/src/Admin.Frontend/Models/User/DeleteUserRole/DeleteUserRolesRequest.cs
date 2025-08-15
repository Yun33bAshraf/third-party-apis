namespace IApply.Frontend.Models.User.DeleteUserRole
{
    public class DeleteUserRolesRequest
    {
        public Guid RecordId { get; set; }
        public List<Guid> Roles { get; set; }
    }
}
