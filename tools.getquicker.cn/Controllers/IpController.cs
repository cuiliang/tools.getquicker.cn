using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickerWebTools.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces(typeof(string))]
    public class IpController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;

        public IpController(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        /// <summary>
        /// 返回用户的公网IP地址
        /// </summary>
        /// <returns></returns>
        [HttpGet,HttpPost]
        public string MyIp()
        {
            return _accessor?.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "";
        }
    }
}
