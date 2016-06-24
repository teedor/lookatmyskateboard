using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebFrontEnd.Models.Account;

namespace WebFrontEnd.Controllers
{
    public class ResetController : Controller
    {
        // GET: Reset
        public ActionResult Index()
        {
            if (Session["User"] != null && (Session["User"] as UserAccount).IsAdmin)
            {
                var cs = ConfigurationManager.ConnectionStrings["lookatmyskateboardConnection"].ConnectionString;
                using (var cn = new SqlConnection(cs))
                {
                    cn.Open();
                    var sql = @"delete from [like]
                                delete from [comment]
                                delete from [skateboard]
                                delete from [users]

                                SET IDENTITY_INSERT [dbo].[Users] ON 

                                INSERT [dbo].[Users] ([id], [username], [password], [isAdmin]) VALUES (1, N'admin', N'password', 1)
                                INSERT [dbo].[Users] ([id], [username], [password], [isAdmin]) VALUES (2, N'test01', N'password', 1)
                                SET IDENTITY_INSERT [dbo].[Users] OFF
                                SET IDENTITY_INSERT [dbo].[Skateboard] ON 

                                INSERT [dbo].[Skateboard] ([id], [name], [description], [imageUrl], [uploadedBy]) VALUES (2, N'Enuff Classic Skateboard Deck - Natural', N'These Enuff Classic decks and ideal for riders that don’t want lots of graphics on their setup, with only a basic print on the top ply.', N'001.jpg', 1)
                                INSERT [dbo].[Skateboard] ([id], [name], [description], [imageUrl], [uploadedBy]) VALUES (3, N'REVIVE LIFELINE SKATEBOARD DECK - TIE DYE', N'ReVive Skateboards! Formally ReVenge Skateboards, these cool cats have been supplying quality skateboards for a while now and we''re proud to have them on board!', N'002.jpg', 1)
                                INSERT [dbo].[Skateboard] ([id], [name], [description], [imageUrl], [uploadedBy]) VALUES (4, N'REVIVE SPLATTER SKATEBOARD DECK', N'Part of the awesome new range from the unstoppable guys at ReVive!', N'003.png', 1)
                                INSERT [dbo].[Skateboard] ([id], [name], [description], [imageUrl], [uploadedBy]) VALUES (5, N'ENUFF CLASSIC SKATEBOARD DECK - GREEN', N' Enuff Classic decks and ideal for riders that don’t want lots of graphics on their setup, with only a basic print on the top ply.', N'004.jpg', 1)
                                INSERT [dbo].[Skateboard] ([id], [name], [description], [imageUrl], [uploadedBy]) VALUES (6, N'ENUFF CLASSIC SKATEBOARD DECK - RED', N'Ideal for riders that don’t want lots of graphics on their setup, with only a basic print on the top ply.', N'005.jpg', 1)
                                INSERT [dbo].[Skateboard] ([id], [name], [description], [imageUrl], [uploadedBy]) VALUES (9, N'CLICHE SKATEBOARD DECK - HANDWRITTEN PSYCHE 8 inch', N'Part of the tasty new Cliche range!', N'006.jpg', 1)
                                SET IDENTITY_INSERT[dbo].[Skateboard] OFF";

                    using (var cmd = new SqlCommand(sql, cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                return Content("Reset complete");
            }

            return Content("Not authorised");
        }
    }
}