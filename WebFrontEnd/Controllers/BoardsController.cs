using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using WebFrontEnd.Models.Board;
using System.Data.Entity;
using System.Data.SqlClient;
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

        public ActionResult Search()
        {
            var model = new SearchModel
            {
                SearchText = string.Empty,
                Skateboards = null
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Search(SearchModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var cs = "data source=.;initial catalog=lookatmyskateboard;integrated security=True;";
            using (var cn = new SqlConnection(cs))
            {
                model.Skateboards = new List<BoardModel>();

                cn.Open();
                var sql = "SELECT Name, Description, Id FROM Skateboard WHERE Name Like '%" + model.SearchText +
                          "%' OR Description Like '%" + model.SearchText + "%'";
                using (var cmd = new SqlCommand(sql, cn))
                {
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.Skateboards.Add(new BoardModel
                            {
                                Name = reader.GetString(0),
                                Description = reader.GetString(1),
                                Id = reader.GetInt32(2)
                            });
                        }
                    }
                }
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
                    Id = board.id,
                    Name = board.name,
                    Description = board.description,
                    ImageUrl = board.imageUrl,
                    UploadedBy = board.User.username
                };
                model.Comments = board.Comments.Select(x => new CommentModel { User = x.User.username, Text = x.Text }).ToList();
                model.NewComment = new CommentModel
                {
                    Text = string.Empty,
                    UserId = Session["User"] != null ? (Session["User"] as UserAccount).UserId : new int?(),
                    User = Session["User"] != null ? (Session["User"] as UserAccount).Username : string.Empty
                };
                if (Session["User"] != null)
                {
                    model.Liked = board.Likes.Contains(db.Users.Find((Session["User"] as UserAccount).UserId));
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddComment(ViewBoardModel model)
        {
            var cs = "data source=.;initial catalog=lookatmyskateboard;integrated security=True;";
            using (var cn = new SqlConnection(cs))
            {
                cn.Open();
                var sql = "INSERT INTO Comment (skateboardid, userid, text) VALUES (" + model.Board.Id + ", " +
                          model.NewComment.UserId.Value + ", '" + model.NewComment.Text.Replace("'", "''") + "');";

                using (var cmd = new SqlCommand(sql, cn))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToAction("View", new { id = model.Board.Id });
        }

        public ActionResult Like(int boardId, int userId)
        {
            using (var db = new lookatmyskateboardEntities())
            {
                db.Users.Find(userId).Likes.Add(db.Skateboards.Find(boardId));
                db.SaveChanges();
            }

            return RedirectToAction("View", new {id = boardId});
        }
    }
}