using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Transfer.Shared.Models;

namespace Transfer.API.Controller.Base
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreateActionResult<T>(Response<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = (int?)response.StatusCode
            };
        }
    }
}
