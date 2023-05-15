using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for AddressSeeder
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the Address table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class AddressSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Address>().HasData(new Address() { Tuid = 1, AddressLine1 = "1000 Cathay", AddressLine2 = "", City = "Saginaw", State = "MI", Zipcode = "48601" }); //Authur Eddy Academny
			modelBuilder.Entity<Address>().HasData(new Address() { Tuid = 2, AddressLine1 = "224 N. Elm St.", AddressLine2 = "", City = "Saginaw", State = "MI", Zipcode = "48602" }); //Handley Elementary
			modelBuilder.Entity<Address>().HasData(new Address() { Tuid = 3, AddressLine1 = "1006 State St.", AddressLine2 = "", City = "Saginaw", State = "MI", Zipcode = "48602" }); //Stone Elementary
			modelBuilder.Entity<Address>().HasData(new Address() { Tuid = 4, AddressLine1 = "435 Randolph", AddressLine2 = "", City = "Saginaw", State = "MI", Zipcode = "48601" }); //Jessie Rouse Elm.
			modelBuilder.Entity<Address>().HasData(new Address() { Tuid = 5, AddressLine1 = "3040 Davenport", AddressLine2 = "", City = "Saginaw", State = "MI", Zipcode = "48602" }); //Kempton Elementary
			modelBuilder.Entity<Address>().HasData(new Address() { Tuid = 6, AddressLine1 = "3650 Southfield Dr", AddressLine2 = "", City = "Saginaw", State = "MI", Zipcode = "48601" }); //Bridgeport
			modelBuilder.Entity<Address>().HasData(new Address() { Tuid = 7, AddressLine1 = "3211 Carla Dr", AddressLine2 = "", City = "Carrollton", State = "MI", Zipcode = "48724" }); //Carrolton
			modelBuilder.Entity<Address>().HasData(new Address() { Tuid = 8, AddressLine1 = "2190 Wieneke", AddressLine2 = "", City = "Tittabawasee Township", State = "MI", Zipcode = "48603" }); //Growing Years
			modelBuilder.Entity<Address>().HasData(new Address() { Tuid = 9, AddressLine1 = "5173 Lodge", AddressLine2 = "", City = "Saginaw", State = "MI", Zipcode = "48601" }); //Saginaw Prep
			modelBuilder.Entity<Address>().HasData(new Address() { Tuid = 10, AddressLine1 = "2201 Owen St", AddressLine2 = "", City = "Saginaw", State = "MI", Zipcode = "48601" }); //Francis Reh Academy
			modelBuilder.Entity<Address>().HasData(new Address() { Tuid = 11, AddressLine1 = "3870 Shattuck Rd", AddressLine2 = "", City = "Tittabawasee Township", State = "MI", Zipcode = "48603" }); //Sherwood Elementary
			modelBuilder.Entity<Address>().HasData(new Address() { Tuid = 12, AddressLine1 = "1300 Malzahn St.", AddressLine2 = "", City = "Saginaw", State = "MI", Zipcode = "48602" }); //Sag. Acad. of Excellence
			modelBuilder.Entity<Address>().HasData(new Address() { Tuid = 13, AddressLine1 = "1821 Mckinley St BC", AddressLine2 = "", City = "Saginaw", State = "MI", Zipcode = "48708" }); //Sag. Acad. of Excellence
			modelBuilder.Entity<Address>().HasData(new Address() { Tuid = 14, AddressLine1 = "", AddressLine2 = "", City = "Saginaw", State = "MI", Zipcode = "48601" }); //David Burch
			modelBuilder.Entity<Address>().HasData(new Address() { Tuid = 15, AddressLine1 = "", AddressLine2 = "", City = "Saginaw", State = "MI", Zipcode = "48602" }); //Eddie Chapin
			modelBuilder.Entity<Address>().HasData(new Address() { Tuid = 16, AddressLine1 = "", AddressLine2 = "", City = "Saginaw", State = "MI", Zipcode = "48602" }); //Keith Cook
			modelBuilder.Entity<Address>().HasData(new Address() { Tuid = 17, AddressLine1 = "", AddressLine2 = "", City = "Saginaw", State = "MI", Zipcode = "48601" }); //John Depew
			modelBuilder.Entity<Address>().HasData(new Address() { Tuid = 18, AddressLine1 = "", AddressLine2 = "", City = "Saginaw", State = "MI", Zipcode = "48601" }); //Andrew Drake
			modelBuilder.Entity<Address>().HasData(new Address() { Tuid = 19, AddressLine1 = "123 Mary Street", AddressLine2 = "", City = "Saginaw", State = "MI", Zipcode = "48601" }); //Sam Henry
			modelBuilder.Entity<Address>().HasData(new Address() { Tuid = 20, AddressLine1 = "234 Sandra Rd", AddressLine2 = "", City = "Saginaw", State = "MI", Zipcode = "48602" }); //Matt Horton
			modelBuilder.Entity<Address>().HasData(new Address() { Tuid = 21, AddressLine1 = "456 Linda Blvd", AddressLine2 = "", City = "Saginaw", State = "MI", Zipcode = "48604" }); //Dennis Jackson
			modelBuilder.Entity<Address>().HasData(new Address() { Tuid = 22, AddressLine1 = "", AddressLine2 = "", City = "Saginaw", State = "MI", Zipcode = "48601" }); //Steven Jones
			modelBuilder.Entity<Address>().HasData(new Address() { Tuid = 23, AddressLine1 = "", AddressLine2 = "", City = "Saginaw", State = "MI", Zipcode = "48601" }); //Don Landers
		}
	}
}
