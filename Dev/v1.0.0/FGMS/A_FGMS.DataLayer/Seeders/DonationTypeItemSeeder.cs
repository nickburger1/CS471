using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for DonationTypeItem
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the DonationTypeItem table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class DonationTypeItemSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<DonationTypeItem>().HasData(new DonationTypeItem() { Tuid = 1, Name = "Corporate Donation", Description = ""});
			modelBuilder.Entity<DonationTypeItem>().HasData(new DonationTypeItem() { Tuid = 2, Name = "FGP Gifts", Description = "" });
		}
	}
}
