using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for Volunteer
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the Volunteer table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class VolunteerSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Volunteer>().HasData(new Volunteer() { 
				Tuid = 1,
				AddressTuid = 14, 
				EthnicityTuid  = 2, 
				IdentifiesAsTuid = 2, 
				RacialGroupTuid = 1, 
				GenderTuid = 2, 
				FirstName = "David", 
				LastName = "Burch", 
				Phone = "989-111-1111",
				Email = "",
				AltPhone = "", 
				Birthday = DateTime.Parse("7/30/1954"),
				IsFamilyOfMilitary = false, 
				IsVeteran = false,
				IsDeleted= false,
				StartDate = DateTime.Parse("8/1/2021")
			});

			modelBuilder.Entity<Volunteer>().HasData(new Volunteer()
			{
				Tuid = 2,
				AddressTuid = 15,
				EthnicityTuid = 2,
				IdentifiesAsTuid = 2,
				RacialGroupTuid = 1,
				GenderTuid = 2,
				FirstName = "Eddie",
				LastName = "Cahpin",
				Phone = "989-222-2222",
				Email = "fostergrand@mymail.com",
				AltPhone = "",
				Birthday = DateTime.Parse("1/24/1957"),
				IsFamilyOfMilitary = false,
				IsVeteran = false,
                IsDeleted = false,
                StartDate = DateTime.Parse("8/1/2021")
            });

			modelBuilder.Entity<Volunteer>().HasData(new Volunteer()
			{
				Tuid = 3,
				AddressTuid = 1,
				EthnicityTuid = 2,
				IdentifiesAsTuid = 2,
				RacialGroupTuid = 1,
				GenderTuid = 2,
				FirstName = "Keth",
				LastName = "Cook",
				Phone = "989-333-3333",
				Email = "",
				AltPhone = "",
				Birthday = DateTime.Parse("11/3/1952"),
				IsFamilyOfMilitary = false,
				IsVeteran = false,
                IsDeleted = false,
                StartDate = DateTime.Parse("8/1/2021")
            });

			modelBuilder.Entity<Volunteer>().HasData(new Volunteer()
			{
				Tuid = 4,
				AddressTuid = 1,
				EthnicityTuid = 2,
				IdentifiesAsTuid = 2,
				RacialGroupTuid = 2,
				GenderTuid = 2,
				FirstName = "John",
				LastName = "Depew",
				Phone = "989-444-4444",
				Email = "",
				AltPhone = "",
				Birthday = DateTime.Parse("12/16/1952"),
				IsFamilyOfMilitary = true,
				IsVeteran = false,
                IsDeleted = false,
                StartDate = DateTime.Parse("8/1/2021")
            });

			modelBuilder.Entity<Volunteer>().HasData(new Volunteer()
			{
				Tuid = 5,
				AddressTuid = 1,
				EthnicityTuid = 2,
				IdentifiesAsTuid = 2,
				RacialGroupTuid = 1,
				GenderTuid = 2,
				FirstName = "Andrew",
				LastName = "Drake",
				Phone = "989-555-5555",
				Email = "",
				AltPhone = "",
				Birthday = DateTime.Parse("11/27/1951"),
				IsFamilyOfMilitary = false,
				IsVeteran = false,
                IsDeleted = false,
                StartDate = DateTime.Parse("8/1/2021")
            });

			modelBuilder.Entity<Volunteer>().HasData(new Volunteer()
			{
				Tuid = 6,
				AddressTuid = 1,
				EthnicityTuid = 2,
				IdentifiesAsTuid = 2,
				RacialGroupTuid = 1,
				GenderTuid = 2,
				FirstName = "Sam",
				LastName = "Henery",
				Phone = "989-666-6666",
				Email = "",
				AltPhone = "",
				Birthday = DateTime.Parse("1/31/1963"),
				IsFamilyOfMilitary = false,
				IsVeteran = false,
                IsDeleted = false,
                StartDate = DateTime.Parse("8/1/2021")
            });

			modelBuilder.Entity<Volunteer>().HasData(new Volunteer()
			{
				Tuid = 7,
				AddressTuid = 1,
				EthnicityTuid = 2,
				IdentifiesAsTuid = 2,
				RacialGroupTuid = 2,
				GenderTuid = 2,
				FirstName = "Matt",
				LastName = "Horton",
				Phone = "989-777-7777",
				Email = "",
				AltPhone = "",
				Birthday = DateTime.Parse("12/5/1951"),
				IsFamilyOfMilitary = false,
				IsVeteran = false,
                IsDeleted = false,
                StartDate = DateTime.Parse("8/1/2021")
            });

			modelBuilder.Entity<Volunteer>().HasData(new Volunteer()
			{
				Tuid = 8,
				AddressTuid = 1,
				EthnicityTuid = 2,
				IdentifiesAsTuid = 2,
				RacialGroupTuid = 1,
				GenderTuid = 2,
				FirstName = "Dennis",
				LastName = "Jackson",
				Phone = "989-888-8888",
				Email = "",
				AltPhone = "",
				Birthday = DateTime.Parse("12/9/1948"),
				IsFamilyOfMilitary = false,
				IsVeteran = false,
                IsDeleted = false,
                StartDate = DateTime.Parse("8/1/2021")
            });

			modelBuilder.Entity<Volunteer>().HasData(new Volunteer()
			{
				Tuid = 9,
				AddressTuid = 1,
				EthnicityTuid = 2,
				IdentifiesAsTuid = 2,
				RacialGroupTuid = 1,
				GenderTuid = 2,
				FirstName = "Steven",
				LastName = "Jones",
				Phone = "989-999-9999",
				Email = "",
				AltPhone = "",
				Birthday = DateTime.Parse("12/12/1948"),
				IsFamilyOfMilitary = false,
				IsVeteran = false,
                IsDeleted = false,
                StartDate = DateTime.Parse("8/1/2021")
            });

			modelBuilder.Entity<Volunteer>().HasData(new Volunteer()
			{
				Tuid = 10,
				AddressTuid = 1,
				EthnicityTuid = 2,
				IdentifiesAsTuid = 2,
				RacialGroupTuid = 1,
				GenderTuid = 2,
				FirstName = "Don",
				LastName = "Landers",
				Phone = "989-111-1110",
				Email = "",
				AltPhone = "",
				Birthday = DateTime.Parse("10/10/1939"),
				IsFamilyOfMilitary = false,
				IsVeteran = true,
                IsDeleted = false,
                StartDate = DateTime.Parse("8/1/2021")
            });
		}
	}
}
