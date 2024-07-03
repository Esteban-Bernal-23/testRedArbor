using CandidateExperienceTest.Commands;
using CandidateExperienceTest.Entities;
using CandidateExperienceTest.Persistence_Repository;
using Microsoft.EntityFrameworkCore;

namespace CandidateExperienceTest.Services
{
    public class CandidateCommandService
    {
        private readonly ApplicationDbContext _DBcontext;

        public CandidateCommandService(ApplicationDbContext context)
        {
            _DBcontext = context;
        }

        public async Task<Candidate> CandidateCreate(CreateCandidateCommand command)
        {
            if (_DBcontext.Candidates.Any(c => c.Email == command.Email))
                throw new Exception("A candidate with the same email already exists.");
        
            var candidate = ToCandidate(command);
            _DBcontext.Candidates.Add(candidate);
            await _DBcontext.SaveChangesAsync();

            return candidate;
        }

        public async Task<Candidate> CandidateUpdate(UpdateCandidateCommand command)
        {
            var candidate = await _DBcontext.Candidates.Include(c => c.CandidateExperiences)
                                                      .FirstOrDefaultAsync(c => c.IdCandidate == command.IdCandidate) ?? throw new Exception("Candidate not found.");
            candidate.Name = command.Name;
            candidate.Surname = command.Surname;
            candidate.Birthdate = command.Birthdate;
            candidate.Email = command.Email;
            candidate.ModifyDate = DateTime.Now;

            foreach (var exp in command.CandidateExperiences)
            {
                var existExperience = candidate.CandidateExperiences
                                                  .FirstOrDefault(e => e.IdCandidateExperience == exp.IdCandidateExperience);

                if (existExperience != null)
                {
                    existExperience.Company = exp.Company;
                    existExperience.Job = exp.Job;
                    existExperience.Description = exp.Description;
                    existExperience.Salary = exp.Salary;
                    existExperience.BeginDate = exp.BeginDate;
                    existExperience.EndDate = exp.EndDate;
                    existExperience.ModifyDate = DateTime.Now;
                }
                else
                {
                    candidate.CandidateExperiences.Add(new CandidateExperience
                    {
                        Company = exp.Company,
                        Job = exp.Job,
                        Description = exp.Description,
                        Salary = exp.Salary,
                        BeginDate = exp.BeginDate,
                        EndDate = exp.EndDate,
                        InsertDate = DateTime.Now,
                        IdCandidate = candidate.IdCandidate
                    });
                }
            }
            await _DBcontext.SaveChangesAsync();

            return candidate;
        }
        public async Task<bool> CandidateDelete(DeleteCandidateCommand command)
        {
            var candidate = await _DBcontext.Candidates.FindAsync(command.IdCandidate) ?? throw new Exception("Candidate not found.");
            _DBcontext.Candidates.Remove(candidate);
            await _DBcontext.SaveChangesAsync();

            return true;
        }

        private Candidate ToCandidate(CreateCandidateCommand command) { 
            return new Candidate
            {
                Name = command.Name,
                Surname = command.Surname,
                Birthdate = command.Birthdate,
                Email = command.Email,
                InsertDate = DateTime.Now,
                CandidateExperiences = command.CandidateExperiences.Select(ce => new CandidateExperience
                {
                    Company = ce.Company,
                    Job = ce.Job,
                    Description = ce.Description,
                    Salary = ce.Salary,
                    BeginDate = ce.BeginDate,
                    EndDate = ce.EndDate,
                    InsertDate = DateTime.Now
                }).ToList()
            };
        }
    }
}
