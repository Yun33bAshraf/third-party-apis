﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ThirdPartyAPIs.Web.Controllers.Base;

//TODO: Check how can I apply cors policy with appsettings.json
[ApiController]
[EnableCors("CorsPolicy")]
public class AnonymousController : Controller
{
}
