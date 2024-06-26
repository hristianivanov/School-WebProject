﻿namespace DevHunter.Web.ViewModels.Development
{
	using Technology;

	public class DevelopmentViewModel
	{
        public DevelopmentViewModel()
        {
            this.Technologies = new HashSet<TechnologyViewModel>();
        }

        public string Id { get; set; } = null!;

        public int Count { get; set; }

        public string Name { get; set; } = null!;

		public string ImageUrl { get; set; } = null!;

        public IEnumerable<TechnologyViewModel> Technologies { get; set; }
	}
}
