using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for StudentNeed
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the StudentNeed table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class StudentNeedSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 1, StudentTuid = 12, StudentNeedItemTuid = 1 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 2, StudentTuid = 71, StudentNeedItemTuid = 2 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 3, StudentTuid = 72, StudentNeedItemTuid = 3 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 4, StudentTuid = 19, StudentNeedItemTuid = 4 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 5, StudentTuid = 70, StudentNeedItemTuid = 5 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 6, StudentTuid = 93, StudentNeedItemTuid = 6 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 7, StudentTuid = 48, StudentNeedItemTuid = 7 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 8, StudentTuid = 66, StudentNeedItemTuid = 8 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 9, StudentTuid = 75, StudentNeedItemTuid = 9 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 10, StudentTuid = 89, StudentNeedItemTuid = 1 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 11, StudentTuid = 60, StudentNeedItemTuid = 2 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 12, StudentTuid = 18, StudentNeedItemTuid = 3 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 13, StudentTuid = 78, StudentNeedItemTuid = 4 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 14, StudentTuid = 11, StudentNeedItemTuid = 5 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 15, StudentTuid = 98, StudentNeedItemTuid = 6 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 16, StudentTuid = 6, StudentNeedItemTuid = 7 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 17, StudentTuid = 69, StudentNeedItemTuid = 8 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 18, StudentTuid = 81, StudentNeedItemTuid = 9 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 19, StudentTuid = 65, StudentNeedItemTuid = 1 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 20, StudentTuid = 23, StudentNeedItemTuid = 2 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 21, StudentTuid = 74, StudentNeedItemTuid = 3 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 22, StudentTuid = 24, StudentNeedItemTuid = 4 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 23, StudentTuid = 68, StudentNeedItemTuid = 5 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 24, StudentTuid = 45, StudentNeedItemTuid = 6 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 25, StudentTuid = 79, StudentNeedItemTuid = 7 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 26, StudentTuid = 96, StudentNeedItemTuid = 8 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 27, StudentTuid = 32, StudentNeedItemTuid = 9 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 28, StudentTuid = 53, StudentNeedItemTuid = 1 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 29, StudentTuid = 50, StudentNeedItemTuid = 2 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 30, StudentTuid = 91, StudentNeedItemTuid = 3 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 31, StudentTuid = 3, StudentNeedItemTuid = 4 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 32, StudentTuid = 17, StudentNeedItemTuid = 5 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 33, StudentTuid = 38, StudentNeedItemTuid = 6 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 34, StudentTuid = 84, StudentNeedItemTuid = 7 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 35, StudentTuid = 88, StudentNeedItemTuid = 8 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 36, StudentTuid = 21, StudentNeedItemTuid = 9 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 37, StudentTuid = 2, StudentNeedItemTuid = 1 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 38, StudentTuid = 8, StudentNeedItemTuid = 2 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 39, StudentTuid = 77, StudentNeedItemTuid = 3 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 40, StudentTuid = 31, StudentNeedItemTuid = 4 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 41, StudentTuid = 97, StudentNeedItemTuid = 5 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 42, StudentTuid = 20, StudentNeedItemTuid = 6 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 43, StudentTuid = 29, StudentNeedItemTuid = 7 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 44, StudentTuid = 80, StudentNeedItemTuid = 8 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 45, StudentTuid = 92, StudentNeedItemTuid = 9 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 46, StudentTuid = 57, StudentNeedItemTuid = 1 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 47, StudentTuid = 42, StudentNeedItemTuid = 2 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 48, StudentTuid = 34, StudentNeedItemTuid = 3 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 49, StudentTuid = 85, StudentNeedItemTuid = 4 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 50, StudentTuid = 54, StudentNeedItemTuid = 5 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 51, StudentTuid = 62, StudentNeedItemTuid = 6 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 52, StudentTuid = 59, StudentNeedItemTuid = 7 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 53, StudentTuid = 25, StudentNeedItemTuid = 8 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 54, StudentTuid = 30, StudentNeedItemTuid = 9 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 55, StudentTuid = 47, StudentNeedItemTuid = 1 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 56, StudentTuid = 43, StudentNeedItemTuid = 2 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 57, StudentTuid = 9, StudentNeedItemTuid = 3 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 58, StudentTuid = 14, StudentNeedItemTuid = 4 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 59, StudentTuid = 33, StudentNeedItemTuid = 5 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 60, StudentTuid = 94, StudentNeedItemTuid = 6 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 61, StudentTuid = 51, StudentNeedItemTuid = 7 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 62, StudentTuid = 61, StudentNeedItemTuid = 8 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 63, StudentTuid = 90, StudentNeedItemTuid = 9 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 64, StudentTuid = 16, StudentNeedItemTuid = 1 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 65, StudentTuid = 73, StudentNeedItemTuid = 2 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 66, StudentTuid = 4, StudentNeedItemTuid = 3 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 67, StudentTuid = 55, StudentNeedItemTuid = 4 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 68, StudentTuid = 41, StudentNeedItemTuid = 5 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 69, StudentTuid = 56, StudentNeedItemTuid = 6 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 70, StudentTuid = 7, StudentNeedItemTuid = 7 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 71, StudentTuid = 52, StudentNeedItemTuid = 8 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 72, StudentTuid = 36, StudentNeedItemTuid = 9 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 73, StudentTuid = 10, StudentNeedItemTuid = 1 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 74, StudentTuid = 37, StudentNeedItemTuid = 2 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 75, StudentTuid = 46, StudentNeedItemTuid = 3 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 76, StudentTuid = 100, StudentNeedItemTuid = 4 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 77, StudentTuid = 39, StudentNeedItemTuid = 5 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 78, StudentTuid = 15, StudentNeedItemTuid = 6 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 79, StudentTuid = 28, StudentNeedItemTuid = 7 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 80, StudentTuid = 5, StudentNeedItemTuid = 8 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 81, StudentTuid = 86, StudentNeedItemTuid = 9 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 82, StudentTuid = 95, StudentNeedItemTuid = 1 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 83, StudentTuid = 40, StudentNeedItemTuid = 2 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 84, StudentTuid = 49, StudentNeedItemTuid = 3 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 85, StudentTuid = 76, StudentNeedItemTuid = 4 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 86, StudentTuid = 22, StudentNeedItemTuid = 5 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 87, StudentTuid = 87, StudentNeedItemTuid = 6 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 88, StudentTuid = 67, StudentNeedItemTuid = 7 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 89, StudentTuid = 35, StudentNeedItemTuid = 8 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 90, StudentTuid = 44, StudentNeedItemTuid = 9 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 91, StudentTuid = 58, StudentNeedItemTuid = 1 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 92, StudentTuid = 82, StudentNeedItemTuid = 2 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 93, StudentTuid = 26, StudentNeedItemTuid = 3 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 94, StudentTuid = 1, StudentNeedItemTuid = 4 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 95, StudentTuid = 83, StudentNeedItemTuid = 5 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 96, StudentTuid = 99, StudentNeedItemTuid = 6 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 97, StudentTuid = 63, StudentNeedItemTuid = 7 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 98, StudentTuid = 27, StudentNeedItemTuid = 8 });
			modelBuilder.Entity<StudentNeed>().HasData(new StudentNeed() { Tuid = 99, StudentTuid = 64, StudentNeedItemTuid = 9 });
		}
	}
}
