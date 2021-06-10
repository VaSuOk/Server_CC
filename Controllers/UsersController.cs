using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server_CC.DataContext;
using Server_CC.Models;

namespace Server_CC.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private UsersContext usersContext;
        public UsersController()
        {
            usersContext = new UsersContext();
        }
       // [HttpGet("{Login}/{Password}")]
        //public ActionResult<int> GetLoginStatus(string Login, string Password)
       // {
            //return usersContext.UserLogin(Login, Password);
       // }

        [HttpGet("{ID}")]
        public ActionResult<User> GetUserByID(int ID)
        {
            return usersContext.GetUserByID(ID);
        }


        [HttpPost]
        public async Task<ActionResult<User>> UpdateUser(User user)
        {
            usersContext.UpdateUser(user);
            return CreatedAtAction("GetUserByID", new { id = user.id }, user);
        }

        [HttpPut("{id}")]
        public void PutUserOrder(uint id, User user)
        {
            if (id != user.id)
            {
                return;
            }

            try
            {
                this.usersContext.CreateUser(user);
            }
            catch
            {
                return;
            }
        }
    }
}
