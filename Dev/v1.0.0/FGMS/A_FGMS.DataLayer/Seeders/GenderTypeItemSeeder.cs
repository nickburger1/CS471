using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for GenderTypeItem
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the GenderTypeItem table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class GenderTypeItemSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<GenderTypeItem>().HasData(new GenderTypeItem() { Tuid = 1, Name = "Female" });
			modelBuilder.Entity<GenderTypeItem>().HasData(new GenderTypeItem() { Tuid = 2, Name = "Male" });
			modelBuilder.Entity<GenderTypeItem>().HasData(new GenderTypeItem() { Tuid = 3, Name = "Transgender" });
			modelBuilder.Entity<GenderTypeItem>().HasData(new GenderTypeItem() { Tuid = 4, Name = "Non-binary/non-conforming" });
			modelBuilder.Entity<GenderTypeItem>().HasData(new GenderTypeItem() { Tuid = 5, Name = "Prefer not to respond" });
		}
	}
}
