using JobHive.Data;
using JobHive.Models;
using JobHive.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;

namespace JobHive.Tests
{
    public class JobPostingRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public JobPostingRepositoryTests()
        {
            // Configure in-memory database options
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "JobPostingDb")
                .Options;
        }

        // Helper method to create a new ApplicationDbContext instance
        private ApplicationDbContext CreateDbContext() => new ApplicationDbContext(_options);

        // Test for AddAsync method in JobPostingRepository
        [Fact]  // xUnit Fact attribute for test methods
        public async Task AddAsync_ShouldAddJobPosting()
        {
            // Arrange: Set up in-memory database and repository
            var db = CreateDbContext();
            var jobPostingRepository = new JobPostingRepository(db);

            // Create a new job posting
            var jobPosting = new JobPosting
            {
                Title = "Software Developer",
                Description = "Develop software",
                Location = "Lagos",
                Company = "Andela",
                UserId = "1",
                User = new IdentityUser { Id = "1", UserName = "testuser" } // Set required User property
            };

            // Act: Add the job posting to the repository
            await jobPostingRepository.AddAsync(jobPosting);

            // Assert: Verify that the job posting was added
            var result = await db.JobPostings.FirstOrDefaultAsync(j => j.Title == "Software Developer");
            Assert.NotNull(result);
            Assert.Equal(jobPosting.Title, result.Title);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnJobPosting()
        {
            // Arrange: Set up in-memory database and repository
            var db = CreateDbContext();
            var jobPostingRepository = new JobPostingRepository(db);

            // Create a new job posting
            var jobPosting = new JobPosting
            {
                Title = "Data Analyst",
                Description = "Analyze Data",
                Location = "Abuja",
                Company = "Andela",
                UserId = "2",
                User = new IdentityUser { Id = "2", UserName = "botuser" } // Set required User property
            };

            await db.JobPostings.AddAsync(jobPosting);
            await db.SaveChangesAsync();

            var result = await jobPostingRepository.GetByIdAsync(jobPosting.Id);

            Assert.NotNull(result);
            Assert.Equal(jobPosting.Title, result.Title);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowKeyNotFoundException()
        {
            // Arrange: Set up in-memory database and repository
            var db = CreateDbContext();
            var jobPostingRepository = new JobPostingRepository(db);

            // Act and Assert: Verify that a KeyNotFoundException is thrown
            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => jobPostingRepository.GetByIdAsync(999)
            );
        }
    }
}
