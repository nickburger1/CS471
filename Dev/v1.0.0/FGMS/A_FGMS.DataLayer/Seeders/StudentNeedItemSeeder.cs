using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for StudentNeedItem
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the StudentNeedItem table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class StudentNeedItemSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<StudentNeedItem>().HasData(new StudentNeedItem() { Tuid = 1, Acronym = "A", Description = "Nurturing / Comfort" });
			modelBuilder.Entity<StudentNeedItem>().HasData(new StudentNeedItem() { Tuid = 2, Acronym = "B", Description = "Social Skilles" });
			modelBuilder.Entity<StudentNeedItem>().HasData(new StudentNeedItem() { Tuid = 3, Acronym = "C", Description = "Communication Skills" });
			modelBuilder.Entity<StudentNeedItem>().HasData(new StudentNeedItem() { Tuid = 4, Acronym = "D", Description = "Reading" });
			modelBuilder.Entity<StudentNeedItem>().HasData(new StudentNeedItem() { Tuid = 5, Acronym = "E", Description = "Help with Letter Identification" });
			modelBuilder.Entity<StudentNeedItem>().HasData(new StudentNeedItem() { Tuid = 6, Acronym = "F", Description = "Positive Reinforcement / Redirection" });
			modelBuilder.Entity<StudentNeedItem>().HasData(new StudentNeedItem() { Tuid = 7, Acronym = "G", Description = "Conversion Skills" });
			modelBuilder.Entity<StudentNeedItem>().HasData(new StudentNeedItem() { Tuid = 8, Acronym = "H", Description = "Help with Numeracy Skills" });
			modelBuilder.Entity<StudentNeedItem>().HasData(new StudentNeedItem() { Tuid = 9, Acronym = "I", Description = "Assist with Cognitive Activities" });
		}
	}
}
