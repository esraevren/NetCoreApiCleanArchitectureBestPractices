﻿using App.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(ServiceResult<T> result)
        {
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return new ObjectResult(null) { StatusCode = result.StatusCode.GetHashCode() };
            }

            return new ObjectResult(result) { StatusCode = result.StatusCode.GetHashCode() };
        }

        [NonAction]
        public IActionResult CreateActionResult(ServiceResult result)
        {
            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return new ObjectResult(null) { StatusCode = result.StatusCode.GetHashCode() };
            }

            return new ObjectResult(result) { StatusCode = result.StatusCode.GetHashCode() };
        }
    }
}
