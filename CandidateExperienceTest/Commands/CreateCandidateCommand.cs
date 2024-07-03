using CandidateExperienceTest.Entities;

namespace CandidateExperienceTest.Commands
{
    public class CreateCandidateCommand
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public List<CreateCandidateExperienceCommand> CandidateExperiences { get; set; }
    }
}
