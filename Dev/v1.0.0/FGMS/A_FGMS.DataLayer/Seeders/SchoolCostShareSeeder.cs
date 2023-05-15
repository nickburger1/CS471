using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for SchoolCostShare
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the SchoolCostShare table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class SchoolCostShareSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<SchoolCostShare>().HasData(new SchoolCostShare() { Tuid = 1, Name = "1st Billing", Date = DateTime.Parse("1/1/2022"), Value = 120.00 });
			modelBuilder.Entity<SchoolCostShare>().HasData(new SchoolCostShare() { Tuid = 2, Name = "2nd Billing", Date = DateTime.Parse("5/1/2022"), Value = 105.00 });
			modelBuilder.Entity<SchoolCostShare>().HasData(new SchoolCostShare() { Tuid = 3, Name = "3rd Billing", Date = DateTime.Parse("7/1/2022"), Value = 100.00 });
			modelBuilder.Entity<SchoolCostShare>().HasData(new SchoolCostShare() { Tuid = 4, Name = "4th Billing", Date = DateTime.Parse("11/1/2022"), Value = 180.00 });
		}
	}
}
