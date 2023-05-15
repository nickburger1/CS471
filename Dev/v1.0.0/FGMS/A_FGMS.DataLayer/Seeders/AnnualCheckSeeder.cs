using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for AnnualChecks
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the AnnualChecks table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class AnnualCheckSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AnnualCheck>().HasData(new AnnualCheck() { 
				Tuid = 1, 
				VolunteerTuid = 1,
				PhotoReleaseDate = DateTime.Parse("9/1/2021"), 
				CarInsuranceDate = DateTime.Parse("8/1/2021"), 
				CovidSouDate = DateTime.Parse("8/1/2021"), 
				EmergencyBeneficiaryDate = DateTime.Parse("8/1/2021"), 
				HippaReleaseDate = DateTime.Parse("8/1/2021"),
				PhysicalDate = DateTime.Parse("8/1/2021"), 
				Year = 2021
			});
			modelBuilder.Entity<AnnualCheck>().HasData(new AnnualCheck()
			{
				Tuid = 2,
				VolunteerTuid = 2,
				PhotoReleaseDate = DateTime.Parse("9/1/2021"),
				CarInsuranceDate = DateTime.Parse("8/1/2021"),
				CovidSouDate = DateTime.Parse("8/1/2021"),
				EmergencyBeneficiaryDate = DateTime.Parse("8/1/2021"),
				HippaReleaseDate = DateTime.Parse("8/1/2021"),
				PhysicalDate = DateTime.Parse("8/1/2021"),
				Year = 2021
			});
			modelBuilder.Entity<AnnualCheck>().HasData(new AnnualCheck()
			{
				Tuid = 3,
				VolunteerTuid = 3,
				PhotoReleaseDate = DateTime.Parse("9/1/2021"),
				CarInsuranceDate = DateTime.Parse("8/1/2021"),
				CovidSouDate = DateTime.Parse("8/1/2021"),
				EmergencyBeneficiaryDate = DateTime.Parse("8/1/2021"),
				HippaReleaseDate = DateTime.Parse("8/1/2021"),
				PhysicalDate = DateTime.Parse("8/1/2021"),
				Year = 2021
			});
			modelBuilder.Entity<AnnualCheck>().HasData(new AnnualCheck()
			{
				Tuid = 4,
				VolunteerTuid = 4,
				PhotoReleaseDate = DateTime.Parse("8/1/2021"),
				CarInsuranceDate = DateTime.Parse("8/1/2021"),
				CovidSouDate = DateTime.Parse("8/1/2021"),
				EmergencyBeneficiaryDate = DateTime.Parse("8/1/2021"),
				HippaReleaseDate = DateTime.Parse("8/1/2021"),
				PhysicalDate = DateTime.Parse("8/1/2021"),
				Year = 2021
			});
			modelBuilder.Entity<AnnualCheck>().HasData(new AnnualCheck()
			{
				Tuid = 5,
				VolunteerTuid = 5,
				PhotoReleaseDate = DateTime.Parse("9/1/2021"),
				CarInsuranceDate = DateTime.Parse("8/1/2021"),
				CovidSouDate = DateTime.Parse("8/1/2021"),
				EmergencyBeneficiaryDate = DateTime.Parse("8/1/2021"),
				HippaReleaseDate = DateTime.Parse("8/1/2021"),
				PhysicalDate = DateTime.Parse("8/1/2021"),
				Year = 2021
			});
			modelBuilder.Entity<AnnualCheck>().HasData(new AnnualCheck()
			{
				Tuid = 6,
				VolunteerTuid = 6,
				PhotoReleaseDate = DateTime.Parse("9/1/2021"),
				CarInsuranceDate = DateTime.Parse("8/1/2021"),
				CovidSouDate = DateTime.Parse("8/1/2021"),
				EmergencyBeneficiaryDate = DateTime.Parse("8/1/2021"),
				HippaReleaseDate = DateTime.Parse("8/1/2021"),
				PhysicalDate = DateTime.Parse("8/1/2021"),
				Year = 2021
			});
			modelBuilder.Entity<AnnualCheck>().HasData(new AnnualCheck()
			{
				Tuid = 7,
				VolunteerTuid = 7,
				PhotoReleaseDate = DateTime.Parse("9/1/2021"),
				CarInsuranceDate = DateTime.Parse("8/1/2021"),
				CovidSouDate = DateTime.Parse("8/1/2021"),
				EmergencyBeneficiaryDate = DateTime.Parse("8/1/2021"),
				HippaReleaseDate = DateTime.Parse("8/1/2021"),
				PhysicalDate = DateTime.Parse("8/1/2021"),
				Year = 2021
			});
			modelBuilder.Entity<AnnualCheck>().HasData(new AnnualCheck()
			{
				Tuid = 8,
				VolunteerTuid = 8,
				PhotoReleaseDate = DateTime.Parse("9/1/2021"),
				CarInsuranceDate = DateTime.Parse("8/1/2021"),
				CovidSouDate = DateTime.Parse("8/1/2021"),
				EmergencyBeneficiaryDate = DateTime.Parse("8/1/2021"),
				HippaReleaseDate = DateTime.Parse("8/1/2021"),
				PhysicalDate = DateTime.Parse("8/1/2021"),
				Year = 2021
			});
			modelBuilder.Entity<AnnualCheck>().HasData(new AnnualCheck()
			{
				Tuid = 9,
				VolunteerTuid = 9,
				PhotoReleaseDate = DateTime.Parse("9/1/2021"),
				CarInsuranceDate = DateTime.Parse("8/1/2021"),
				CovidSouDate = DateTime.Parse("8/1/2021"),
				EmergencyBeneficiaryDate = DateTime.Parse("8/1/2021"),
				HippaReleaseDate = DateTime.Parse("8/1/2021"),
				PhysicalDate = DateTime.Parse("8/1/2021"),
				Year = 2021
			});
			modelBuilder.Entity<AnnualCheck>().HasData(new AnnualCheck()
			{
				Tuid = 10,
				VolunteerTuid = 10,
				PhotoReleaseDate = DateTime.Parse("9/1/2021"),
				CarInsuranceDate = DateTime.Parse("8/1/2021"),
				CovidSouDate = DateTime.Parse("8/1/2021"),
				EmergencyBeneficiaryDate = DateTime.Parse("8/1/2021"),
				HippaReleaseDate = DateTime.Parse("8/1/2021"),
				PhysicalDate = DateTime.Parse("8/1/2021"),
				Year = 2021
			}); modelBuilder.Entity<AnnualCheck>().HasData(new AnnualCheck()
            {
                Tuid = 11,
                VolunteerTuid = 1,
                PhotoReleaseDate = DateTime.Parse("9/1/2021"),
                CarInsuranceDate = DateTime.Parse("8/1/2021"),
                CovidSouDate = DateTime.Parse("8/1/2021"),
                EmergencyBeneficiaryDate = DateTime.Parse("8/1/2021"),
                HippaReleaseDate = DateTime.Parse("8/1/2021"),
                PhysicalDate = DateTime.Parse("8/1/2021"),
                Year = 2021
            });
            modelBuilder.Entity<AnnualCheck>().HasData(new AnnualCheck()
            {
                Tuid = 12,
                VolunteerTuid = 2,
                PhotoReleaseDate = DateTime.Parse("9/1/2021"),
                CarInsuranceDate = DateTime.Parse("8/1/2021"),
                CovidSouDate = DateTime.Parse("8/1/2021"),
                EmergencyBeneficiaryDate = DateTime.Parse("8/1/2021"),
                HippaReleaseDate = DateTime.Parse("8/1/2021"),
                PhysicalDate = DateTime.Parse("8/1/2021"),
                Year = 2021
            });
            modelBuilder.Entity<AnnualCheck>().HasData(new AnnualCheck()
            {
                Tuid = 13,
                VolunteerTuid = 3,
                PhotoReleaseDate = DateTime.Parse("9/1/2021"),
                CarInsuranceDate = DateTime.Parse("8/1/2021"),
                CovidSouDate = DateTime.Parse("8/1/2021"),
                EmergencyBeneficiaryDate = DateTime.Parse("8/1/2021"),
                HippaReleaseDate = DateTime.Parse("8/1/2021"),
                PhysicalDate = DateTime.Parse("8/1/2021"),
                Year = 2021
            });
            modelBuilder.Entity<AnnualCheck>().HasData(new AnnualCheck()
            {
                Tuid = 14,
                VolunteerTuid = 4,
                PhotoReleaseDate = DateTime.Parse("8/1/2021"),
                CarInsuranceDate = DateTime.Parse("8/1/2021"),
                CovidSouDate = DateTime.Parse("8/1/2021"),
                EmergencyBeneficiaryDate = DateTime.Parse("8/1/2021"),
                HippaReleaseDate = DateTime.Parse("8/1/2021"),
                PhysicalDate = DateTime.Parse("8/1/2021"),
                Year = 2021
            });
            modelBuilder.Entity<AnnualCheck>().HasData(new AnnualCheck()
            {
                Tuid = 15,
                VolunteerTuid = 5,
                PhotoReleaseDate = DateTime.Parse("9/1/2021"),
                CarInsuranceDate = DateTime.Parse("8/1/2021"),
                CovidSouDate = DateTime.Parse("8/1/2021"),
                EmergencyBeneficiaryDate = DateTime.Parse("8/1/2021"),
                HippaReleaseDate = DateTime.Parse("8/1/2021"),
                PhysicalDate = DateTime.Parse("8/1/2021"),
                Year = 2021
            });
            modelBuilder.Entity<AnnualCheck>().HasData(new AnnualCheck()
            {
                Tuid = 16,
                VolunteerTuid = 6,
                PhotoReleaseDate = DateTime.Parse("9/1/2021"),
                CarInsuranceDate = DateTime.Parse("8/1/2021"),
                CovidSouDate = DateTime.Parse("8/1/2021"),
                EmergencyBeneficiaryDate = DateTime.Parse("8/1/2021"),
                HippaReleaseDate = DateTime.Parse("8/1/2021"),
                PhysicalDate = DateTime.Parse("8/1/2021"),
                Year = 2021
            });
            modelBuilder.Entity<AnnualCheck>().HasData(new AnnualCheck()
            {
                Tuid = 17,
                VolunteerTuid = 7,
                PhotoReleaseDate = DateTime.Parse("9/1/2021"),
                CarInsuranceDate = DateTime.Parse("8/1/2021"),
                CovidSouDate = DateTime.Parse("8/1/2021"),
                EmergencyBeneficiaryDate = DateTime.Parse("8/1/2021"),
                HippaReleaseDate = DateTime.Parse("8/1/2021"),
                PhysicalDate = DateTime.Parse("8/1/2021"),
                Year = 2021
            });
            modelBuilder.Entity<AnnualCheck>().HasData(new AnnualCheck()
            {
                Tuid = 18,
                VolunteerTuid = 8,
                PhotoReleaseDate = DateTime.Parse("9/1/2021"),
                CarInsuranceDate = DateTime.Parse("8/1/2021"),
                CovidSouDate = DateTime.Parse("8/1/2021"),
                EmergencyBeneficiaryDate = DateTime.Parse("8/1/2021"),
                HippaReleaseDate = DateTime.Parse("8/1/2021"),
                PhysicalDate = DateTime.Parse("8/1/2021"),
                Year = 2021
            });
            modelBuilder.Entity<AnnualCheck>().HasData(new AnnualCheck()
            {
                Tuid = 19,
                VolunteerTuid = 9,
                PhotoReleaseDate = DateTime.Parse("9/1/2021"),
                CarInsuranceDate = DateTime.Parse("8/1/2021"),
                CovidSouDate = DateTime.Parse("8/1/2021"),
                EmergencyBeneficiaryDate = DateTime.Parse("8/1/2021"),
                HippaReleaseDate = DateTime.Parse("8/1/2021"),
                PhysicalDate = DateTime.Parse("8/1/2021"),
                Year = 2021
            });
            modelBuilder.Entity<AnnualCheck>().HasData(new AnnualCheck()
            {
                Tuid = 20,
                VolunteerTuid = 10,
                PhotoReleaseDate = DateTime.Parse("9/1/2021"),
                CarInsuranceDate = DateTime.Parse("8/1/2021"),
                CovidSouDate = DateTime.Parse("8/1/2021"),
                EmergencyBeneficiaryDate = DateTime.Parse("8/1/2021"),
                HippaReleaseDate = DateTime.Parse("8/1/2021"),
                PhysicalDate = DateTime.Parse("8/1/2021"),
                Year = 2021
            });
        }
	}
}
