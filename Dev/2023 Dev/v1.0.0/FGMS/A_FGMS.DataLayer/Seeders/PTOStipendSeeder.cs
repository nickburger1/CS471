using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for PTOStipend
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the PTOStipend table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class PTOStipendSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PTOStipend>().HasData(new PTOStipend()
			{
				Tuid = 1,
				VolunteerTuid = 1,
				RegularHours = 40,
				PtoUsed = 0,
				PtoEarned = 0,
				PtoStart = 0,
				PtoEnd = 0,
				StipendPaid = 0,
				YearToDateHour = 0,
				Date = DateTime.Parse("8/1/2021")
			});
			modelBuilder.Entity<PTOStipend>().HasData(new PTOStipend()
			{
				Tuid = 2,
				VolunteerTuid = 2,
				RegularHours = 40,
				PtoUsed = 0,
				PtoEarned = 0,
				PtoStart = 0,
				PtoEnd = 0,
				StipendPaid = 0,
				YearToDateHour = 0,
				Date = DateTime.Parse("8/1/2021")
			});
			modelBuilder.Entity<PTOStipend>().HasData(new PTOStipend()
			{
				Tuid = 3,
				VolunteerTuid = 3,
				RegularHours = 40,
				PtoUsed = 0,
				PtoEarned = 0,
				PtoStart = 0,
				PtoEnd = 0,
				StipendPaid = 0,
				YearToDateHour = 0,
				Date = DateTime.Parse("8/1/2021")
			});
			modelBuilder.Entity<PTOStipend>().HasData(new PTOStipend()
			{
				Tuid = 4,
				VolunteerTuid = 4,
				RegularHours = 40,
				PtoUsed = 0,
				PtoEarned = 0,
				PtoStart = 0,
				PtoEnd = 0,
				StipendPaid = 0,
				YearToDateHour = 0,
				Date = DateTime.Parse("8/1/2021")
			});
			modelBuilder.Entity<PTOStipend>().HasData(new PTOStipend()
			{
				Tuid = 5,
				VolunteerTuid = 5,
				RegularHours = 40,
				PtoUsed = 0,
				PtoEarned = 0,
				PtoStart = 0,
				PtoEnd = 0,
				StipendPaid = 0,
				YearToDateHour = 0,
				Date = DateTime.Parse("8/1/2021")
			});
			modelBuilder.Entity<PTOStipend>().HasData(new PTOStipend()
			{
				Tuid = 6,
				VolunteerTuid = 6,
				RegularHours = 40,
				PtoUsed = 0,
				PtoEarned = 0,
				PtoStart = 0,
				PtoEnd = 0,
				StipendPaid = 0,
				YearToDateHour = 0,
				Date = DateTime.Parse("8/1/2021")
			});
			modelBuilder.Entity<PTOStipend>().HasData(new PTOStipend()
			{
				Tuid = 7,
				VolunteerTuid = 7,
				RegularHours = 40,
				PtoUsed = 0,
				PtoEarned = 0,
				PtoStart = 0,
				PtoEnd = 0,
				StipendPaid = 0,
				YearToDateHour = 0,
				Date = DateTime.Parse("8/1/2021")
			});
			modelBuilder.Entity<PTOStipend>().HasData(new PTOStipend()
			{
				Tuid = 8,
				VolunteerTuid = 8,
				RegularHours = 40,
				PtoUsed = 0,
				PtoEarned = 0,
				PtoStart = 0,
				PtoEnd = 0,
				StipendPaid = 0,
				YearToDateHour = 0,
				Date = DateTime.Parse("8/1/2021")
			});
			modelBuilder.Entity<PTOStipend>().HasData(new PTOStipend()
			{
				Tuid = 9,
				VolunteerTuid = 9,
				RegularHours = 40,
				PtoUsed = 0,
				PtoEarned = 0,
				PtoStart = 0,
				PtoEnd = 0,
				StipendPaid = 0,
				YearToDateHour = 0,
				Date = DateTime.Parse("8/1/2021")
			});
			modelBuilder.Entity<PTOStipend>().HasData(new PTOStipend()
			{
				Tuid = 10,
				VolunteerTuid = 10,
				RegularHours = 40,
				PtoUsed = 0,
				PtoEarned = 0,
				PtoStart = 0,
				PtoEnd = 0,
				StipendPaid = 0,
				YearToDateHour = 0,
				Date = DateTime.Parse("8/1/2021")
			});
			modelBuilder.Entity<PTOStipend>().HasData(new PTOStipend()
			{
				Tuid = 11,
				VolunteerTuid = 1,
				RegularHours = 40,
				PtoUsed = 0,
				PtoEarned = 0,
				PtoStart = 0,
				PtoEnd = 0,
				StipendPaid = 0,
				YearToDateHour = 0,
				Date = DateTime.Parse("8/1/2022")
			});
			modelBuilder.Entity<PTOStipend>().HasData(new PTOStipend()
			{
				Tuid = 12,
				VolunteerTuid = 2,
				RegularHours = 40,
				PtoUsed = 0,
				PtoEarned = 0,
				PtoStart = 0,
				PtoEnd = 0,
				StipendPaid = 0,
				YearToDateHour = 0,
				Date = DateTime.Parse("8/1/2022")
			});
			modelBuilder.Entity<PTOStipend>().HasData(new PTOStipend()
			{
				Tuid = 13,
				VolunteerTuid = 3,
				RegularHours = 40,
				PtoUsed = 0,
				PtoEarned = 0,
				PtoStart = 0,
				PtoEnd = 0,
				StipendPaid = 0,
				YearToDateHour = 0,
				Date = DateTime.Parse("8/1/2022")
			});
			modelBuilder.Entity<PTOStipend>().HasData(new PTOStipend()
			{
				Tuid = 14,
				VolunteerTuid = 4,
				RegularHours = 40,
				PtoUsed = 0,
				PtoEarned = 0,
				PtoStart = 0,
				PtoEnd = 0,
				StipendPaid = 0,
				YearToDateHour = 0,
				Date = DateTime.Parse("8/1/2022")
			});
			modelBuilder.Entity<PTOStipend>().HasData(new PTOStipend()
			{
				Tuid = 15,
				VolunteerTuid = 5,
				RegularHours = 40,
				PtoUsed = 0,
				PtoEarned = 0,
				PtoStart = 0,
				PtoEnd = 0,
				StipendPaid = 0,
				YearToDateHour = 0,
				Date = DateTime.Parse("8/1/2022")
			});
			modelBuilder.Entity<PTOStipend>().HasData(new PTOStipend()
			{
				Tuid = 16,
				VolunteerTuid = 6,
				RegularHours = 40,
				PtoUsed = 0,
				PtoEarned = 0,
				PtoStart = 0,
				PtoEnd = 0,
				StipendPaid = 0,
				YearToDateHour = 0,
				Date = DateTime.Parse("8/1/2022")
			});
			modelBuilder.Entity<PTOStipend>().HasData(new PTOStipend()
			{
				Tuid = 17,
				VolunteerTuid = 7,
				RegularHours = 40,
				PtoUsed = 0,
				PtoEarned = 0,
				PtoStart = 0,
				PtoEnd = 0,
				StipendPaid = 0,
				YearToDateHour = 0,
				Date = DateTime.Parse("8/1/2022")
			});
			modelBuilder.Entity<PTOStipend>().HasData(new PTOStipend()
			{
				Tuid = 18,
				VolunteerTuid = 8,
				RegularHours = 40,
				PtoUsed = 0,
				PtoEarned = 0,
				PtoStart = 0,
				PtoEnd = 0,
				StipendPaid = 0,
				YearToDateHour = 0,
				Date = DateTime.Parse("8/1/2022")
			});
			modelBuilder.Entity<PTOStipend>().HasData(new PTOStipend()
			{
				Tuid = 19,
				VolunteerTuid = 9,
				RegularHours = 40,
				PtoUsed = 0,
				PtoEarned = 0,
				PtoStart = 0,
				PtoEnd = 0,
				StipendPaid = 0,
				YearToDateHour = 0,
				Date = DateTime.Parse("8/1/2022")
			});
			modelBuilder.Entity<PTOStipend>().HasData(new PTOStipend()
			{
				Tuid = 20,
				VolunteerTuid = 10,
				RegularHours = 40,
				PtoUsed = 0,
				PtoEarned = 0,
				PtoStart = 0,
				PtoEnd = 0,
				StipendPaid = 0,
				YearToDateHour = 0,
				Date = DateTime.Parse("8/1/2022")
			});
		}
	}
}
