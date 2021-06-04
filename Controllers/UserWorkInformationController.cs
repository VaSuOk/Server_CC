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
    }
}
