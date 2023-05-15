using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for InKindDonationItem
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the InKindDonationItem table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class InKindDonationItemSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<InKindDonationTypeItem>().HasData(new InKindDonationTypeItem() { Tuid = 1, DonationTypeItemTuid = 2, Name = "48 Pint Craft", Date = DateTime.Parse("8/17/2022"), Value = 144.00});
			modelBuilder.Entity<InKindDonationTypeItem>().HasData(new InKindDonationTypeItem() { Tuid = 2, DonationTypeItemTuid = 2, Name = "Jean Willis", Date = DateTime.Parse("8/17/2022"), Value = 156.00 });
			modelBuilder.Entity<InKindDonationTypeItem>().HasData(new InKindDonationTypeItem() { Tuid = 3, DonationTypeItemTuid = 2, Name = "Holiday Candles", Date = DateTime.Parse("8/17/2022"), Value = 150.61 });
			modelBuilder.Entity<InKindDonationTypeItem>().HasData(new InKindDonationTypeItem() { Tuid = 4, DonationTypeItemTuid = 2, Name = "Kathy Chapman", Date = DateTime.Parse("8/17/2022"), Value = 145.00 });
		}
	}
}
