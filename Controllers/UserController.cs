using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstOne.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        Dictionary<string, string> _users = new Dictionary<string, string> 
        {
            { "Caio", "123456" },
            { "Gabriel", "654321" },
            { "Leonardo", "147852" }
        };
        [HttpGet("GetUsers")]
        public IActionResult UserList()
        {
            return Ok(_users);
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult AddUser(string key, string value)
        {
            _users.Add(key, value);
            return Ok("Usuário adicionado com sucesso!!");
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult SignIn(string key, string value)
        {
            var getUser = _users[key] == value;
            if (!getUser)
            {
               return BadRequest("Nem tem esse ó!!!");
            }
            return Ok("Usuário logado com sucesso!!");
        }
    }
}
