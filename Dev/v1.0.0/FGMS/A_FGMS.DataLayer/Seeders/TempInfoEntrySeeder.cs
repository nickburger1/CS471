using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for TempInfoEntry
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the TempInfoEntry table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class TempInfoEntrySeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<TempInfoEntry>().HasData(new TempInfoEntry() { Tuid = 1, TempInfoTypeItemTuid = 1, VolunteerTuid = 1, value = "true" });
		}
	}
}
