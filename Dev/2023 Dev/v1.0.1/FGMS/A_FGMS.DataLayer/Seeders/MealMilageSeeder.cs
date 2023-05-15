using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for MeaMilage
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the MealMilage table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class MealMilageSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<MealMileage>().HasData(new MealMileage()
			{
				Tuid = 1,
				VolunteerTuid = 1,
				BusRideCount = 9,
				MealCount = 13,
				Mileage = 78,
				Date = DateTime.Parse("3/2/2021")
			});
			modelBuilder.Entity<MealMileage>().HasData(new MealMileage()
			{
				Tuid = 2,
				VolunteerTuid = 2,
				BusRideCount = 65,
				MealCount = 69,
				Mileage = 30,
				Date = DateTime.Parse("3/2/2021")
			});
			modelBuilder.Entity<MealMileage>().HasData(new MealMileage()
			{
				Tuid = 3,
				VolunteerTuid = 3,
				BusRideCount = 41,
				MealCount = 45,
				Mileage = 80,
				Date = DateTime.Parse("3/2/2021")
			});
			modelBuilder.Entity<MealMileage>().HasData(new MealMileage()
			{
				Tuid = 4,
				VolunteerTuid = 4,
				BusRideCount = 78,
				MealCount = 9,
				Mileage = 59,
				Date = DateTime.Parse("3/2/2021")
			});
			modelBuilder.Entity<MealMileage>().HasData(new MealMileage()
			{
				Tuid = 5,
				VolunteerTuid = 5,
				BusRideCount = 5,
				MealCount = 51,
				Mileage = 64,
				Date = DateTime.Parse("3/2/2021")
			});
			modelBuilder.Entity<MealMileage>().HasData(new MealMileage()
			{
				Tuid = 6,
				VolunteerTuid = 6,
				BusRideCount = 89,
				MealCount = 56,
				Mileage = 22,
				Date = DateTime.Parse("3/2/2021")
			});
			modelBuilder.Entity<MealMileage>().HasData(new MealMileage()
			{
				Tuid = 7,
				VolunteerTuid = 7,
				BusRideCount = 6,
				MealCount = 35,
				Mileage = 28,
				Date = DateTime.Parse("3/2/2021")
			});
			modelBuilder.Entity<MealMileage>().HasData(new MealMileage()
			{
				Tuid = 8,
				VolunteerTuid = 8,
				BusRideCount = 0,
				MealCount = 5,
				Mileage = 45,
				Date = DateTime.Parse("3/2/2021")
			});
			modelBuilder.Entity<MealMileage>().HasData(new MealMileage()
			{
				Tuid = 9,
				VolunteerTuid = 9,
				BusRideCount = 48,
				MealCount = 90,
				Mileage = 8,
				Date = DateTime.Parse("3/2/2021")
			});
			modelBuilder.Entity<MealMileage>().HasData(new MealMileage()
			{
				Tuid = 10,
				VolunteerTuid = 10,
				BusRideCount = 38,
				MealCount = 13,
				Mileage = 34,
				Date = DateTime.Parse("3/2/2021")
			});
			modelBuilder.Entity<MealMileage>().HasData(new MealMileage()
			{
				Tuid = 11,
				VolunteerTuid = 1,
				BusRideCount = 45,
				MealCount = 36,
				Mileage = 64,
				Date = DateTime.Parse("3/2/2022")
			});
			modelBuilder.Entity<MealMileage>().HasData(new MealMileage()
			{
				Tuid = 12,
				VolunteerTuid = 2,
				BusRideCount = 53,
				MealCount = 10,
				Mileage = 77,
				Date = DateTime.Parse("3/2/2022")
			});
			modelBuilder.Entity<MealMileage>().HasData(new MealMileage()
			{
				Tuid = 13,
				VolunteerTuid = 3,
				BusRideCount = 37,
				MealCount = 57,
				Mileage = 5,
				Date = DateTime.Parse("3/2/2022")
			});
			modelBuilder.Entity<MealMileage>().HasData(new MealMileage()
			{
				Tuid = 14,
				VolunteerTuid = 4,
				BusRideCount = 30,
				MealCount = 89,
				Mileage = 73,
				Date = DateTime.Parse("3/2/2022")
			});
			modelBuilder.Entity<MealMileage>().HasData(new MealMileage()
			{
				Tuid = 15,
				VolunteerTuid = 5,
				BusRideCount = 3,
				MealCount = 92,
				Mileage = 85,
				Date = DateTime.Parse("3/2/2022")
			});
			modelBuilder.Entity<MealMileage>().HasData(new MealMileage()
			{
				Tuid = 16,
				VolunteerTuid = 6,
				BusRideCount = 5,
				MealCount = 82,
				Mileage = 43,
				Date = DateTime.Parse("3/2/2022")
			});
			modelBuilder.Entity<MealMileage>().HasData(new MealMileage()
			{
				Tuid = 17,
				VolunteerTuid = 7,
				BusRideCount = 92,
				MealCount = 32,
				Mileage = 56,
				Date = DateTime.Parse("3/2/2022")
			});
			modelBuilder.Entity<MealMileage>().HasData(new MealMileage()
			{
				Tuid = 18,
				VolunteerTuid = 8,
				BusRideCount = 65,
				MealCount = 18,
				Mileage = 74,
				Date = DateTime.Parse("3/2/2022")
			});
			modelBuilder.Entity<MealMileage>().HasData(new MealMileage()
			{
				Tuid = 19,
				VolunteerTuid = 9,
				BusRideCount = 6,
				MealCount = 63,
				Mileage = 71,
				Date = DateTime.Parse("3/2/2022")
			});
			modelBuilder.Entity<MealMileage>().HasData(new MealMileage()
			{
				Tuid = 20,
				VolunteerTuid = 10,
				BusRideCount = 41,
				MealCount = 94,
				Mileage = 67,
				Date = DateTime.Parse("3/2/2022")
			});
		}
	}
}
