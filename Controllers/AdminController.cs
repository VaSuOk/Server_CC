using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server_CC.DataContext;

namespace Server_CC.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private AdminContext adminContext;
        public AdminController()
        {
            adminContext = new AdminContext();
        }
        [HttpGet("{Login}/{Password}")]
        public ActionResult<int> AdminLogin(string Login, string Password)
        {
            return adminContext.AdminLogin(Login, Password);
        }
    }
}
