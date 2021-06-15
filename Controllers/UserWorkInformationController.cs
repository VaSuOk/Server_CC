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
    public class UserWorkInformationController : Controller
    {
        private UserWorkInformationContext userWorkInformationContext;
        public UserWorkInformationController()
        {
            userWorkInformationContext = new UserWorkInformationContext();
        }

        [HttpGet("{Region}/{Stage}/{Position}")]
        public ActionResult<List<UserWorkInformation>> GetUserByStageAndPosition( string Region, string Stage, string Position)
        {
            return userWorkInformationContext.GetUserWIByStageAndPosition(Region, Stage, Position);
        }

        [HttpGet("{Name}/{Surname}")]
        public ActionResult<List<UserWorkInformation>> GetUserByInitial(string Name, string Surname)
        {
            return userWorkInformationContext.GetUserWIByNameAndSurname(Name, Surname);
        }

        [HttpGet("{ID}")]
        public UserWorkInformation GetUserByID(int ID)
        {
            return userWorkInformationContext.GetUserWIByID(ID);
        }

        [HttpGet]
        public ActionResult<List<UserWorkInformation>> GetQuestionnairesByID()
        {
            return userWorkInformationContext.GetAllUserWI();
        }

        [HttpPut("{id}")]
        public void PutQuestionnaire(uint id, UserWorkInformation userWorkInformation)
        {
            try
            {
                userWorkInformationContext.InsertUserWorkInformation(userWorkInformation);
            }
            catch
            {
                return;
            }
        }

        [HttpPost]
        public int UpdateUserWorkInformation(UserWorkInformation userWorkInformation)
        {
            return userWorkInformationContext.UpdateUserWI(userWorkInformation);
        }

        [HttpDelete("{id}")]
        public int DeleteUserWI(int id)
        {
            return userWorkInformationContext.DeleteUserWI(id);
        }
    }
}
