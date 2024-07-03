using CandidateExperienceTest.Entities;

namespace CandidateExperienceTest.Commands
{
    public class UpdateCandidateCommand
    {
        public int IdCandidate { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public List<UpdateCandidateExperienceCommand> CandidateExperiences { get; set; }
    }
}
