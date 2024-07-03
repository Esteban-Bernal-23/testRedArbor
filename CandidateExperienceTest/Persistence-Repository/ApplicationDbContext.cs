using CandidateExperienceTest.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace CandidateExperienceTest.Persistence_Repository
{
    public class ApplicationDbContext : DbContext
    {
        private static int _candidateIdSequence = 1;
        private static int _candidateExperienceIdSequence = 1;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<CandidateExperience> CandidateExperiences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>()
                .HasKey(c => c.IdCandidate);

            modelBuilder.Entity<Candidate>()
                .HasMany(c => c.CandidateExperiences)
                .WithOne(e => e.Candidate)
                .HasForeignKey(e => e.IdCandidate);

            modelBuilder.Entity<CandidateExperience>()
                .HasKey(e => e.IdCandidateExperience);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            HandleIdAssignment();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            HandleIdAssignment();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void HandleIdAssignment()
        {
            foreach (var entry in ChangeTracker.Entries<Candidate>().Where(e => e.State == EntityState.Added))
            {
                entry.Entity.IdCandidate = _candidateIdSequence++;
            }

            foreach (var entry in ChangeTracker.Entries<CandidateExperience>().Where(e => e.State == EntityState.Added))
            {
                entry.Entity.IdCandidateExperience = _candidateExperienceIdSequence++;
            }
        }

        public static void ResetSequences()
        {
            _candidateIdSequence = 1;
            _candidateExperienceIdSequence = 1;
        }
    }
}
