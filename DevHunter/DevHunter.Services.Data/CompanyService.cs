﻿namespace DevHunter.Services.Data
{
	using System.Globalization;

	using Ganss.Xss;
	using Microsoft.EntityFrameworkCore;

	using DevHunter.Data;
	using DevHunter.Data.Models;

	using Interfaces;
	using Web.ViewModels.User;
	using Web.ViewModels.JobOffer;
	using Web.ViewModels.Technology;
	using Web.ViewModels.Company;

	using static Common.GeneralApplicationConstants.JobOffer;

	public class CompanyService : ICompanyService
	{
		private readonly DevHunterDbContext context;
		private readonly IImageService imageService;

		public CompanyService(DevHunterDbContext context, IImageService imageService)
		{
			this.context = context;
			this.imageService = imageService;
		}

		public async Task AddAsync(CompanyRegisterFormModel model, Guid userId)
		{
			var company = new Company()
			{
				Name = model.Name,
				CreatorId = userId,
				WebsiteUrl = model.WebsiteUrl,
				ImageUrl = await this.imageService.UploadImage(model.Image, "DevHunter/companies", model.Name),
			};

			await context.Companies.AddAsync(company);
			await this.context.SaveChangesAsync();
		}

		public async Task<bool> ExistsByNameAsync(string name)
			=> await this.context.Companies.FirstOrDefaultAsync(c => c.Name == name) != null;

		public async Task<bool> ExistsByIdAsync(string id)
			=> await this.context.Companies.FirstOrDefaultAsync(c => c.Id.ToString() == id) != null;

		public async Task<CompanyFormModel> GetForEditByIdAsync(string id)
		{
			var company = await this.context
				.Companies
				.FirstAsync(t => t.Id.ToString() == id);

			return new CompanyFormModel
			{
				Name = company.Name,
				ImageUrl = company.ImageUrl!,
				Description = company.Description,
				Activity = company.Activity,
				WebsiteUrl = company.WebsiteUrl,
				Sector = company.Sector,
				Address = company.Location,
				EmployeesCnt = company.EmployeeCount,
				FoundedDate = company.FoundedDate,
			};
		}

		public async Task EditAsync(string id, CompanyFormModel model)
		{
			var company = await this.context.Companies
				.FirstAsync(c => c.Id.ToString() == id);

			var sanitizer = new HtmlSanitizer();

			bool hasChanges = false;

			if (company.Name != model.Name)
			{
				company.Name = model.Name;
				hasChanges = true;
			}
			if (company.Description != model.Description)
			{
				var sanitized = sanitizer.Sanitize(model.Description!);

				company.Description = sanitized;
				hasChanges = true;
			}
			if (company.Activity != model.Activity)
			{
				company.Activity = model.Activity;
				hasChanges = true;
			}
			if (company.WebsiteUrl != model.WebsiteUrl)
			{
				company.WebsiteUrl = model.WebsiteUrl;
				hasChanges = true;
			}
			if (company.Sector != model.Sector)
			{
				company.Sector = model.Sector;
				hasChanges = true;
			}
			if (company.Location != model.Address)
			{
				company.Location = model.Address;
				hasChanges = true;
			}
			if (company.EmployeeCount != model.EmployeesCnt)
			{
				company.EmployeeCount = model.EmployeesCnt;
				hasChanges = true;
			}
			if (company.FoundedDate != model.FoundedDate)
			{
				company.FoundedDate = model.FoundedDate;
				hasChanges = true;
			}

			if (hasChanges)
				await this.context.SaveChangesAsync();
		}

		public async Task<string?> GetCompanyIdByCreatorIdAsync(string userId)
		{
			var company = await this.context
				.Companies
				.FirstOrDefaultAsync(c => c.CreatorId.ToString() == userId);

			return company.Id.ToString();
		}

		public async Task<IEnumerable<CompanyAdminViewModel>> AllForAdminAsync()
		{
			var companies = await this.context
				.Companies
				.Select(c => new CompanyAdminViewModel()
				{
					Id = c.Id.ToString(),
					Name = c.Name,
					CEO = c.Creator.UserName, // TODO: email
					OffersCnt = c.JobOffers.Count()
				})
				.ToListAsync();

			return companies;
		}

		public async Task<IEnumerable<CompanyAllViewModel>> AllAsync()
		{
			var companies = await this.context
				.Companies
				.Select(c => new CompanyAllViewModel()
				{
					Id = c.Id.ToString(),
					JobOffersCount = c.JobOffers.Count(),
					EmployeesCount = c.EmployeeCount ?? 0,
					ImageUrl = c.ImageUrl!,
					Name = c.Name,
				})
				.AsNoTracking()
				.ToListAsync();

			return companies;
		}


		public async Task<CompanyDetailViewModel> GetDetailsByIdAsync(string id)
		{
			var company = await this.context
				.Companies
				.FirstAsync(c => c.Id.ToString() == id);

			var jobOffers = company
				.JobOffers
				.Select(j => new JobOfferAllViewModel()
				{
					Id = j.Id.ToString(),
					CompanyName = company.Name,
					CompanyImageUrl = company.ImageUrl!,
					CreatedOn = j.CreatedOn.ToString(CreatedOnDateFormat, CultureInfo.InvariantCulture),
					JobLocation = j.PlaceToWork,
					JobPosition = j.JobPosition,
					Salary = GetSalary(j.MinSalary, j.MaxSalary),
					Technologies = j.JobOfferTechnologies.Select(tj => new TechnologyViewModel()
					{
						Id = tj.TechnologyId.ToString(),
						ImageUrl = tj.Technology.ImageUrl!,
						Name = tj.Technology.Name,
					}),
				})
				.ToList();

			var companyTechnologies = new List<TechnologyViewModel>();

			foreach (var companyJobOffer in company.JobOffers)
			{
				foreach (var technology in companyJobOffer.JobOfferTechnologies)
				{
					companyTechnologies.Add(new TechnologyViewModel()
						{

							Id = technology.TechnologyId.ToString(),
							Name = technology.Technology.Name,
							ImageUrl = technology.Technology.ImageUrl!,
						}
					);
				}
			}

			companyTechnologies = companyTechnologies.DistinctBy(x => x.Id).ToList();

			var model = new CompanyDetailViewModel
			{
				Id = company.Id.ToString(),
				Name = company.Name,
				ImageUrl = company.ImageUrl!,
				WebsiteUrl = company.WebsiteUrl,
				JobOffers = jobOffers,
				Technologies = companyTechnologies
			};

			if (!string.IsNullOrWhiteSpace(company.Sector))
			{
				model.Sector = company.Sector;
			}
			if (!string.IsNullOrWhiteSpace(company.Activity))
			{
				model.Activity = company.Activity;
			}
			if (!string.IsNullOrWhiteSpace(company.Location))
			{
				model.Location = company.Location;
			}
			if (!string.IsNullOrWhiteSpace(company.Description))
			{
				model.Description = company.Description;
			}
			if (company.FoundedDate.HasValue)
			{
				model.FoundedYear = company.FoundedDate.Value.Year;
			}
			if (company.EmployeeCount.HasValue)
			{
				model.EmployeesCnt = company.EmployeeCount.Value;
			}

			return model;
		}



		private static string GetSalary(decimal? minSalary, decimal? maxSalary)
		{
			if (minSalary == null && maxSalary == null)
			{
				return string.Empty;
			}

			string? formattedMaxSalary = maxSalary?
				.ToString("#,0", CultureInfo.InvariantCulture)
				.Replace(",", " ");
			string? formattedMinSalary = minSalary?
				.ToString("#,0", CultureInfo.InvariantCulture)
				.Replace(",", " ");

			if (!string.IsNullOrWhiteSpace(formattedMinSalary))
			{
				return $"{formattedMinSalary} - {formattedMaxSalary} lv.";
			}

			return $"{formattedMaxSalary} lv.";
		}
	}
}
