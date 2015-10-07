using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThePortfolio.ViewModels
{
    public class TagViewModel
    {
        public int TagID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<PortfolioItemViewModel> PortfolioItems { get; set; }
    }
}