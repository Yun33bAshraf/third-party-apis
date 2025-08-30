using Microsoft.AspNetCore.Authorization;

namespace OpenProfileAPI.Web.Controllers.Base;

[Authorize(Policy = "full-compliant")]

public class FullCompliantController : BasicCompliantController
{
}
