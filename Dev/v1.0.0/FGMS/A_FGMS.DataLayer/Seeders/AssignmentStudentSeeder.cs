using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for AssignmentStudent
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the AssignmentStudent table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class AssignmentStudentSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 1, StudentTuid = 1, AssignmentTuid = 22, Date = DateTime.Parse("10/2/2022"), DesiredOutcome = "read on grade level" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 2, StudentTuid = 2, AssignmentTuid = 30, Date = DateTime.Parse("5/11/2022"), DesiredOutcome = "read on grade level" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 3, StudentTuid = 3, AssignmentTuid = 19, Date = DateTime.Parse("10/4/2022"), DesiredOutcome = "read on grade level" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 4, StudentTuid = 4, AssignmentTuid = 19, Date = DateTime.Parse("3/3/2022"), DesiredOutcome = "read on grade level" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 5, StudentTuid = 5, AssignmentTuid = 21, Date = DateTime.Parse("10/15/2022"), DesiredOutcome = "read on grade level" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 6, StudentTuid = 6, AssignmentTuid = 6, Date = DateTime.Parse("2/14/2022"), DesiredOutcome = "read on grade level" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 7, StudentTuid = 7, AssignmentTuid = 19, Date = DateTime.Parse("3/13/2022"), DesiredOutcome = "read on grade level" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 8, StudentTuid = 8, AssignmentTuid = 15, Date = DateTime.Parse("6/26/2022"), DesiredOutcome = "read on grade level" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 9, StudentTuid = 9, AssignmentTuid = 3, Date = DateTime.Parse("4/6/2022"), DesiredOutcome = "read on grade level" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 10, StudentTuid = 10, AssignmentTuid = 19, Date = DateTime.Parse("4/24/2022"), DesiredOutcome = "read on grade level" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 11, StudentTuid = 11, AssignmentTuid = 18, Date = DateTime.Parse("2/23/2022"), DesiredOutcome = "read on grade level" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 12, StudentTuid = 12, AssignmentTuid = 15, Date = DateTime.Parse("1/15/2022"), DesiredOutcome = "read on grade level" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 13, StudentTuid = 13, AssignmentTuid = 27, Date = DateTime.Parse("1/24/2022"), DesiredOutcome = "read on grade level" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 14, StudentTuid = 14, AssignmentTuid = 15, Date = DateTime.Parse("4/9/2022"), DesiredOutcome = "read on grade level" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 15, StudentTuid = 15, AssignmentTuid = 25, Date = DateTime.Parse("4/28/2022"), DesiredOutcome = "read on grade level" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 16, StudentTuid = 16, AssignmentTuid = 2, Date = DateTime.Parse("12/27/2022"), DesiredOutcome = "read on grade level" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 17, StudentTuid = 17, AssignmentTuid = 6, Date = DateTime.Parse("4/7/2022"), DesiredOutcome = "read on grade level" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 18, StudentTuid = 18, AssignmentTuid = 24, Date = DateTime.Parse("1/25/2022"), DesiredOutcome = "read on grade level" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 19, StudentTuid = 19, AssignmentTuid = 25, Date = DateTime.Parse("10/8/2022"), DesiredOutcome = "read on grade level" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 20, StudentTuid = 20, AssignmentTuid = 13, Date = DateTime.Parse("5/7/2022"), DesiredOutcome = "read on grade level" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 21, StudentTuid = 21, AssignmentTuid = 6, Date = DateTime.Parse("6/19/2022"), DesiredOutcome = "social skills" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 22, StudentTuid = 22, AssignmentTuid = 14, Date = DateTime.Parse("7/15/2022"), DesiredOutcome = "social skills" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 23, StudentTuid = 23, AssignmentTuid = 10, Date = DateTime.Parse("2/9/2022"), DesiredOutcome = "social skills" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 24, StudentTuid = 24, AssignmentTuid = 6, Date = DateTime.Parse("5/3/2022"), DesiredOutcome = "social skills" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 25, StudentTuid = 25, AssignmentTuid = 17, Date = DateTime.Parse("1/14/2022"), DesiredOutcome = "social skills" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 26, StudentTuid = 26, AssignmentTuid = 28, Date = DateTime.Parse("8/13/2022"), DesiredOutcome = "social skills" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 27, StudentTuid = 27, AssignmentTuid = 27, Date = DateTime.Parse("4/16/2022"), DesiredOutcome = "social skills" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 28, StudentTuid = 28, AssignmentTuid = 7, Date = DateTime.Parse("12/11/2022"), DesiredOutcome = "social skills" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 29, StudentTuid = 29, AssignmentTuid = 16, Date = DateTime.Parse("5/27/2022"), DesiredOutcome = "social skills" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 30, StudentTuid = 30, AssignmentTuid = 26, Date = DateTime.Parse("6/13/2022"), DesiredOutcome = "social skills" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 31, StudentTuid = 31, AssignmentTuid = 6, Date = DateTime.Parse("5/2/2022"), DesiredOutcome = "social skills" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 32, StudentTuid = 32, AssignmentTuid = 20, Date = DateTime.Parse("11/9/2022"), DesiredOutcome = "social skills" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 33, StudentTuid = 33, AssignmentTuid = 16, Date = DateTime.Parse("5/15/2022"), DesiredOutcome = "social skills" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 34, StudentTuid = 34, AssignmentTuid = 30, Date = DateTime.Parse("12/3/2022"), DesiredOutcome = "social skills" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 35, StudentTuid = 35, AssignmentTuid = 13, Date = DateTime.Parse("4/28/2022"), DesiredOutcome = "social skills" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 36, StudentTuid = 36, AssignmentTuid = 27, Date = DateTime.Parse("10/19/2022"), DesiredOutcome = "social skills" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 37, StudentTuid = 37, AssignmentTuid = 9, Date = DateTime.Parse("12/15/2022"), DesiredOutcome = "social skills" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 38, StudentTuid = 38, AssignmentTuid = 18, Date = DateTime.Parse("6/5/2022"), DesiredOutcome = "social skills" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 39, StudentTuid = 39, AssignmentTuid = 21, Date = DateTime.Parse("3/25/2022"), DesiredOutcome = "social skills" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 40, StudentTuid = 40, AssignmentTuid = 3, Date = DateTime.Parse("7/14/2022"), DesiredOutcome = "social skills" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 41, StudentTuid = 41, AssignmentTuid = 16, Date = DateTime.Parse("5/28/2022"), DesiredOutcome = "Count 1-10" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 42, StudentTuid = 42, AssignmentTuid = 25, Date = DateTime.Parse("9/9/2022"), DesiredOutcome = "Count 1-10" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 43, StudentTuid = 43, AssignmentTuid = 12, Date = DateTime.Parse("3/6/2022"), DesiredOutcome = "Count 1-10" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 44, StudentTuid = 44, AssignmentTuid = 24, Date = DateTime.Parse("10/15/2022"), DesiredOutcome = "Count 1-10" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 45, StudentTuid = 45, AssignmentTuid = 14, Date = DateTime.Parse("6/9/2022"), DesiredOutcome = "Count 1-10" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 46, StudentTuid = 46, AssignmentTuid = 4, Date = DateTime.Parse("10/13/2022"), DesiredOutcome = "Count 1-10" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 47, StudentTuid = 47, AssignmentTuid = 20, Date = DateTime.Parse("10/10/2022"), DesiredOutcome = "Count 1-10" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 48, StudentTuid = 48, AssignmentTuid = 15, Date = DateTime.Parse("2/22/2022"), DesiredOutcome = "Count 1-10" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 49, StudentTuid = 49, AssignmentTuid = 27, Date = DateTime.Parse("1/9/2022"), DesiredOutcome = "Count 1-10" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 50, StudentTuid = 50, AssignmentTuid = 16, Date = DateTime.Parse("12/1/2022"), DesiredOutcome = "Count 1-10" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 51, StudentTuid = 51, AssignmentTuid = 20, Date = DateTime.Parse("3/18/2022"), DesiredOutcome = "Count 1-10" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 52, StudentTuid = 52, AssignmentTuid = 10, Date = DateTime.Parse("8/11/2022"), DesiredOutcome = "Count 1-10" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 53, StudentTuid = 53, AssignmentTuid = 17, Date = DateTime.Parse("12/23/2022"), DesiredOutcome = "Count 1-10" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 54, StudentTuid = 54, AssignmentTuid = 3, Date = DateTime.Parse("1/8/2022"), DesiredOutcome = "Count 1-10" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 55, StudentTuid = 55, AssignmentTuid = 28, Date = DateTime.Parse("8/9/2022"), DesiredOutcome = "Count 1-10" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 56, StudentTuid = 56, AssignmentTuid = 24, Date = DateTime.Parse("6/8/2022"), DesiredOutcome = "Count 1-10" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 57, StudentTuid = 57, AssignmentTuid = 29, Date = DateTime.Parse("6/14/2022"), DesiredOutcome = "Count 1-10" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 58, StudentTuid = 58, AssignmentTuid = 18, Date = DateTime.Parse("8/1/2022"), DesiredOutcome = "Count 1-10" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 59, StudentTuid = 59, AssignmentTuid = 26, Date = DateTime.Parse("12/22/2022"), DesiredOutcome = "Count 1-10" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 60, StudentTuid = 60, AssignmentTuid = 7, Date = DateTime.Parse("9/12/2022"), DesiredOutcome = "Count 1-10" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 61, StudentTuid = 61, AssignmentTuid = 1, Date = DateTime.Parse("7/5/2022"), DesiredOutcome = "Proficiency" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 62, StudentTuid = 62, AssignmentTuid = 11, Date = DateTime.Parse("3/13/2022"), DesiredOutcome = "Proficiency" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 63, StudentTuid = 63, AssignmentTuid = 2, Date = DateTime.Parse("12/9/2022"), DesiredOutcome = "Proficiency" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 64, StudentTuid = 64, AssignmentTuid = 13, Date = DateTime.Parse("7/19/2022"), DesiredOutcome = "Proficiency" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 65, StudentTuid = 65, AssignmentTuid = 29, Date = DateTime.Parse("7/22/2022"), DesiredOutcome = "Proficiency" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 66, StudentTuid = 66, AssignmentTuid = 21, Date = DateTime.Parse("9/5/2022"), DesiredOutcome = "Proficiency" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 67, StudentTuid = 67, AssignmentTuid = 17, Date = DateTime.Parse("5/1/2022"), DesiredOutcome = "Proficiency" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 68, StudentTuid = 68, AssignmentTuid = 5, Date = DateTime.Parse("5/18/2022"), DesiredOutcome = "Proficiency" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 69, StudentTuid = 69, AssignmentTuid = 8, Date = DateTime.Parse("12/22/2022"), DesiredOutcome = "Proficiency" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 70, StudentTuid = 70, AssignmentTuid = 29, Date = DateTime.Parse("7/23/2022"), DesiredOutcome = "Proficiency" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 71, StudentTuid = 71, AssignmentTuid = 9, Date = DateTime.Parse("6/17/2022"), DesiredOutcome = "Proficiency" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 72, StudentTuid = 72, AssignmentTuid = 9, Date = DateTime.Parse("11/28/2022"), DesiredOutcome = "Proficiency" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 73, StudentTuid = 73, AssignmentTuid = 5, Date = DateTime.Parse("1/1/2022"), DesiredOutcome = "Proficiency" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 74, StudentTuid = 74, AssignmentTuid = 26, Date = DateTime.Parse("2/8/2022"), DesiredOutcome = "Proficiency" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 75, StudentTuid = 75, AssignmentTuid = 2, Date = DateTime.Parse("5/20/2022"), DesiredOutcome = "Proficiency" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 76, StudentTuid = 76, AssignmentTuid = 1, Date = DateTime.Parse("10/21/2022"), DesiredOutcome = "Proficiency" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 77, StudentTuid = 77, AssignmentTuid = 21, Date = DateTime.Parse("3/19/2022"), DesiredOutcome = "Proficiency" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 78, StudentTuid = 78, AssignmentTuid = 3, Date = DateTime.Parse("7/13/2022"), DesiredOutcome = "Proficiency" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 79, StudentTuid = 79, AssignmentTuid = 18, Date = DateTime.Parse("1/9/2022"), DesiredOutcome = "Proficiency" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 80, StudentTuid = 80, AssignmentTuid = 25, Date = DateTime.Parse("5/9/2022"), DesiredOutcome = "Proficiency" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 81, StudentTuid = 81, AssignmentTuid = 17, Date = DateTime.Parse("5/4/2022"), DesiredOutcome = "Grade level in all areas" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 82, StudentTuid = 82, AssignmentTuid = 14, Date = DateTime.Parse("9/25/2022"), DesiredOutcome = "Grade level in all areas" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 83, StudentTuid = 83, AssignmentTuid = 27, Date = DateTime.Parse("3/9/2022"), DesiredOutcome = "Grade level in all areas" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 84, StudentTuid = 84, AssignmentTuid = 29, Date = DateTime.Parse("5/16/2022"), DesiredOutcome = "Grade level in all areas" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 85, StudentTuid = 85, AssignmentTuid = 3, Date = DateTime.Parse("10/24/2022"), DesiredOutcome = "Grade level in all areas" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 86, StudentTuid = 86, AssignmentTuid = 23, Date = DateTime.Parse("1/7/2022"), DesiredOutcome = "Grade level in all areas" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 87, StudentTuid = 87, AssignmentTuid = 22, Date = DateTime.Parse("11/25/2022"), DesiredOutcome = "Grade level in all areas" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 88, StudentTuid = 88, AssignmentTuid = 7, Date = DateTime.Parse("11/28/2022"), DesiredOutcome = "Grade level in all areas" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 89, StudentTuid = 89, AssignmentTuid = 1, Date = DateTime.Parse("4/10/2022"), DesiredOutcome = "Grade level in all areas" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 90, StudentTuid = 90, AssignmentTuid = 13, Date = DateTime.Parse("7/26/2022"), DesiredOutcome = "Grade level in all areas" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 91, StudentTuid = 91, AssignmentTuid = 3, Date = DateTime.Parse("9/5/2022"), DesiredOutcome = "Grade level in all areas" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 92, StudentTuid = 92, AssignmentTuid = 7, Date = DateTime.Parse("6/21/2022"), DesiredOutcome = "Grade level in all areas" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 93, StudentTuid = 93, AssignmentTuid = 4, Date = DateTime.Parse("12/22/2022"), DesiredOutcome = "Grade level in all areas" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 94, StudentTuid = 94, AssignmentTuid = 1, Date = DateTime.Parse("3/5/2022"), DesiredOutcome = "Grade level in all areas" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 95, StudentTuid = 95, AssignmentTuid = 1, Date = DateTime.Parse("2/19/2022"), DesiredOutcome = "Grade level in all areas" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 96, StudentTuid = 96, AssignmentTuid = 8, Date = DateTime.Parse("8/18/2022"), DesiredOutcome = "Grade level in all areas" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 97, StudentTuid = 97, AssignmentTuid = 1, Date = DateTime.Parse("10/13/2022"), DesiredOutcome = "Grade level in all areas" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 98, StudentTuid = 98, AssignmentTuid = 16, Date = DateTime.Parse("10/11/2022"), DesiredOutcome = "Grade level in all areas" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 99, StudentTuid = 99, AssignmentTuid = 6, Date = DateTime.Parse("2/18/2022"), DesiredOutcome = "Grade level in all areas" });
			modelBuilder.Entity<AssignmentStudent>().HasData(new AssignmentStudent() { Tuid = 100, StudentTuid = 100, AssignmentTuid = 22, Date = DateTime.Parse("12/20/2022"), DesiredOutcome = "Grade level in all areas" });
		}
	}
}
