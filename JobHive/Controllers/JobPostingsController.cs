using JobHive.Models;
using JobHive.Repositories;
using JobHive.ViewModels;
using JobHive.Constants;
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
            var allJobPostings = await _jobPostingRepository.GetAllAsync();
            
            // Filter job postings for employers
            if(User.IsInRole(Roles.Employer))
            {
                var userId = _userManager.GetUserId(User);
                var filteredJobPostings = allJobPostings.Where(jp => jp.UserId == userId );
                return View(filteredJobPostings);
            }

            return View(allJobPostings);
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

        // JobPosting/Delete/5
        [HttpDelete]
        [Authorize(Roles = "Admin, Employer")]
        public async Task<IActionResult> Delete(int id)
        {
            // find job posting
            var jobPosting = await _jobPostingRepository.GetByIdAsync(id);

            if(jobPosting == null)
            {
                return NotFound();
            }

            // find job posting creator
            var userId = _userManager.GetUserId(User);

            // check if the user is the creator of the job posting or an admin
            if (User.IsInRole(Roles.Admin) == false && jobPosting.UserId != userId)
            {
                return Forbid();
            }

            // delete the job posting
            await _jobPostingRepository.DeleteAsync(id);
            return Ok();
        }

    }
}