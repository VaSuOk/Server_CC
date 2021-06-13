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
    public class ConstructionObjectController : Controller
    {
        public ConstructionObjectContext constructionObjectContext = new ConstructionObjectContext();
        [HttpPut("{id}")]
        public void PutQuestionnaire(uint id, ConstructionObject constructionObject)
        {
            try
            {
                this.constructionObjectContext.CreateConstructionObject(constructionObject);
            }
            catch
            {
                return;
            }
        }
        [HttpGet("{Region}/{Sity}/{Type}")]
        public ActionResult<List<ConstructionObject>> GetUserByStageAndPosition(string Region, string Sity, string Type)
        {
            return constructionObjectContext.GetAllBObject(Region, Sity, Type);
        }
    }
}
