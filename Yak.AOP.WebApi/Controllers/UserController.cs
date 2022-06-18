using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yak.AOP.Service;

namespace Yak.AOP.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        //这里已经通过Autofac 注册过了，直接通过构造函数注入即可
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
 
        [HttpGet]
        [Route("AddUser")]
        public IActionResult AddUser(string name, int age)
        {
            //正常调用用户新增操作
            _userService.AddUser(new Model.User() {Name = name, Age= age });
            return Ok("Success!!");
        }
    }
}
