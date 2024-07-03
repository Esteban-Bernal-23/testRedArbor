namespace CandidateExperienceTest.Commands
{
    public class CreateCandidateExperienceCommand
    {
        public string Company { get; set; }
        public string Job { get; set; }
        public string Description { get; set; }
        public decimal Salary { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime InsertDate { get; set; }
    }
}
