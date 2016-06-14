using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebFrontEnd.Models.Board
{
    public class ViewBoardModel
    {
        public int Id { get; set; }

        public BoardModel Board { get; set; }

        public List<CommentModel> Comments { get; set; }

        [Required(ErrorMessage = "The comment text is empty")]
        public CommentModel NewComment { get; set; }
    }
}