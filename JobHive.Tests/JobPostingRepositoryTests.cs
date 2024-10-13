using JobHive.Data;
using JobHive.Models;
using JobHive.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;

namespace JobHive.Tests
{
    public class JobPostingRepositoryTests : IAsyncLifetime
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private ApplicationDbContext _db;

        public JobPostingRepositoryTests()
        {
            // Configure in-memory database options with a unique database name
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())  // Unique DB for each test class
                .Options;
        }

        // Helper method to create a new ApplicationDbContext instance
        private ApplicationDbContext CreateDbContext() => new ApplicationDbContext(_options);

        // Initialize DB Context before each test
        public async Task InitializeAsync()
        {
            _db = CreateDbContext();
        }

        // Dispose DB Context after each test
        public async Task DisposeAsync()
        {
            await _db.Database.EnsureDeletedAsync();  // Clean up after tests
            await _db.DisposeAsync();
        }

        [Fact]
        public async Task AddAsync_ShouldAddJobPosting()
        {
            // Arrange: Set up repository
            var jobPostingRepository = new JobPostingRepository(_db);

            // Create a new job posting
            var jobPosting = new JobPosting
            {
                Title = "Software Developer",
                Description = "Develop software",
                Location = "Lagos",
                Company = "Andela",
                UserId = "1",
                User = new IdentityUser { Id = "1", UserName = "testuser" }
            };

            // Act: Add the job posting to the repository
            await jobPostingRepository.AddAsync(jobPosting);

            // Assert: Verify that the job posting was added
            var result = await _db.JobPostings.FirstOrDefaultAsync(j => j.Title == "Software Developer");
            Assert.NotNull(result);
            Assert.Equal(jobPosting.Title, result.Title);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnJobPosting()
        {
            // Arrange: Set up repository and add a job posting
            var jobPostingRepository = new JobPostingRepository(_db);
            var jobPosting = new JobPosting
            {
                Title = "Data Analyst",
                Description = "Analyze Data",
                Location = "Abuja",
                Company = "Andela",
                UserId = "2",
                User = new IdentityUser { Id = "2", UserName = "botuser" }
            };

            await _db.JobPostings.AddAsync(jobPosting);
            await _db.SaveChangesAsync();

            // Act: Retrieve the job posting by ID
            var result = await jobPostingRepository.GetByIdAsync(jobPosting.Id);

            // Assert: Verify that the correct job posting was returned
            Assert.NotNull(result);
            Assert.Equal(jobPosting.Title, result.Title);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowKeyNotFoundException()
        {
            // Arrange: Set up repository
            var jobPostingRepository = new JobPostingRepository(_db);

            // Act and Assert: Verify that a KeyNotFoundException is thrown
            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => jobPostingRepository.GetByIdAsync(999)
            );
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllJobPostings()
        {
            // Arrange: Set up repository and add multiple job postings
            var jobPostingRepository = new JobPostingRepository(_db);

            var jobPosting1 = new JobPosting
            {
                Title = "Cyber Security System Analyst",
                Description = "Analyze software security",
                Location = "Calabar",
                Company = "Andela",
                UserId = "1",
                User = new IdentityUser { Id = "1", UserName = "testuser" }
            };

            var jobPosting2 = new JobPosting
            {
                Title = "Business Analyst",
                Description = "Business Data",
                Location = "Kano",
                Company = "Andela",
                UserId = "2",
                User = new IdentityUser { Id = "2", UserName = "botuser" }
            };

            await _db.JobPostings.AddRangeAsync(jobPosting1, jobPosting2);
            await _db.SaveChangesAsync();

            // Act: Retrieve all job postings
            var result = await jobPostingRepository.GetAllAsync();

            // Assert: Verify that all job postings are returned
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.True(result.Count() >= 2);  // Ensure at least 2 job postings are returned
            Assert.Collection(result,
                j => Assert.Equal(jobPosting1.Title, j.Title),
                j => Assert.Equal(jobPosting2.Title, j.Title)
            );
        }


        [Fact]
        public async Task UpdateAsync_ShouldUpdateJobPosting()
        {
            // Arrange: Set up repository and add a job posting
            var jobPostingRepository = new JobPostingRepository(_db);
            var jobPosting = new JobPosting
            {
                Title = "Crime Analyst",
                Description = "Analyze Crime",
                Location = "Makurdi",
                Company = "Andela",
                UserId = "2",
                User = new IdentityUser { Id = "2", UserName = "botuser" }
            };

            await _db.JobPostings.AddAsync(jobPosting);
            await _db.SaveChangesAsync();

            // Update the job posting
            jobPosting.Title = "Data Scientist";
            jobPosting.Description = "Analyze Data";

            // Act: Update the job posting
            await jobPostingRepository.UpdateAsync(jobPosting);

            // Assert: Verify that the job posting was updated
            var result = await _db.JobPostings.FindAsync(jobPosting.Id);
            Assert.NotNull(result);
            Assert.Equal(jobPosting.Title, result.Title);
            Assert.Equal(jobPosting.Description, result.Description);
        }
    }
}
