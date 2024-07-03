using CandidateExperienceTest.Entities;
using CandidateExperienceTest.Persistence_Repository;
using Microsoft.EntityFrameworkCore;

namespace CandidateExperienceTest.Services
{
    public class CandidateQueryService
    {
        private readonly ApplicationDbContext _DBcontext;

        public CandidateQueryService(ApplicationDbContext context)
        {
            _DBcontext = context;
        }

        public async Task<List<Candidate>> GetAllCandidates()
        {
            return await Task.FromResult(_DBcontext.Candidates.Include(c => c.CandidateExperiences).ToList());
        }
    }
}
