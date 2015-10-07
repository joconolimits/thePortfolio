using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThePortfolio.Models;

namespace ThePortfolio.ViewModels
{
    public static class ViewModelHelpers
    {
        public static PortfolioItemViewModel ToViewModel(this PortfolioItem portfolioItem)
        {
            var PortfolioItemViewModel = new PortfolioItemViewModel
            {
                Title = portfolioItem.Title,
                ID = portfolioItem.ID,
                Description = portfolioItem.Description,
                ProjcetUrl = portfolioItem.ProjcetUrl,
                Image = portfolioItem.Image
            };

            foreach (var tag in portfolioItem.Tags)
            {
                PortfolioItemViewModel.Tags.Add(new AssignedTags
                {
                    ID = tag.ID,
                    Name = tag.Name,
                    Assigned = true
                });
            }

            return PortfolioItemViewModel;
        }

        public static PortfolioItemViewModel ToViewModel(this PortfolioItem portfolioItem, ICollection<Tag> allDbTags)
        {
            var userProfileViewModel = new PortfolioItemViewModel
            {
                Title = portfolioItem.Title,
                ID = portfolioItem.ID,
                Description = portfolioItem.Description,
                ProjcetUrl = portfolioItem.ProjcetUrl,
                Image = portfolioItem.Image
            };

            // Collection for full list of tags with portfolioItems's already assigned tags included
            ICollection<AssignedTags> allTags = new List<AssignedTags>();

            foreach (var t in allDbTags)
            {
                // Create new AssignedTag for each tagrse and set Assigned = true if portfolioItem already has tag
                var assignedTags = new AssignedTags
                {
                    ID = t.ID,
                    Name = t.Name,
                    Assigned = portfolioItem.Tags.FirstOrDefault(x => x.ID == t.ID) != null
                };

                allTags.Add(assignedTags);
            }

            userProfileViewModel.Tags = allTags;

            return userProfileViewModel;
        }

        public static PortfolioItem ToDomainModel(this PortfolioItemViewModel portfolioItemViewModel)
        {
            var portfolioItem = new PortfolioItem();
            portfolioItem.Title = portfolioItemViewModel.Title;
            portfolioItem.ID = portfolioItemViewModel.ID;
            portfolioItem.ProjcetUrl = portfolioItemViewModel.ProjcetUrl;
            portfolioItem.Image = portfolioItemViewModel.Image;

            return portfolioItem;
        }
    }
}