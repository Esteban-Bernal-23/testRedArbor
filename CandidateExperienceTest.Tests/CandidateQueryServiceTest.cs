using CandidateExperienceTest.Entities;
using CandidateExperienceTest.Persistence_Repository;
using CandidateExperienceTest.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateExperienceTest.Tests
{
    public class CandidateQueryServiceTest
    {
        private readonly CandidateQueryService _service;
        private readonly ApplicationDbContext _context;

        public CandidateQueryServiceTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDbForTestingGetAll")
                .Options;
            _context = new ApplicationDbContext(options);
            _context.Candidates.AddRange(
                new Candidate { IdCandidate = 1, Name = "Esteban", Surname = "Bernal", Email = "esteban123@gmail.com" },
                new Candidate { IdCandidate = 2, Name = "Guillermo", Surname = "Bernal", Email = "guillo123@gmail.com" }
            );
            _context.SaveChanges();
            _service = new CandidateQueryService(_context);
        }

        [Fact]
        public async Task Handle_ReturnsAllCandidates()
        {
            var results = await _service.GetAllCandidates();

            Assert.Equal(2, results.Count());
        }
    }
}
