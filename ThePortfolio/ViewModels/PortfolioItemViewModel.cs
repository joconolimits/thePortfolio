using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace ThePortfolio.ViewModels
{
    public class PortfolioItemViewModel
    {
        public PortfolioItemViewModel()
        {
            Tags = new Collection<AssignedTags>();
        }

        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProjcetUrl { get; set; }
        public string Image { get; set; }
        public virtual ICollection<AssignedTags> Tags { get; set; }
    }
}