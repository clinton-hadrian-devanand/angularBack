using angularAPI.Interfaces;
using angularAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace angularAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    [EnableCors]
    public class AuthController : ControllerBase
    {
        #region
        private readonly IAuth _authService;

        public AuthController(IAuth authService)
        {
            _authService = authService; 
        }

        #endregion

        [HttpPost]
        [ActionName("Auth")]
        public IActionResult Authenticate([FromBody] AuthModel auth)
        {
            if(auth == null)
            {
                return BadRequest();
            }

            DataTable result = _authService.Authenticate(auth);
            
            if(result.Rows.Count == 0)
            {
                return NoContent();

            }
            else
            {
                return Ok(new
                {
                    message = "it is hit",
                    data = JsonConvert.SerializeObject(result)
                }); ;
            }
            


        }

        [HttpPost]
        [ActionName("Register")]
        public IActionResult RegisterUser([FromBody] AuthModel auth)
        {
            if (auth == null)
            {
                return BadRequest();

            }

            DataTable result =_authService.RegisterUser(auth);

            if (result == null)
            {
                return BadRequest();
            }

            if (result.Rows.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(new
                {
                    message = "User Registered",
                    result = JsonConvert.SerializeObject(result)

                });
            }
        }

    }
}
