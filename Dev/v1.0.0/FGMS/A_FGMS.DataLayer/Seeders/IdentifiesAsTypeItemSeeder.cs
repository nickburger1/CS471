using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for IdentifiesAsTypeItem
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the IdentifiesAsTypeItem table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class IdentifiesAsTypeItemSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<IdentifiesAsTypeItem>().HasData(new IdentifiesAsTypeItem() { Tuid = 1, Name = "Woman" });
			modelBuilder.Entity<IdentifiesAsTypeItem>().HasData(new IdentifiesAsTypeItem() { Tuid = 2, Name = "Man" });
			modelBuilder.Entity<IdentifiesAsTypeItem>().HasData(new IdentifiesAsTypeItem() { Tuid = 3, Name = "Non-binary" });
			modelBuilder.Entity<IdentifiesAsTypeItem>().HasData(new IdentifiesAsTypeItem() { Tuid = 4, Name = "Undeclared" });
		}
	}
}
