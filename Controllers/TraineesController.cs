using EjadaTraineesManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EjadaTraineesManagementSystem.Models;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;

namespace EjadaTraineesManagementSystem.Controllers
{
	public class TraineesController : Controller
	{
		private readonly ApplicationDbContext _context;
		public TraineesController(ApplicationDbContext context)
		{
			_context = context;
		}
		public IActionResult Trainees()
		{
			var Result = _context.Trainees.Include(x => x.Department)
			.Include(x => x.University)
			.OrderBy(x => x.TraineeName).ToList();
			return View(Result);
		}

		public IActionResult Create()
		{
			ViewBag.Departments = _context.Departments.OrderBy(x => x.DepartmentName).ToList();
			ViewBag.Universities = _context.Universities.OrderBy(x => x.UniversityName).ToList();

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Trainee model)
		{
			ViewBag.Departments = _context.Departments.OrderBy(x => x.DepartmentName).ToList();
			ViewBag.Universities = _context.Universities.OrderBy(x => x.UniversityName).ToList();

			if (ModelState.IsValid)
			{
				UploadImage(model);
				_context.Trainees.Add(model);
				_context.SaveChanges();
				return RedirectToAction(nameof(Trainees));
			}
			return View();
		}

		private void UploadImage(Trainee model)
		{
			var file = HttpContext.Request.Form.Files;
			if (file.Count > 0)
			{
				//upload image
				string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
				var fileStream = new FileStream(Path.Combine(@"wwwroot/", "Images", ImageName), FileMode.Create);
				file[0].CopyTo(fileStream);
				model.ImageUser = ImageName;
			}
			else if (model.ImageUser == null && model.TraineeId == null)
			{
				//not upload and new trainee 
				model.ImageUser = "default_avatar.png";
			}
			else
			{
				//edit
				model.ImageUser = model.ImageUser;
			}
		}

		public IActionResult Edit(int? Id)
		{
			ViewBag.Departments = _context.Departments.OrderBy(x => x.DepartmentName).ToList();
			ViewBag.Universities = _context.Universities.OrderBy(x => x.UniversityName).ToList();
			var Result = _context.Trainees.Find(Id);

			return View("Create", Result);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Trainee model)
		{
			var oldData = _context.Trainees.AsNoTracking().FirstOrDefault(x => x.TraineeId == model.TraineeId);

			UploadImage(model);

			if (model.ImageUser == null && oldData != null)
			{
				model.ImageUser = oldData.ImageUser;
			}

			if (ModelState.IsValid)
			{
				_context.Trainees.Update(model);
				_context.SaveChanges();
				return RedirectToAction(nameof(Trainees));
			}

			ViewBag.Departments = _context.Departments.OrderBy(x => x.DepartmentName).ToList();
			ViewBag.Universities = _context.Universities.OrderBy(x => x.UniversityName).ToList();

			return View(model);
		}

		public IActionResult Delete(int? Id)
		{
			var Result = _context.Trainees.Find(Id);
			if (Result != null)
			{
				_context.Trainees.Remove(Result);
				_context.SaveChanges();
			}

			return RedirectToAction(nameof(Trainees));
		}

		[HttpPost]

		public IActionResult SetLanguage(string culture, string returnUrl)
		{
			Response.Cookies.Append(
				CookieRequestCultureProvider.DefaultCookieName,
				CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
				new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
				);

			return LocalRedirect(returnUrl);
		}

	}

}