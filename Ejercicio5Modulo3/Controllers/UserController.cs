using Ejercicio5Modulo3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ejercicio5Modulo3.Controllers
{
    [Route("api/v1/usuarios/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService UserService;

        public UserController(IUserService UserService)
        {
            this.UserService = UserService;
        }

        [HttpGet]
        public async Task<ActionResult> FindUsersUsingFilter([FromQuery] String? LastName, String? Email)
        {
            var result = await UserService.GetUsersUsingFilter(LastName, Email);
            return Ok(result);
        }

        [HttpPost]
        [Route("seed")]
        public async Task<ActionResult> LoadUsersInDB()
        {
            await UserService.LoadUsersInDB();
            return Ok();
        }
    }
}
