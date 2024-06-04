﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace THACO.Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public abstract class BaseApi : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("now")]
        public IActionResult GetNow()
        {
            return Ok(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        }
    }
}
