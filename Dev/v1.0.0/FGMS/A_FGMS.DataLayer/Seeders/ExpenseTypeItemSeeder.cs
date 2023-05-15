using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for ExpenseTypeItem
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the ExpenseTypeItem table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class ExpenseTypeItemSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ExpenseTypeItem>().HasData(new ExpenseTypeItem() { Tuid = 1, Name = "Physicals", Description = "" });
			modelBuilder.Entity<ExpenseTypeItem>().HasData(new ExpenseTypeItem() { Tuid = 2, Name = "Paper", Description = "" });
		}
	}
}
