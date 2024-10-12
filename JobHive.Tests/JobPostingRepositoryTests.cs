using JobHive.Data;
using JobHive.Models;
using JobHive.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHive.Tests
{
    internal class JobPostingRepositoryTests
    {
        // Only a public class can be tested

        public readonly DbContextOptions<ApplicationDbContext> _options;

        public JobPostingRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("JobPostingDb")
                .Options;
        }

        // creating an instance of the ApplicationDbContext
        private ApplicationDbContext CreateDbContext() => new ApplicationDbContext(_options);

        // Test for Add JobPosting Async
        public async Task AddAsync_ShouldAddJobPosting()
        {
            // db context
            var db = CreateDbContext();
            // job posting repository
            var jobPostingRepository = new JobPostingRepository(db);
            // job posting

            var jobPosting = new JobPosting
            {
                Title = "Software Developer",
                Description = "Develop software",
                Location = "Lagos",
                Company = "Andela",
                UserId = "1",
                User = new IdentityUser { Id = "1", UserName = "testuser" } // Set the required User property
            };

            // expected job posting created
            await jobPostingRepository.AddAsync(jobPosting);
            // result job posting created

            var result = db.JobPostings.FirstOrDefault(j => j.Title == "Software Developer");
            // assert
            Assert.NotNull(result);
            Assert.Equal(jobPosting.Title, result.Title);
        }
    }
}
