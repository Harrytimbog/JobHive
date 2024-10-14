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
        public IActionResult Index()
        {
            return View();
        }
    }
}