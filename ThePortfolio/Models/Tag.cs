using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThePortfolio.Models
{
    public class Tag
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PortfolioItem> PortfolioItems { get; set; }
    }
}