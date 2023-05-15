using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for OneTimeCheck
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the OneTimeCheck table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class OneTimeCheckSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<OneTimeCheck>().HasData(new OneTimeCheck()
			{
				Tuid = 1,
				VolunteerTuid = 1,
				AliasFingerprintDate = DateTime.Parse("8/1/2021"),
				ConfidenceSouDate = DateTime.Parse("8/1/2021"),
				DhsDate = DateTime.Parse("8/1/2021"),
				FieldPrintDate = DateTime.Parse("8/1/2021"),
				HasBackgroundCheck = true,
				HasFilePhoto = true,
				HasIdCopy = true,
				HasNschc = true,
				HasServiceDescription = true,
				HasTrainingSheet = true,
				IChatDate = DateTime.Parse("8/1/2021"),
				NsopwDate = DateTime.Parse("8/1/2021"),
				ServiceStartDate = DateTime.Parse("8/1/2021"),
				TbShotDate = DateTime.Parse("8/1/2021"),
				TrueScreenDate = DateTime.Parse("8/1/2021")
			});
			modelBuilder.Entity<OneTimeCheck>().HasData(new OneTimeCheck()
			{
				Tuid = 2,
				VolunteerTuid = 2,
				AliasFingerprintDate = DateTime.Parse("8/1/2021"),
				ConfidenceSouDate = DateTime.Parse("8/1/2021"),
				DhsDate = DateTime.Parse("8/1/2021"),
				FieldPrintDate = DateTime.Parse("8/1/2021"),
				HasBackgroundCheck = true,
				HasFilePhoto = true,
				HasIdCopy = true,
				HasNschc = true,
				HasServiceDescription = true,
				HasTrainingSheet = true,
				IChatDate = DateTime.Parse("8/1/2021"),
				NsopwDate = DateTime.Parse("8/1/2021"),
				ServiceStartDate = DateTime.Parse("8/1/2021"),
				TbShotDate = DateTime.Parse("8/1/2021"),
				TrueScreenDate = DateTime.Parse("8/1/2021")
			});
			modelBuilder.Entity<OneTimeCheck>().HasData(new OneTimeCheck()
			{
				Tuid = 3,
				VolunteerTuid = 3,
				AliasFingerprintDate = DateTime.Parse("8/1/2021"),
				ConfidenceSouDate = DateTime.Parse("8/1/2021"),
				DhsDate = DateTime.Parse("8/1/2021"),
				FieldPrintDate = DateTime.Parse("8/1/2021"),
				HasBackgroundCheck = true,
				HasFilePhoto = true,
				HasIdCopy = true,
				HasNschc = true,
				HasServiceDescription = true,
				HasTrainingSheet = true,
				IChatDate = DateTime.Parse("8/1/2021"),
				NsopwDate = DateTime.Parse("8/1/2021"),
				ServiceStartDate = DateTime.Parse("8/1/2021"),
				TbShotDate = DateTime.Parse("8/1/2021"),
				TrueScreenDate = DateTime.Parse("8/1/2021")
			});
			modelBuilder.Entity<OneTimeCheck>().HasData(new OneTimeCheck()
			{
				Tuid = 4,
				VolunteerTuid = 4,
				AliasFingerprintDate = DateTime.Parse("8/1/2021"),
				ConfidenceSouDate = DateTime.Parse("8/1/2021"),
				DhsDate = DateTime.Parse("8/1/2021"),
				FieldPrintDate = DateTime.Parse("8/1/2021"),
				HasBackgroundCheck = true,
				HasFilePhoto = true,
				HasIdCopy = true,
				HasNschc = true,
				HasServiceDescription = true,
				HasTrainingSheet = true,
				IChatDate = DateTime.Parse("8/1/2021"),
				NsopwDate = DateTime.Parse("8/1/2021"),
				ServiceStartDate = DateTime.Parse("8/1/2021"),
				TbShotDate = DateTime.Parse("8/1/2021"),
				TrueScreenDate = DateTime.Parse("8/1/2021")
			});
			modelBuilder.Entity<OneTimeCheck>().HasData(new OneTimeCheck()
			{
				Tuid = 5,
				VolunteerTuid = 5,
				AliasFingerprintDate = DateTime.Parse("8/1/2021"),
				ConfidenceSouDate = DateTime.Parse("8/1/2021"),
				DhsDate = DateTime.Parse("8/1/2021"),
				FieldPrintDate = DateTime.Parse("8/1/2021"),
				HasBackgroundCheck = true,
				HasFilePhoto = true,
				HasIdCopy = true,
				HasNschc = true,
				HasServiceDescription = true,
				HasTrainingSheet = true,
				IChatDate = DateTime.Parse("8/1/2021"),
				NsopwDate = DateTime.Parse("8/1/2021"),
				ServiceStartDate = DateTime.Parse("8/1/2021"),
				TbShotDate = DateTime.Parse("8/1/2021"),
				TrueScreenDate = DateTime.Parse("8/1/2021")
			});
			modelBuilder.Entity<OneTimeCheck>().HasData(new OneTimeCheck()
			{
				Tuid = 6,
				VolunteerTuid = 6,
				AliasFingerprintDate = DateTime.Parse("8/1/2021"),
				ConfidenceSouDate = DateTime.Parse("8/1/2021"),
				DhsDate = DateTime.Parse("8/1/2021"),
				FieldPrintDate = DateTime.Parse("8/1/2021"),
				HasBackgroundCheck = true,
				HasFilePhoto = true,
				HasIdCopy = true,
				HasNschc = true,
				HasServiceDescription = true,
				HasTrainingSheet = true,
				IChatDate = DateTime.Parse("8/1/2021"),
				NsopwDate = DateTime.Parse("8/1/2021"),
				ServiceStartDate = DateTime.Parse("8/1/2021"),
				TbShotDate = DateTime.Parse("8/1/2021"),
				TrueScreenDate = DateTime.Parse("8/1/2021")
			});
			modelBuilder.Entity<OneTimeCheck>().HasData(new OneTimeCheck()
			{
				Tuid = 7,
				VolunteerTuid = 7,
				AliasFingerprintDate = DateTime.Parse("8/1/2021"),
				ConfidenceSouDate = DateTime.Parse("8/1/2021"),
				DhsDate = DateTime.Parse("8/1/2021"),
				FieldPrintDate = DateTime.Parse("8/1/2021"),
				HasBackgroundCheck = true,
				HasFilePhoto = true,
				HasIdCopy = true,
				HasNschc = true,
				HasServiceDescription = true,
				HasTrainingSheet = true,
				IChatDate = DateTime.Parse("8/1/2021"),
				NsopwDate = DateTime.Parse("8/1/2021"),
				ServiceStartDate = DateTime.Parse("8/1/2021"),
				TbShotDate = DateTime.Parse("8/1/2021"),
				TrueScreenDate = DateTime.Parse("8/1/2021")
			});
			modelBuilder.Entity<OneTimeCheck>().HasData(new OneTimeCheck()
			{
				Tuid = 8,
				VolunteerTuid = 8,
				AliasFingerprintDate = DateTime.Parse("8/1/2021"),
				ConfidenceSouDate = DateTime.Parse("8/1/2021"),
				DhsDate = DateTime.Parse("8/1/2021"),
				FieldPrintDate = DateTime.Parse("8/1/2021"),
				HasBackgroundCheck = true,
				HasFilePhoto = true,
				HasIdCopy = true,
				HasNschc = true,
				HasServiceDescription = true,
				HasTrainingSheet = true,
				IChatDate = DateTime.Parse("8/1/2021"),
				NsopwDate = DateTime.Parse("8/1/2021"),
				ServiceStartDate = DateTime.Parse("8/1/2021"),
				TbShotDate = DateTime.Parse("8/1/2021"),
				TrueScreenDate = DateTime.Parse("8/1/2021")
			});
			modelBuilder.Entity<OneTimeCheck>().HasData(new OneTimeCheck()
			{
				Tuid = 9,
				VolunteerTuid = 9,
				AliasFingerprintDate = DateTime.Parse("8/1/2021"),
				ConfidenceSouDate = DateTime.Parse("8/1/2021"),
				DhsDate = DateTime.Parse("8/1/2021"),
				FieldPrintDate = DateTime.Parse("8/1/2021"),
				HasBackgroundCheck = true,
				HasFilePhoto = true,
				HasIdCopy = true,
				HasNschc = true,
				HasServiceDescription = true,
				HasTrainingSheet = true,
				IChatDate = DateTime.Parse("8/1/2021"),
				NsopwDate = DateTime.Parse("8/1/2021"),
				ServiceStartDate = DateTime.Parse("8/1/2021"),
				TbShotDate = DateTime.Parse("8/1/2021"),
				TrueScreenDate = DateTime.Parse("8/1/2021")
			});
			modelBuilder.Entity<OneTimeCheck>().HasData(new OneTimeCheck()
			{
				Tuid = 10,
				VolunteerTuid = 10,
				AliasFingerprintDate = DateTime.Parse("8/1/2021"),
				ConfidenceSouDate = DateTime.Parse("8/1/2021"),
				DhsDate = DateTime.Parse("8/1/2021"),
				FieldPrintDate = DateTime.Parse("8/1/2021"),
				HasBackgroundCheck = true,
				HasFilePhoto = true,
				HasIdCopy = true,
				HasNschc = true,
				HasServiceDescription = true,
				HasTrainingSheet = true,
				IChatDate = DateTime.Parse("8/1/2021"),
				NsopwDate = DateTime.Parse("8/1/2021"),
				ServiceStartDate = DateTime.Parse("8/1/2021"),
				TbShotDate = DateTime.Parse("8/1/2021"),
				TrueScreenDate = DateTime.Parse("8/1/2021")
			});
		}
	}
}
