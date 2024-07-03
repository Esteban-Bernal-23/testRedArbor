using CandidateExperienceTest.Commands;
using CandidateExperienceTest.Entities;
using CandidateExperienceTest.Persistence_Repository;
using CandidateExperienceTest.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CandidateExperienceTest.Tests
{
    public class CandidateCommandServiceAddTest
    {
        private readonly CandidateCommandService _service;
        private readonly Mock<ApplicationDbContext> _mockContext;

        public CandidateCommandServiceAddTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDbForTesting")
                .Options;
            var context = new ApplicationDbContext(options);
            _service = new CandidateCommandService(context);
        }

        [Fact]
        public async Task Handle_GivenValidCandidate_ReturnsCandidate()
        {
            var command = new CreateCandidateCommand
            {
                Name = "Esteban",
                Surname = "Bernal",
                Birthdate = System.DateTime.Now,
                Email = "esteban123@gmail.com",
                CandidateExperiences = new List<CreateCandidateExperienceCommand>
                    {
                        new CreateCandidateExperienceCommand
                        {
                            Company = "Company test 1",
                            Job = "Job",
                            Description = "description",
                            Salary = 70000,
                            BeginDate = System.DateTime.Now.AddYears(-2),
                            EndDate = System.DateTime.Now,
                            InsertDate = System.DateTime.Now
                        },
                        new CreateCandidateExperienceCommand
                        {
                            Company = "Company test 2",
                            Job = "Job",
                            Description = "description",
                            Salary = 90000,
                            BeginDate = System.DateTime.Now.AddYears(-1),
                            EndDate = null,
                            InsertDate = System.DateTime.Now
                        }
                    }
            };

            var result = await _service.CandidateCreate(command);

            Assert.NotNull(result);
            Assert.Equal("Esteban", result.Name);
        }
    }
}
