using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebFrontEnd.Models.Board
{
    public class SearchModel
    {
        [Display(Name = "Search")]
        [Required(ErrorMessage = "Please enter some search text")]
        public string SearchText { get; set; }
        public List<BoardModel> Skateboards { get; set; }
    }
}