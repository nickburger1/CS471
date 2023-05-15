using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for School
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the School table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class SchoolSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<School>().HasData(new School() { Tuid = 1, AddressTuid = 1, Name = "Arthur Eddy Academy", Principal = "Janice Davis", Secretary = "Ms. Aday", ContactNumber = "(989) 399-4300", StartTime = "8:25", EndTime = "3:25", IsActive = true, Days = "12345" });
			modelBuilder.Entity<School>().HasData(new School() { Tuid = 2, AddressTuid = 2, Name = "Handley Elementary", Principal = "Julie Miller", Secretary = "Lori Menapace", ContactNumber = "(989) 399-5100", StartTime = "8:25", EndTime = "3:25", IsActive = true, Days = "12345" });
			modelBuilder.Entity<School>().HasData(new School() { Tuid = 3, AddressTuid = 3, Name = "Stone Elementary", Principal = "Joseph Wamback", Secretary = "", ContactNumber = "(989) 399-5100", StartTime = "8:25", EndTime = "3:15", IsActive = true, Days = "12345" });
			modelBuilder.Entity<School>().HasData(new School() { Tuid = 4, AddressTuid = 4, Name = "Jessie Rouse Elementary", Principal = "Amanda Kitterman", Secretary = "Vicki Trejo", ContactNumber = "(989) 399-5000", StartTime = "8:25", EndTime = "3:25", IsActive = true, Days = "12345" });
			modelBuilder.Entity<School>().HasData(new School() { Tuid = 5, AddressTuid = 5, Name = "Kempton Elementary", Principal = "Dana Heyl", Secretary = "Jackie Collins", ContactNumber = "(989) 399-4600", StartTime = "8:25", EndTime = "3:25", IsActive = true, Days = "12345" });
			modelBuilder.Entity<School>().HasData(new School() { Tuid = 6, AddressTuid = 6, Name = "Thomas White Elementary", Principal = "Emily Redewahn", Secretary = "", ContactNumber = "(989) 777-2811", StartTime = "8:30", EndTime = "3:45", IsActive = true, Days = "12345" });
			modelBuilder.Entity<School>().HasData(new School() { Tuid = 7, AddressTuid = 7, Name = "Carrollton Elementary", Principal = "Sarah Coates", Secretary = "Allie Davenport", ContactNumber = "(989) 754-2425", StartTime = "7:45", EndTime = "2:55", IsActive = true, Days = "12345" });
			modelBuilder.Entity<School>().HasData(new School() { Tuid = 8, AddressTuid = 8, Name = "Growing Years", Principal = "", Secretary = "", ContactNumber = "(989) 792-8670", StartTime = "6:30", EndTime = "6:00", IsActive = true, Days = "12345" });
			modelBuilder.Entity<School>().HasData(new School() { Tuid = 9, AddressTuid = 9, Name = "Saginaw Prep", Principal = "James Kenny", Secretary = "", ContactNumber = "(989) 792-8670", StartTime = "6:30", EndTime = "6:00", IsActive = true, Days = "12345" });
			modelBuilder.Entity<School>().HasData(new School() { Tuid = 10, AddressTuid = 10, Name = "Francis Reh Acadamy", Principal = "Katie Scheid Weber", Secretary = "", ContactNumber = "(989) 753-2349", StartTime = "8:00", EndTime = "3:50", IsActive = true, Days = "12345" });
			modelBuilder.Entity<School>().HasData(new School() { Tuid = 11, AddressTuid = 11, Name = "Sherwood Elementary", Principal = "Mark Abenth", Secretary = "Amy Greenwood", ContactNumber = "(989) 799-7382", StartTime = "7:40", EndTime = "2:40", IsActive = true, Days = "12345" });
			modelBuilder.Entity<School>().HasData(new School() { Tuid = 12, AddressTuid = 12, Name = "Saginaw Academy of Excellence", Principal = "Helen Jiles", Secretary = "Anitra Frazier", ContactNumber = "(989) 799-7382", StartTime = "8:00", EndTime = "3:30", IsActive = true, Days = "12345" });
			modelBuilder.Entity<School>().HasData(new School() { Tuid = 13, AddressTuid = 13, Name = "Washington", Principal = "Dr. Sara Moore", Secretary = "Michele Kibue", ContactNumber = "(989) 894-2744", StartTime = "8:00", EndTime = "3:30", IsActive = true, Days = "12345" });
		}
	}
}
