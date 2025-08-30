using Microsoft.AspNetCore.Authorization;

namespace OpenProfileAPI.Web.Controllers.Base;

[Authorize(Policy = "basic-compliant")]
public class BasicCompliantController : AnonymousController
{
}
