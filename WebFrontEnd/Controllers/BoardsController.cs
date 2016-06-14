using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using WebFrontEnd.Models.Board;

namespace WebFrontEnd.Controllers
{
    public class BoardsController : Controller
    {
        // GET: Boards
        public ActionResult Index()
        {
            var model = new List<BoardModel>();
            using (var db = new lookatmyskateboardEntities())
            {
                model.AddRange(db.Skateboards.Select(skateboard => new BoardModel
                {
                    Id = skateboard.id, Name = skateboard.name, ImageUrl = skateboard.imageUrl, Description = skateboard.description, UploadedBy = skateboard.User.username
                }));
            }

            return View(model);
        }

        public ActionResult View()
        {
            throw new NotImplementedException();
        }
    }
}