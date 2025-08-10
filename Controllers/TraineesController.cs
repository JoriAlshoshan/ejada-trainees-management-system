using EjadaTraineesManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EjadaTraineesManagementSystem.Models;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace EjadaTraineesManagementSystem.Controllers
{
    public class TraineesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Users> _userManager;

        public TraineesController(ApplicationDbContext context, UserManager<Users> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

		

		[Authorize(Roles = "Admin")]
        public IActionResult Trainees()
        {
            var trainees = _context.Trainees
                .Include(t => t.Department)
                .Include(t => t.University)
                .Include(t => t.SupervisorTrainees)
                    .ThenInclude(st => st.SupervisorUser)
                .OrderBy(t => t.TraineeName)
                .ToList();

            return View(trainees);
        }

        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> TraineesOfSupervisor()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var trainees = await _context.SupervisorTrainees
                .Where(st => st.SupervisorId == currentUser.Id)
                .Include(st => st.Trainee)
                    .ThenInclude(t => t.Department)
                .Include(st => st.Trainee)
                    .ThenInclude(t => t.University)
                .Include(st => st.Trainee)
                    .ThenInclude(t => t.SupervisorTrainees)
                        .ThenInclude(st => st.SupervisorUser)
                .Select(st => st.Trainee)
                .OrderBy(t => t.TraineeName)
                .ToListAsync();

            return View(trainees);
        }

        private void PopulateDropdowns()
        {
            ViewBag.Departments = _context.Departments.OrderBy(d => d.DepartmentName).ToList();
            ViewBag.Universities = _context.Universities.OrderBy(u => u.UniversityName).ToList();
            ViewBag.Supervisors = _userManager.GetUsersInRoleAsync("Supervisor").Result
                   .Select(s => new
                   {
                       Id = s.Id,
                       fullName = s.fullName
                   }).ToList();
        }

        [HttpGet]
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View(new Trainee());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Trainee model)
        {
            PopulateDropdowns();


            if (!ModelState.IsValid)
                return View(model);


            UploadImage(model);

            _context.Trainees.Add(model);
            _context.SaveChanges();

            if (model.SupervisorIds != null)
            {
                foreach (var supervisorId in model.SupervisorIds)
                {
                    _context.SupervisorTrainees.Add(new SupervisorTrainee
                    {
                        SupervisorId = supervisorId,
                        TraineeId = model.TraineeId
                    });
                }
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Trainees));
        }




        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            PopulateDropdowns();

            var trainee = _context.Trainees
                .Include(t => t.SupervisorTrainees)
                .FirstOrDefault(t => t.TraineeId == id);

            if (trainee == null)
                return NotFound();

            trainee.SupervisorIds = trainee.SupervisorTrainees.Select(st => st.SupervisorId).ToList();

            return View("Create", trainee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Trainee model)
        {
            PopulateDropdowns();

            if (!ModelState.IsValid)
                return View("Create", model);

            var oldData = _context.Trainees.AsNoTracking().FirstOrDefault(t => t.TraineeId == model.TraineeId);

            UploadImage(model);

            if (string.IsNullOrEmpty(model.ImageUser) && oldData != null)
                model.ImageUser = oldData.ImageUser;

            _context.Trainees.Update(model);
            _context.SaveChanges();

            var existingRelations = _context.SupervisorTrainees.Where(st => st.TraineeId == model.TraineeId);
            _context.SupervisorTrainees.RemoveRange(existingRelations);
            _context.SaveChanges();

            if (model.SupervisorIds != null)
            {
                foreach (var supervisorId in model.SupervisorIds)
                {
                    _context.SupervisorTrainees.Add(new SupervisorTrainee
                    {
                        SupervisorId = supervisorId,
                        TraineeId = model.TraineeId
                    });
                }
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Trainees));
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var trainee = _context.Trainees.Find(id);
            if (trainee != null)
            {
                _context.Trainees.Remove(trainee);
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

        private void UploadImage(Trainee model)
        {
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                var file = files[0];
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var path = Path.Combine("wwwroot", "Images", imageName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                model.ImageUser = imageName;
            }
            else if (string.IsNullOrEmpty(model.ImageUser) && model.TraineeId == 0)
            {
                model.ImageUser = "default_avatar.png";
            }

        }
    }
}
