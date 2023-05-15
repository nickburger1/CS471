using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for RacialGroupTypeItem
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the RacialGroupTypeItem table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class RacialGroupTypeItemSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<RacialGroupTypeItem>().HasData(new RacialGroupTypeItem() { Tuid = 1, Name = "Black" });
			modelBuilder.Entity<RacialGroupTypeItem>().HasData(new RacialGroupTypeItem() { Tuid = 2, Name = "White" });
			modelBuilder.Entity<RacialGroupTypeItem>().HasData(new RacialGroupTypeItem() { Tuid = 3, Name = "Asian" });
			modelBuilder.Entity<RacialGroupTypeItem>().HasData(new RacialGroupTypeItem() { Tuid = 4, Name = "Other" });
		}
	}
}
