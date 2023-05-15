using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for ConditionItem
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the ConditionItem table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class ConditionItemSeeder : ISeeder
    {
        public void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConditionItem>().HasData(new ConditionItem() { Tuid = 1, Acronym = "AD", Description = "Attention Difficulties" });
            modelBuilder.Entity<ConditionItem>().HasData(new ConditionItem() { Tuid = 2, Acronym = "AN", Description = "Abused or Neglected" });
            modelBuilder.Entity<ConditionItem>().HasData(new ConditionItem() { Tuid = 3, Acronym = "CI", Description = "Child of Incarcerated Parent" });
            modelBuilder.Entity<ConditionItem>().HasData(new ConditionItem() { Tuid = 4, Acronym = "CH", Description = "Homeless or Recently Displaced" });
            modelBuilder.Entity<ConditionItem>().HasData(new ConditionItem() { Tuid = 5, Acronym = "DD", Description = "Developmental Disibilities" });
            modelBuilder.Entity<ConditionItem>().HasData(new ConditionItem() { Tuid = 6, Acronym = "ES", Description = "Emotional / Social Difficulties" });
            modelBuilder.Entity<ConditionItem>().HasData(new ConditionItem() { Tuid = 7, Acronym = "HS", Description = "Behavior / Social Difficulties" });
            modelBuilder.Entity<ConditionItem>().HasData(new ConditionItem() { Tuid = 8, Acronym = "LB", Description = "Language / Literacy Barriers" });
            modelBuilder.Entity<ConditionItem>().HasData(new ConditionItem() { Tuid = 9, Acronym = "LD", Description = "Learning Disabilities" });
            modelBuilder.Entity<ConditionItem>().HasData(new ConditionItem() { Tuid = 10, Acronym = "PD", Description = "Physical Disabilities" });
            modelBuilder.Entity<ConditionItem>().HasData(new ConditionItem() { Tuid = 11, Acronym = "SP", Description = "Speech Impaired" });
        }
    }
}
