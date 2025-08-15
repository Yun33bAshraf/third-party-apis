using Microsoft.AspNetCore.Authorization;

namespace ThirdPartyAPIs.Web.Controllers.Base;

[Authorize(Policy = "basic-compliant")]
public class BasicCompliantController : AnonymousController
{
}
