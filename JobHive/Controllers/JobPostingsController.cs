using JobHive.Models;
using JobHive.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobHive.Controllers
{
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
        public async Task<IActionResult> Index()
        {
            var jobPostings = await _jobPostingRepository.GetAllAsync();
            return View(jobPostings);
        }

        // Create Action
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(JobPosting jobPosting)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}