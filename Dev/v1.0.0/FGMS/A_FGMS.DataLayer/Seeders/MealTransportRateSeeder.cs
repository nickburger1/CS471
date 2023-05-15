using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for MealTransportRate
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the MealTransportRate table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class MealTransportRateSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<MealTransportRate>().HasData(new MealTransportRate() { Tuid = 1, MealRate = 1.00, MileageRate = 1.00, BusMileageRate = 1.25, Date = DateTime.Parse("1/1/2022") });
			modelBuilder.Entity<MealTransportRate>().HasData(new MealTransportRate() { Tuid = 2, MealRate = 2.00, MileageRate = 1.00, BusMileageRate = 1.25, Date = DateTime.Parse("5/1/2022") });
			modelBuilder.Entity<MealTransportRate>().HasData(new MealTransportRate() { Tuid = 3, MealRate = 2.00, MileageRate = 3.00, BusMileageRate = 1.25, Date = DateTime.Parse("10/1/2022") });
		}
	}
}
