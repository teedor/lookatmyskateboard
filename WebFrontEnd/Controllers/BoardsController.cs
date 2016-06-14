using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using WebFrontEnd.Models.Board;
using System.Data.Entity;
using WebFrontEnd.Models.Account;

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

        public ActionResult View(int id)
        {
            var model = new ViewBoardModel();
            using (var db = new lookatmyskateboardEntities())
            {
                var board = db.Skateboards.Include(x => x.User).Include(x => x.Comments).First(x => x.id == id);
                model.Board = new BoardModel
                {
                    Name = board.name,
                    Description = board.description,
                    ImageUrl = board.imageUrl,
                    UploadedBy = board.User.username
                };
                model.Comments = board.Comments.Select(x => new CommentModel { User = x.User.username, Text = x.Text }).ToList();
                model.NewComment = new CommentModel
                {
                    Text = string.Empty,
                    User = Session["User"] != null ? (Session["User"] as UserAccount).Username : string.Empty
                };
            }

            return View(model);
        }

        public ActionResult AddComment()
        {
            throw new NotImplementedException();
        }
    }
}