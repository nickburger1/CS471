using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for PTOStipendRate
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the PTOStipendRate table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class PTOStipendRateSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PTOStipendRate>().HasData(new PTOStipendRate() { Tuid = 1, PTORate = 1.00, StipendRate = 2.00, Date = DateTime.Parse("1/1/2022") });
			modelBuilder.Entity<PTOStipendRate>().HasData(new PTOStipendRate() { Tuid = 2, PTORate = 1.50, StipendRate = 2.00, Date = DateTime.Parse("12/1/2022") });
			modelBuilder.Entity<PTOStipendRate>().HasData(new PTOStipendRate() { Tuid = 3, PTORate = 1.50, StipendRate = 5.00, Date = DateTime.Parse("10/1/2022") });
		}
	}
}
