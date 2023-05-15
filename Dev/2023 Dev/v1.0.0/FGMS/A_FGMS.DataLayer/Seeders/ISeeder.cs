using Microsoft.EntityFrameworkCore;

/// <summary>
/// Interface for seeder objects
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Interface for seeder objects
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public interface ISeeder
	{
		void SeedData(ModelBuilder modelBuilder);
	}
}
