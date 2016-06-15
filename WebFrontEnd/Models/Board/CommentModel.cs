using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFrontEnd.Models.Board
{
    public class CommentModel
    {
        public string Text { get; set; }
        public string User { get; set; }
        public int? UserId { get; set; }
    }
}