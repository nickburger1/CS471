using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for ReasonSeparated
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the ReasonSeparated table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class ReasonSeparatedSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ReasonSeparated>().HasData(new ReasonSeparated() { Tuid = 1, VolunteerTuid = 1, InactiveStatusTypeItemTuid = 8, Date = DateTime.Parse("3/3/2022") });
		}
	}
}
