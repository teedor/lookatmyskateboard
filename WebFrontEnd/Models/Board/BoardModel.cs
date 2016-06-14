using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFrontEnd.Models.Board
{
    public class BoardModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UploadedBy { get; set; }
        public string ImageUrl { get; set; }
    }
}