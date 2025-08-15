using Microsoft.AspNetCore.Authorization;

namespace ThirdPartyAPIs.Web.Controllers.Base;

[Authorize(Policy = "full-compliant")]

public class FullCompliantController : BasicCompliantController
{
}
