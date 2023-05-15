using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for Users
/// </summary>
/// <author>Richard Nader, Jr.</author>
/// <created>4/9/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the Users table
    /// </summary>
    /// <author>Richard Nader, Jr.</author>
    /// <created>4/9/2023</created>
    public class UsersSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasData(new User() { 
                Tuid = 1, 
                Name = "SVSU Developer", 
                Email = "svsu.developer@svsu.edu", 
                PhoneNumber = "(989) 964-4000", 
                HashedPassword = "AAAAAAIAAYagAAAAEOzgQmjJNUK60RAtv8EeQmEZ3q++aBgIhrVLdpHk6ywewdwf5U5eFiVPnK242weLOw==", 
                IsActive = true, 
                IsAdmin = true, 
                IsDeleted = false,
                IsReadOnly = true
            });
		}
	}
}
