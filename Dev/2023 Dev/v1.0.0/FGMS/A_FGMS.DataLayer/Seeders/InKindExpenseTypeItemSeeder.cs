using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for InKindExpenseTypeItem
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the InKindExpenseTypeItem table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class InKindExpenseTypeItemSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<InKindExpenseTypeItem>().HasData(new InKindExpenseTypeItem() { Tuid = 1, ExpenseTypeItemTuid = 2, Name = "5 Cases", Date = DateTime.Parse("8/1/2022 9:30:52 AM"), Value = 100 });
			modelBuilder.Entity<InKindExpenseTypeItem>().HasData(new InKindExpenseTypeItem() { Tuid = 2, ExpenseTypeItemTuid = 2, Name = "5 Cases", Date = DateTime.Parse("5/5/2022 8:30:52 AM"), Value = 100 });
			modelBuilder.Entity<InKindExpenseTypeItem>().HasData(new InKindExpenseTypeItem() { Tuid = 3, ExpenseTypeItemTuid = 2, Name = "5 Cases", Date = DateTime.Parse("2/1/2022 7:30:52 PM"), Value = 100 });
			modelBuilder.Entity<InKindExpenseTypeItem>().HasData(new InKindExpenseTypeItem() { Tuid = 4, ExpenseTypeItemTuid = 2, Name = "5 Cases", Date = DateTime.Parse("2/15/2022 7:30:52 AM"), Value = 100 });
			modelBuilder.Entity<InKindExpenseTypeItem>().HasData(new InKindExpenseTypeItem() { Tuid = 5, ExpenseTypeItemTuid = 2, Name = "5 Cases", Date = DateTime.Parse("12/5/2022 8:30:52 AM"), Value = 100 });
			modelBuilder.Entity<InKindExpenseTypeItem>().HasData(new InKindExpenseTypeItem() { Tuid = 6, ExpenseTypeItemTuid = 2, Name = "5 Cases", Date = DateTime.Parse("11/18/2022 4:30:52 AM"), Value = 100 });
			modelBuilder.Entity<InKindExpenseTypeItem>().HasData(new InKindExpenseTypeItem() { Tuid = 7, ExpenseTypeItemTuid = 2, Name = "5 Cases", Date = DateTime.Parse("5/1/2022 11:30:52 PM"), Value = 100 });
			modelBuilder.Entity<InKindExpenseTypeItem>().HasData(new InKindExpenseTypeItem() { Tuid = 8, ExpenseTypeItemTuid = 2, Name = "5 Cases", Date = DateTime.Parse("7/15/2022 10:30:52 AM"), Value = 100 });
			modelBuilder.Entity<InKindExpenseTypeItem>().HasData(new InKindExpenseTypeItem() { Tuid = 9, ExpenseTypeItemTuid = 2, Name = "5 Cases", Date = DateTime.Parse("1/1/2022 8:30:52 PM"), Value = 100 });
		}
	}
}
