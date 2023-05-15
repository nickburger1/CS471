using A_FGMS.DataLayer.Entities;
using A_FGMS.DataLayer.EventBroker;
using A_FGMS.DataLayer.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


/// <summary>
/// Provide an Entity Framework connection to the database.
/// </summary>
/// <author>Richard Nader, Jr.</author>
/// <dateCreated>01/17/2023</dateCreated>
namespace A_FGMS.DataLayer
{
#pragma warning disable CS8618
    public class ApplicationDbContext : DbContext
    {
        private readonly EntityChangeEventBroker? _eventBroker;

        public ApplicationDbContext() {}

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, EntityChangeEventBroker? eventBroker = null) : base(options)
        {
            _eventBroker = eventBroker;
        }

        public virtual DbSet<ActivityLog> ActivityLogs { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AnnualCheck> AnnualChecks { get; set; }
        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<AssignmentStudent> AssignmentStudents {get; set;} 
        public virtual DbSet<ConditionItem> ConditionItems { get; set; }
        public virtual DbSet<Classroom> Classrooms { get; set; }
        public virtual DbSet<DonationTypeItem> DonationTypeItems { get; set; }
        public virtual DbSet<EthnicityTypeItem> EthnicityTypeItems { get; set; }
        public virtual DbSet<ExpenseTypeItem> ExpenseTypeItems { get; set; }
        public virtual DbSet<GenderTypeItem> GenderTypeItems { get; set; }
        public virtual DbSet<GrantStipend> GrantStipends { get; set; }
        public virtual DbSet<IdentifiesAsTypeItem> IdentifiesAsTypeItems { get; set; }
        public virtual DbSet<InactiveStatusTypeItem> InactiveStatusTypeItems { get; set; }
        public virtual DbSet<InKindDonationTypeItem> InKindDonationTypeItems { get; set; }
        public virtual DbSet<InKindExpense> InKindExpenses { get; set; }
        public virtual DbSet<InKindExpenseTypeItem> InKindExpenseTypeItems { get; set; }
        public virtual DbSet<MealMileage> MealMileages { get; set; }
        public virtual DbSet<MealTransportRate> MealTransportRates { get; set; }
        public virtual DbSet<OneTimeCheck> OneTimeChecks { get; set; }
        public virtual DbSet<PTOStipend> PTOStipends { get; set; }
        public virtual DbSet<PTOStipendRate> PTOStipendRates { get; set; }
        public virtual DbSet<RacialGroupTypeItem> RacialGroupTypeItems { get; set; }
        public virtual DbSet<ReasonSeparated> ReasonsSeparated { get; set; }
        public virtual DbSet<ReportPreset> ReportPresets { get; set; }
        public virtual DbSet<School> Schools { get; set; }
        public virtual DbSet<SchoolCostShare> SchoolCostShares { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentCondition> StudentConditions { get; set; }
        public virtual DbSet<StudentNeed> StudentNeeds { get; set; }
        public virtual DbSet<StudentNeedItem> StudentNeedItems { get; set; }
        public virtual DbSet<TempInfoTypeItem> TempInfoTypeItems { get; set; }
        public virtual DbSet<TempInfoEntry> TempInfoEntries { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Volunteer> Volunteers { get; set; }

        /// <function>OnModelCreating</function>
        /// <author>Tyler Moody</author>
        /// <dateCreated>2/01/23</dateCreated>
        /// <summary>
        /// Seeds the database with default data.
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <returns>Nothing</returns>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //new AddressSeeder().SeedData(modelBuilder);
            new ConditionItemSeeder().SeedData(modelBuilder);
            //new DonationTypeItemSeeder().SeedData(modelBuilder);
            new EthnicityTypeItemSeeder().SeedData(modelBuilder);
            new GenderTypeItemSeeder().SeedData(modelBuilder);
            //new ExpenseTypeItemSeeder().SeedData(modelBuilder);
            new IdentifiesAsTypeItemSeeder().SeedData(modelBuilder);
            new InactiveStatusTypeItemSeeder().SeedData(modelBuilder);
            //new InKindDonationItemSeeder().SeedData(modelBuilder);
            //new InKindExpenseTypeItemSeeder().SeedData(modelBuilder);
            //new MealTransportRateSeeder().SeedData(modelBuilder);
            //new PTOStipendRateSeeder().SeedData(modelBuilder);
            new RacialGroupTypeItemSeeder().SeedData(modelBuilder);
            //new SchoolSeeder().SeedData(modelBuilder);
            //new SchoolCostShareSeeder().SeedData(modelBuilder);
            //new StudentSeeder().SeedData(modelBuilder);
            //new StudentConditionSeeder().SeedData(modelBuilder);
            new StudentNeedItemSeeder().SeedData(modelBuilder);
            //new StudentNeedSeeder().SeedData(modelBuilder);
            //new VolunteerSeeder().SeedData(modelBuilder);
            //new ActivityLogSeeder().SeedData(modelBuilder);
            //new AnnualCheckSeeder().SeedData(modelBuilder);
            //new ClassroomSeeder().SeedData(modelBuilder);
            //new AssignmentSeeder().SeedData(modelBuilder);
            //new AssignmentStudentSeeder().SeedData(modelBuilder);
            //new InKindExpenseSeeder().SeedData(modelBuilder);
            //new MealMilageSeeder().SeedData(modelBuilder);
            //new OneTimeCheckSeeder().SeedData(modelBuilder);
            //new PTOStipendSeeder().SeedData(modelBuilder);
            //new ReasonSeparatedSeeder().SeedData(modelBuilder);
            //new TempInfoTypeItemSeeder().SeedData(modelBuilder);
            //new TempInfoEntrySeeder().SeedData(modelBuilder);
            new UsersSeeder().SeedData(modelBuilder);
        }

        /// <summary>
        /// Override the Save changes method to publish events to the event broker when database changes are made
        /// </summary>
        /// <author>Richard Nader, Jr.</author>
        /// <dateCreated>03/16/2023</dateCreated>
        public override int SaveChanges()
        {
            var changedEntries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added)
                .ToList();

            int result = base.SaveChanges();

            if (result > 0 && _eventBroker != null)
            {
                var entitiesChangedBatch = new List<EntityChangedEvent>();
                foreach(var entry in changedEntries)
                {
                    Type entityType = entry.Entity.GetType();

                    int? tuid = null;

                    var prop = entityType.GetProperty("Tuid");
                    if (prop != null)
                    {
                        tuid = (int) prop.GetValue(entry.Entity, null);
                    }

                    entitiesChangedBatch.Add(new EntityChangedEvent(tuid, entityType, entry.Entity));
                }

                _eventBroker.Publish(entitiesChangedBatch);
            }

            return result;
        }
    }
}