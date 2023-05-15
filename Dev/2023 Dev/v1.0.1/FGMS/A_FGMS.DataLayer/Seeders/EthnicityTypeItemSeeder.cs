using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for EthnicityTypeItem
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the EthnicityTypeItem table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class EthnicityTypeItemSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<EthnicityTypeItem>().HasData(new EthnicityTypeItem() { Tuid = 1, Name = "Hispanic" });
			modelBuilder.Entity<EthnicityTypeItem>().HasData(new EthnicityTypeItem() { Tuid = 2, Name = "Non-Hispanic" });
		}
	}
}
