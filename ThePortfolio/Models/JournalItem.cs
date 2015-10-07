using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThePortfolio.Models
{
    public class JournalItem
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string ItemUrl { get; set; }
    }
}