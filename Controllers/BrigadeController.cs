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
    public class BrigadeController : Controller
    {
        [HttpPut("{id}")]
        public void PutBrigade(int id, Brigade  brigade)
        {
            if (id != brigade.ID)
            {
                return;
            }

            try
            {
                BrigadeContext.CreateBrigade(brigade);
            }
            catch
            {
                return;
            }
        }

        [HttpGet("{Region}/{Stage}/{isWork}")]
        public ActionResult<List<Brigade>> GetBrigadeByFilter(string Region, string Stage, bool isWork)
        {
            return BrigadeContext.GetBrigade(Region, Stage, isWork);
        }

        [HttpGet("{Name}")]
        public ActionResult<List<Brigade>> GetBrigadeByName(string Name)
        {
            return BrigadeContext.GetBrigadeByName(Name);
        }

        [HttpPost]
        public int UpdateUserWorkInformation(Brigade brigade)
        {
            return BrigadeContext.UpdateBrigade(brigade);
        }

        [HttpDelete("{id}")]
        public int DeleteUserWI(int id)
        {
            return BrigadeContext.DeleteBrigade(id);
        }

    }
}
