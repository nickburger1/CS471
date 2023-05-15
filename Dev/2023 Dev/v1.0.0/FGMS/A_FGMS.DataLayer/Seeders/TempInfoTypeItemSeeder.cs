using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for TypeInfoTypeItem
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the TempInfoTypeItem table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class TempInfoTypeItemSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<TempInfoTypeItem>().HasData(new TempInfoTypeItem() { 
				Tuid = 1, 
				TempInfoType = TempInfoTypes.Boolean, 
				Name = "20-21 Covid Statement of Understanding", 
				Description = "" 
			});
		}
	}
}
