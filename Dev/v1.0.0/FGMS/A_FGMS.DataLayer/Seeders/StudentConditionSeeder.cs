using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for StudentCondition
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the StudentCondition table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class StudentConditionSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<StudentCondition>().HasData(new StudentCondition() { Tuid = 1, ConditionItemTuid = 9, StudentTuid = 91 });
			modelBuilder.Entity<StudentCondition>().HasData(new StudentCondition() { Tuid = 2, ConditionItemTuid = 7, StudentTuid = 57 });
			modelBuilder.Entity<StudentCondition>().HasData(new StudentCondition() { Tuid = 3, ConditionItemTuid = 6, StudentTuid = 4 });
			modelBuilder.Entity<StudentCondition>().HasData(new StudentCondition() { Tuid = 4, ConditionItemTuid = 4, StudentTuid = 95 });
			modelBuilder.Entity<StudentCondition>().HasData(new StudentCondition() { Tuid = 5, ConditionItemTuid = 6, StudentTuid = 69 });
			modelBuilder.Entity<StudentCondition>().HasData(new StudentCondition() { Tuid = 6, ConditionItemTuid = 3, StudentTuid = 59 });
			modelBuilder.Entity<StudentCondition>().HasData(new StudentCondition() { Tuid = 7, ConditionItemTuid = 4, StudentTuid = 73 });
			modelBuilder.Entity<StudentCondition>().HasData(new StudentCondition() { Tuid = 8, ConditionItemTuid = 10, StudentTuid = 56 });
			modelBuilder.Entity<StudentCondition>().HasData(new StudentCondition() { Tuid = 9, ConditionItemTuid = 1, StudentTuid = 66 });
			modelBuilder.Entity<StudentCondition>().HasData(new StudentCondition() { Tuid = 10, ConditionItemTuid = 4, StudentTuid = 69 });
			modelBuilder.Entity<StudentCondition>().HasData(new StudentCondition() { Tuid = 11, ConditionItemTuid = 10, StudentTuid = 22 });
			modelBuilder.Entity<StudentCondition>().HasData(new StudentCondition() { Tuid = 12, ConditionItemTuid = 1, StudentTuid = 27 });
			modelBuilder.Entity<StudentCondition>().HasData(new StudentCondition() { Tuid = 13, ConditionItemTuid = 1, StudentTuid = 16 });
			modelBuilder.Entity<StudentCondition>().HasData(new StudentCondition() { Tuid = 14, ConditionItemTuid = 6, StudentTuid = 71 });
			modelBuilder.Entity<StudentCondition>().HasData(new StudentCondition() { Tuid = 15, ConditionItemTuid = 2, StudentTuid = 16 });
		}
	}
}
