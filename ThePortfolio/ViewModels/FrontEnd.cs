using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThePortfolio.Models;

namespace ThePortfolio.ViewModels
{
    public class FrontEnd
    {
        public User User { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Tag> Tags { get; set; }
        public List<PortfolioItem> portfolioItems { get; set; }
        public List<JournalItem> JournalItems { get; set; }
        public List<Service> Services { get; set; }
        public List<Slide> Slides { get; set; }
        public List<Value> Values { get; set; }
                                           //  public int MyProperty { get; set; }
    }
}