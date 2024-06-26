﻿namespace DevHunter.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	using Infrastructure.Extensions;
	using Services.Data.Interfaces;
	using Services.Data.Models.JobOffer;
	using ViewModels.JobOffer;
	using ViewModels.JobApplication;

	using static Common.NotificationMessagesConstants;

	public class JobOfferController : Controller
	{
		private readonly IDocumentService documentService;
		private readonly IJobOfferService jobOfferService;
		private readonly IDevelopmentService devDevelopmentService;
		private readonly IJobApplicationService jobApplicationService;

		public JobOfferController(IJobOfferService jobOfferService, IJobApplicationService jobApplicationService, IDocumentService documentService, IDevelopmentService devDevelopmentService)
		{
			this.jobOfferService = jobOfferService;
			this.jobApplicationService = jobApplicationService;
			this.documentService = documentService;
			this.devDevelopmentService = devDevelopmentService;
		}

		[HttpGet]
		[Route("joboffer/all")]
		public async Task<IActionResult> All([FromQuery] AllJobOffersQueryModel queryModel, string development)
		{
			var developmentsExists = await this.devDevelopmentService.ExistsByIdAsync(development);

			if (!developmentsExists)
			{
				TempData[ErrorMessage] = "Development does not exist!";

				return RedirectToAction("Index","Home");
			}

			queryModel.Development = await this.devDevelopmentService.GetByIdAsync(development);

			queryModel.Filters = await this.jobOfferService.LoadFiltersAsync();

			AllJobOffersFilteredAndPagedServiceModel serviceModel =
				await jobOfferService.AllAsync(queryModel);

			queryModel.JobOffers = serviceModel.JobOffers;
			queryModel.TotalJobOffersCount = serviceModel.TotalJobOffersCount;

			return View(queryModel);
		}

		[HttpGet]
		[Route("joboffer/jobs")]
		public async Task<IActionResult> AllSearch([FromQuery] AllJobOffersQueryModel queryModel)
		{
			AllJobOffersFilteredAndPagedServiceModel serviceModel =
				await jobOfferService.AllBySearchAsync(queryModel);

			queryModel.JobOffers = serviceModel.JobOffers;
			queryModel.TotalJobOffersCount = serviceModel.TotalJobOffersCount;

			return View(queryModel);
		}

		[HttpPost]
		public async Task<IActionResult> Save(string id)
		{
			if (!User!.Identity!.IsAuthenticated)
			{
				return new JsonResult(new { success = false, errorMsg = "Please log in or register to save a job!" });
			}

			try
			{
				string userId = this.User.GetId()!;

				bool jobOfferExists = await this.jobOfferService.ExistsByIdAsync(id);

				if (!jobOfferExists)
				{
					return new JsonResult(new { success = false, errorMsg = "Job offer does not exist!" });
				}

				bool isJobOfferSaved = await this.jobOfferService.IsJobOfferSaved(id, this.User.GetId()!);

				if (isJobOfferSaved)
				{
					await this.jobOfferService.RemoveSaveJobAsync(id, userId);

					return new JsonResult(new { success = true, saved = true });
				}

				await this.jobOfferService.SaveJobAsync(id, userId);

				return new JsonResult(new { success = true });
			}
			catch (Exception)
			{
				return GeneralError();
			}
		}

		[HttpGet]
		[Route("job-offer/detail")]
		public async Task<IActionResult> Detail(string id)
		{
			try
			{
				bool jobOfferExists = await this.jobOfferService
					.ExistsByIdAsync(id);

				if (!jobOfferExists)
				{
					TempData[ErrorMessage] = "Job offer does not exist!";

					return RedirectToAction("All");
				}

				var model = await this.jobOfferService
					.GetDetailsByIdAsync(id);

				if (this.User.Identity!.IsAuthenticated)
				{
					model.ApplyModel = new JobApplicationFormModel()
					{
						Email = this.User.Identity.Name!,
					};
				}

				return View(model);
			}
			catch (Exception)
			{
				return GeneralError();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Apply(JobApplicationFormModel model, string id)
		{
			if (!ModelState.IsValid)
			{
				return new JsonResult(new { success = false, errorMsg = "There is an error in one or more fields" });
			}

			try
			{
				bool jobOfferExists = await this.jobOfferService.ExistsByIdAsync(id);

				if (!jobOfferExists)
				{
					TempData[ErrorMessage] = "Job offer with the provided id does not exist!";

					return RedirectToAction("All");
				}

				string? userId = "";

				if (this.User.Identity!.IsAuthenticated)
				{
					userId = this.User.GetId()!;
				}

				string applicationId = await this.jobApplicationService.ApplyJobOfferAsync(model, id, userId);

				if (model.Files.Count > 0)
				{
					foreach (var file in model.Files)
					{
						string documentUrl = await this.documentService.UploadDocumentAsync(file, "DevHunter/documents");

						await this.documentService.AddAsync(documentUrl, applicationId);
					}
				}

				return new JsonResult(new { success = true, successMsg = "Your application has been sent successfully!" });
			}
			catch (Exception)
			{
				return GeneralError();
			}
		}

		private IActionResult GeneralError()
		{
			TempData[ErrorMessage] = "Unexpected error occurred!";

			return RedirectToAction("Index", "Home");
		}
	}
}