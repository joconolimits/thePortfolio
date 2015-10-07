using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ThePortfolio.Models
{
    public class PortfolioItem
    {
        public PortfolioItem()
        {
            Tags = new List<Tag>();
        }
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProjcetUrl { get; set; }
        public string Image { get; set; }
       // public IEnumerable<Tag> ItemTags { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}