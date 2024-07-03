using CandidateExperienceTest.Commands;
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
    public class CandidateCommandServiceDeleteTest
    {
        private readonly CandidateCommandService _service;
        private readonly ApplicationDbContext _context;

        public CandidateCommandServiceDeleteTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDbForTestingDelete")
                .Options;
            _context = new ApplicationDbContext(options);
            _context.Candidates.Add(new Candidate { IdCandidate = 1, Name = "Esteban", Surname = "Bernal", Email = "esteban123@gmail.com"});
            _context.SaveChanges();
            _service = new CandidateCommandService(_context);
        }

        [Fact]
        public async Task Handle_GivenExistingCandidate_DeletesCandidate()
        {
            var command = new DeleteCandidateCommand { IdCandidate = 1 };

            var result = await _service.CandidateDelete(command);

            Assert.True(result);
            Assert.Empty(_context.Candidates);
        }
    }
}
