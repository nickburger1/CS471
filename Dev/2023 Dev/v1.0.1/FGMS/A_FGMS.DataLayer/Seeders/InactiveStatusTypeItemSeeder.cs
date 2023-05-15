using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for InactiveStatusTypeItem
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the InactiveStatusTypeItem table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class InactiveStatusTypeItemSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<InactiveStatusTypeItem>().HasData(new InactiveStatusTypeItem() { Tuid = 1, Name = "Moved" });
			modelBuilder.Entity<InactiveStatusTypeItem>().HasData(new InactiveStatusTypeItem() { Tuid = 2, Name = "Terminated" });
			modelBuilder.Entity<InactiveStatusTypeItem>().HasData(new InactiveStatusTypeItem() { Tuid = 3, Name = "Resigned/Health" });
			modelBuilder.Entity<InactiveStatusTypeItem>().HasData(new InactiveStatusTypeItem() { Tuid = 4, Name = "Resigned/New Work or Interests" });
			modelBuilder.Entity<InactiveStatusTypeItem>().HasData(new InactiveStatusTypeItem() { Tuid = 5, Name = "Resigned/Other" });
			modelBuilder.Entity<InactiveStatusTypeItem>().HasData(new InactiveStatusTypeItem() { Tuid = 6, Name = "Deceased" });
			modelBuilder.Entity<InactiveStatusTypeItem>().HasData(new InactiveStatusTypeItem() { Tuid = 7, Name = "Transferred" });
			modelBuilder.Entity<InactiveStatusTypeItem>().HasData(new InactiveStatusTypeItem() { Tuid = 8, Name = "Retired" });
		}
	}
}
