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
    public class CandidateCommandServiceUpdateTest
    {
        private readonly CandidateCommandService _service;
        private readonly ApplicationDbContext _context;

        public CandidateCommandServiceUpdateTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDbForTestingUpdate")
                .Options;
            _context = new ApplicationDbContext(options);
            _context.Candidates.Add(new Candidate
            {
                IdCandidate = 1,
                Name = "Esteban",
                Surname = "Bernal",
                Email = "esteban123@gmail.com",
                CandidateExperiences = new List<CandidateExperience>
            {
                new CandidateExperience { IdCandidateExperience = 1, Company = "Company test 1", Description = "description", Job = "Job"}
            }
            });
            _context.SaveChanges();
            _service = new CandidateCommandService(_context);
        }

        [Fact]
        public async Task Handle_GivenUpdatedCandidate_ReturnsUpdatedData()
        {
            var command = new UpdateCandidateCommand
            {
                IdCandidate = 1,
                Name = "Esteban 1",
                Surname = "Bernal 1",
                Email = "esteban321@gmail.com",
                CandidateExperiences = new System.Collections.Generic.List<UpdateCandidateExperienceCommand>
            {
                new UpdateCandidateExperienceCommand
                {
                    IdCandidateExperience = 1,
                    Company = "Company test 1 update",
                    Description = "description",
                    Job = "Job"
                },
                new UpdateCandidateExperienceCommand
                {
                    Company = "Company test 1 update",
                    Description = "description",
                    Job = "Job"
                }
            }
            };

            var result = await _service.CandidateUpdate(command);

            Assert.Equal("Esteban 1", result.Name);
            Assert.Equal(2, result.CandidateExperiences.Count); // Check that a new experience was added
            Assert.Contains(result.CandidateExperiences, ce => ce.Company == "Company test 1 update");
        }
    }
}
