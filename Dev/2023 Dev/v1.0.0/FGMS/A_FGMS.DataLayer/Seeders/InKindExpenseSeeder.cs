using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for InKindExpense
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the InKindExpense table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class InKindExpenseSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<InKindExpense>().HasData(new InKindExpense() { Tuid = 1, ExpenseTypeTuid = 1, VolunteerTuid = 1, Date = DateTime.Parse("8/17/2021"), Value = 100 });
			modelBuilder.Entity<InKindExpense>().HasData(new InKindExpense() { Tuid = 2, ExpenseTypeTuid = 1, VolunteerTuid = 2, Date = DateTime.Parse("8/17/2021"), Value = 100 });
			modelBuilder.Entity<InKindExpense>().HasData(new InKindExpense() { Tuid = 3, ExpenseTypeTuid = 1, VolunteerTuid = 3, Date = DateTime.Parse("8/17/2021"), Value = 100 });
			modelBuilder.Entity<InKindExpense>().HasData(new InKindExpense() { Tuid = 4, ExpenseTypeTuid = 1, VolunteerTuid = 4, Date = DateTime.Parse("8/17/2021"), Value = 100 });
			modelBuilder.Entity<InKindExpense>().HasData(new InKindExpense() { Tuid = 5, ExpenseTypeTuid = 1, VolunteerTuid = 5, Date = DateTime.Parse("8/17/2021"), Value = 100 });
			modelBuilder.Entity<InKindExpense>().HasData(new InKindExpense() { Tuid = 6, ExpenseTypeTuid = 1, VolunteerTuid = 6, Date = DateTime.Parse("8/17/2021"), Value = 100 });
			modelBuilder.Entity<InKindExpense>().HasData(new InKindExpense() { Tuid = 7, ExpenseTypeTuid = 1, VolunteerTuid = 7, Date = DateTime.Parse("8/17/2021"), Value = 100 });
			modelBuilder.Entity<InKindExpense>().HasData(new InKindExpense() { Tuid = 8, ExpenseTypeTuid = 1, VolunteerTuid = 8, Date = DateTime.Parse("8/17/2021"), Value = 100 });
			modelBuilder.Entity<InKindExpense>().HasData(new InKindExpense() { Tuid = 9, ExpenseTypeTuid = 1, VolunteerTuid = 9, Date = DateTime.Parse("8/17/2021"), Value = 100 });
			modelBuilder.Entity<InKindExpense>().HasData(new InKindExpense() { Tuid = 10, ExpenseTypeTuid = 1, VolunteerTuid = 10, Date = DateTime.Parse("8/17/2021"), Value = 100 });
			modelBuilder.Entity<InKindExpense>().HasData(new InKindExpense() { Tuid = 11, ExpenseTypeTuid = 1, VolunteerTuid = 1, Date = DateTime.Parse("8/17/2022"), Value = 100 });
			modelBuilder.Entity<InKindExpense>().HasData(new InKindExpense() { Tuid = 12, ExpenseTypeTuid = 1, VolunteerTuid = 2, Date = DateTime.Parse("8/17/2022"), Value = 100 });
			modelBuilder.Entity<InKindExpense>().HasData(new InKindExpense() { Tuid = 13, ExpenseTypeTuid = 1, VolunteerTuid = 3, Date = DateTime.Parse("8/17/2022"), Value = 100 });
			modelBuilder.Entity<InKindExpense>().HasData(new InKindExpense() { Tuid = 14, ExpenseTypeTuid = 1, VolunteerTuid = 4, Date = DateTime.Parse("8/17/2022"), Value = 100 });
			modelBuilder.Entity<InKindExpense>().HasData(new InKindExpense() { Tuid = 15, ExpenseTypeTuid = 1, VolunteerTuid = 5, Date = DateTime.Parse("8/17/2022"), Value = 100 });
			modelBuilder.Entity<InKindExpense>().HasData(new InKindExpense() { Tuid = 16, ExpenseTypeTuid = 1, VolunteerTuid = 6, Date = DateTime.Parse("8/17/2022"), Value = 100 });
			modelBuilder.Entity<InKindExpense>().HasData(new InKindExpense() { Tuid = 17, ExpenseTypeTuid = 1, VolunteerTuid = 7, Date = DateTime.Parse("8/17/2022"), Value = 100 });
			modelBuilder.Entity<InKindExpense>().HasData(new InKindExpense() { Tuid = 18, ExpenseTypeTuid = 1, VolunteerTuid = 8, Date = DateTime.Parse("8/17/2022"), Value = 100 });
			modelBuilder.Entity<InKindExpense>().HasData(new InKindExpense() { Tuid = 19, ExpenseTypeTuid = 1, VolunteerTuid = 9, Date = DateTime.Parse("8/17/2022"), Value = 100 });
			modelBuilder.Entity<InKindExpense>().HasData(new InKindExpense() { Tuid = 20, ExpenseTypeTuid = 1, VolunteerTuid = 10, Date = DateTime.Parse("8/17/2022"), Value = 100 });
		}
	}
}
