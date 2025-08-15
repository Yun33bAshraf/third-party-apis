namespace IApply.Frontend.Models.AuthPolicies.GetAuthPolicies;

public class AuthPolicyGetRequest
{
    public int AuthPolicyId { get; set; }
    public int UserTypeId { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
