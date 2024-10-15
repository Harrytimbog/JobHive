using JobHive.Models;
using JobHive.Repositories;
using JobHive.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobHive.Controllers
{
    [Authorize]
    public class JobPostingsController : Controller
    {

        private readonly IRepository<JobPosting> _jobPostingRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public JobPostingsController(
            IRepository<JobPosting> jobRepository,
            UserManager<IdentityUser> userManager)
        {
            _jobPostingRepository = jobRepository;
            _userManager = userManager;
        }

        // Index Action
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var jobPostings = await _jobPostingRepository.GetAllAsync();
            return View(jobPostings);
        }

        // Create Action
        [Authorize(Roles ="Admin, Employer")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(JobPostingViewModel jobPostingVm)
        {
            if (ModelState.IsValid)
            {
                // Get the current user
                var userId = _userManager.GetUserId(User);
                var user = await _userManager.FindByIdAsync(userId);

                var jobPosting = new JobPosting
                {
                    Title = jobPostingVm.Title,
                    Description = jobPostingVm.Description,
                    Company = jobPostingVm.Company,
                    Location = jobPostingVm.Location,
                    UserId = userId,
                    User = user  // Assign the User property here
                }; 

                await _jobPostingRepository.AddAsync(jobPosting);

                return RedirectToAction(nameof(Index));
            }

            return View(jobPostingVm); // If the model state is invalid, return the view with the model
        }

        [HttpDelete]
        [Authorize(Roles = "Admin, Employer")]
        public async Task<IActionResult> Delete(int id)
        {
            await _jobPostingRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}