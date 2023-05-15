using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for ActivityLog
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the ActivityLogs table
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class ActivityLogSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog() { 
				Tuid = 1, 
				VolunteerTuid = 1, 
				Date = DateTime.Parse("1/20/2022"), 
				Incident = "Left on emergency", 
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 2,
				VolunteerTuid = 2,
				Date = DateTime.Parse("8/15/2019"),
				Incident = "Leaving 2 hrs early for appt.",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 3,
				VolunteerTuid = 2,
				Date = DateTime.Parse("9/25/2019"),
				Incident = "Away to attend daughter's wedding",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 4,
				VolunteerTuid = 2,
				Date = DateTime.Parse("9/26/2019"),
				Incident = "Away to attend daughter's wedding",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 5,
				VolunteerTuid = 2,
				Date = DateTime.Parse("10/8/2019"),
				Incident = "car repair",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 6,
				VolunteerTuid = 2,
				Date = DateTime.Parse("3/10/2020"),
				Incident = "Working at polls",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 7,
				VolunteerTuid = 2,
				Date = DateTime.Parse("9/15/2021"),
				Incident = "Days needed off",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 8,
				VolunteerTuid = 2,
				Date = DateTime.Parse("9/30/2021"),
				Incident = "Days needed off",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 9,
				VolunteerTuid = 2,
				Date = DateTime.Parse("10/25/2021"),
				Incident = "Ill",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 10,
				VolunteerTuid = 2,
				Date = DateTime.Parse("1/24/2022"),
				Incident = "Off for birthday; use PTO",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 11,
				VolunteerTuid = 2,
				Date = DateTime.Parse("1/20/2022"),
				Incident = "Absent",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 12,
				VolunteerTuid = 3,
				Date = DateTime.Parse("9/26/2018"),
				Incident = "Taking Lunch Today",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 13,
				VolunteerTuid = 3,
				Date = DateTime.Parse("10/24/2018"),
				Incident = "Taking lunch today (early 2 10:30a)",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 14,
				VolunteerTuid = 3,
				Date = DateTime.Parse("11/7/2018"),
				Incident = "Personal Business",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 15,
				VolunteerTuid = 3,
				Date = DateTime.Parse("12/4/2018"),
				Incident = "Appt - will be gone for part of morning",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 16,
				VolunteerTuid = 3,
				Date = DateTime.Parse("1/17/2019"),
				Incident = "gont for 1-2 hours for doctor appt.",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 17,
				VolunteerTuid = 3,
				Date = DateTime.Parse("1/22/2019"),
				Incident = "Waiting for maintenance on her place - will go to school if done by noon",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 18,
				VolunteerTuid = 3,
				Date = DateTime.Parse("3/5/2019"),
				Incident = "With brother who has been hospitalized",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 19,
				VolunteerTuid = 3,
				Date = DateTime.Parse("5/21/2019"),
				Incident = "Ill",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 20,
				VolunteerTuid = 3,
				Date = DateTime.Parse("6/3/2019"),
				Incident = "Out - sinus infection",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 21,
				VolunteerTuid = 3,
				Date = DateTime.Parse("12/13/2019"),
				Incident = "Off - dental work",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 22,
				VolunteerTuid = 3,
				Date = DateTime.Parse("2/10/2020"),
				Incident = "Boiler problems - school is keeping kids, but temp at 51, may or may not leave",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 23,
				VolunteerTuid = 3,
				Date = DateTime.Parse("2/20/2020"),
				Incident = "ill today",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 24,
				VolunteerTuid = 3,
				Date = DateTime.Parse("2/28/2020"),
				Incident = "Ill",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 25,
				VolunteerTuid = 3,
				Date = DateTime.Parse("1/14/2022"),
				Incident = "Out ill",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 26,
				VolunteerTuid = 3,
				Date = DateTime.Parse("2/18/2022"),
				Incident = "Stayed home ill today",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 27,
				VolunteerTuid = 3,
				Date = DateTime.Parse("4/27/2022"),
				Incident = "Fell at school & hurt arm - states that he is ok - we called his daughter & required incident report - keith back at school in the afternoon",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 28,
				VolunteerTuid = 4,
				Date = DateTime.Parse("1/14/2019"),
				Incident = "Funeral today",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 29,
				VolunteerTuid = 4,
				Date = DateTime.Parse("2/3/2020"),
				Incident = "Not in today - car issues",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 30,
				VolunteerTuid = 4,
				Date = DateTime.Parse("2/10/2020"),
				Incident = "Boiler problems - school is keeping kids",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 31,
				VolunteerTuid = 4,
				Date = DateTime.Parse("2/20/2020"),
				Incident = "sick",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 32,
				VolunteerTuid = 4,
				Date = DateTime.Parse("3/2/2020"),
				Incident = "sick",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 33,
				VolunteerTuid = 4,
				Date = DateTime.Parse("3/3/2020"),
				Incident = "sick",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 34,
				VolunteerTuid = 4,
				Date = DateTime.Parse("9/16/2021"),
				Incident = "Absent fr. inservice meeting",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 35,
				VolunteerTuid = 4,
				Date = DateTime.Parse("1/5/2021"),
				Incident = "Fiancee is ill",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 36,
				VolunteerTuid = 5,
				Date = DateTime.Parse("11/14/2019"),
				Incident = "Not at school for at least the A.M.",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 37,
				VolunteerTuid = 5,
				Date = DateTime.Parse("11/18/2019"),
				Incident = "Procedure @ hospital; may work pm",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 38,
				VolunteerTuid = 5,
				Date = DateTime.Parse("11/19/2019"),
				Incident = "absent until next tues",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 39,
				VolunteerTuid = 5,
				Date = DateTime.Parse("1/22/2020"),
				Incident = "Doctor appt",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 40,
				VolunteerTuid = 5,
				Date = DateTime.Parse("1/23/2020"),
				Incident = "Doctor appt",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 41,
				VolunteerTuid = 5,
				Date = DateTime.Parse("2/10/2020"),
				Incident = "Has chemo treatment",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 42,
				VolunteerTuid = 5,
				Date = DateTime.Parse("9/21/2020"),
				Incident = "Appts",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 43,
				VolunteerTuid = 5,
				Date = DateTime.Parse("11/2/2021"),
				Incident = "Out due to quarantine",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 44,
				VolunteerTuid = 6,
				Date = DateTime.Parse("9/16/2021"),
				Incident = "Abset fr. Inservice meeting",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 45,
				VolunteerTuid = 6,
				Date = DateTime.Parse("12/8/2021"),
				Incident = "Out - problems with icy driveway",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 46,
				VolunteerTuid = 6,
				Date = DateTime.Parse("12/24/2022"),
				Incident = "Called in - difficulty getting out due to storm",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 47,
				VolunteerTuid = 7,
				Date = DateTime.Parse("4/26/2022"),
				Incident = "Called in",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 48,
				VolunteerTuid = 7,
				Date = DateTime.Parse("6/1/2022"),
				Incident = "called in",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 49,
				VolunteerTuid = 8,
				Date = DateTime.Parse("11/20/2018"),
				Incident = "at hospital",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 50,
				VolunteerTuid = 8,
				Date = DateTime.Parse("11/30/2018"),
				Incident = "off",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 51,
				VolunteerTuid = 8,
				Date = DateTime.Parse("12/3/2018"),
				Incident = "off",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 52,
				VolunteerTuid = 8,
				Date = DateTime.Parse("12/6/2018"),
				Incident = "off",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 53,
				VolunteerTuid = 8,
				Date = DateTime.Parse("12/4/2018"),
				Incident = "wife ill",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 54,
				VolunteerTuid = 8,
				Date = DateTime.Parse("12/4/2018"),
				Incident = "ill",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 55,
				VolunteerTuid = 8,
				Date = DateTime.Parse("1/22/2019"),
				Incident = "Ill",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 56,
				VolunteerTuid = 8,
				Date = DateTime.Parse("5/10/2019"),
				Incident = "Off",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 57,
				VolunteerTuid = 8,
				Date = DateTime.Parse("8/15/2019"),
				Incident = "Sewer issues",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 58,
				VolunteerTuid = 8,
				Date = DateTime.Parse("9/3/2019"),
				Incident = "Delays school due to roads",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 59,
				VolunteerTuid = 8,
				Date = DateTime.Parse("12/20/2019"),
				Incident = "holiday in-service",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 60,
				VolunteerTuid = 8,
				Date = DateTime.Parse("3/5/2020"),
				Incident = "Ill",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 61,
				VolunteerTuid = 8,
				Date = DateTime.Parse("11/12/2021"),
				Incident = "PTO",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 62,
				VolunteerTuid = 8,
				Date = DateTime.Parse("1/20/2022"),
				Incident = "Moved schools",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 63,
				VolunteerTuid = 9,
				Date = DateTime.Parse("4/27/2017"),
				Incident = "personal",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 64,
				VolunteerTuid = 9,
				Date = DateTime.Parse("5/25/2017"),
				Incident = "personal",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 65,
				VolunteerTuid = 9,
				Date = DateTime.Parse("5/30/2017"),
				Incident = "Ill",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 66,
				VolunteerTuid = 9,
				Date = DateTime.Parse("6/1/2017"),
				Incident = "ill",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 67,
				VolunteerTuid = 9,
				Date = DateTime.Parse("10/4/2014"),
				Incident = "personal",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 68,
				VolunteerTuid = 9,
				Date = DateTime.Parse("11/7/2017"),
				Incident = "ill",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 69,
				VolunteerTuid = 9,
				Date = DateTime.Parse("12/4/2017"),
				Incident = "schools closed",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 70,
				VolunteerTuid = 9,
				Date = DateTime.Parse("1/23/2018"),
				Incident = "ill",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 71,
				VolunteerTuid = 9,
				Date = DateTime.Parse("7/17/2018"),
				Incident = "off",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 72,
				VolunteerTuid = 9,
				Date = DateTime.Parse("10/1/2018"),
				Incident = "personal",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 73,
				VolunteerTuid = 9,
				Date = DateTime.Parse("11/1/2018"),
				Incident = "Off",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 74,
				VolunteerTuid = 9,
				Date = DateTime.Parse("11/6/2018"),
				Incident = "personal",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 75,
				VolunteerTuid = 9,
				Date = DateTime.Parse("12/13/2018"),
				Incident = "off",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 76,
				VolunteerTuid = 9,
				Date = DateTime.Parse("12/17/2018"),
				Incident = "off",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 77,
				VolunteerTuid = 9,
				Date = DateTime.Parse("12/14/2018"),
				Incident = "Off",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 78,
				VolunteerTuid = 9,
				Date = DateTime.Parse("1/9/2019"),
				Incident = "ill",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 79,
				VolunteerTuid = 9,
				Date = DateTime.Parse("1/10/2019"),
				Incident = "ill",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 80,
				VolunteerTuid = 9,
				Date = DateTime.Parse("5/13/2019"),
				Incident = "ill",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 81,
				VolunteerTuid = 9,
				Date = DateTime.Parse("8/13/2019"),
				Incident = "Broke heel",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 82,
				VolunteerTuid = 9,
				Date = DateTime.Parse("9/3/2019"),
				Incident = "Notice of absent",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 83,
				VolunteerTuid = 9,
				Date = DateTime.Parse("9/5/2019"),
				Incident = "New boot, broken heel",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 84,
				VolunteerTuid = 9,
				Date = DateTime.Parse("12/4/2019"),
				Incident = "Doctor appt",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 85,
				VolunteerTuid = 9,
				Date = DateTime.Parse("12/17/2019"),
				Incident = "Kids on field trip",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 86,
				VolunteerTuid = 9,
				Date = DateTime.Parse("2/10/2020"),
				Incident = "Not in today",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 87,
				VolunteerTuid = 9,
				Date = DateTime.Parse("2/11/2020"),
				Incident = "Ill",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 88,
				VolunteerTuid = 9,
				Date = DateTime.Parse("10/4/2021"),
				Incident = "sick",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 89,
				VolunteerTuid = 9,
				Date = DateTime.Parse("11/17/2021"),
				Incident = "not in",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 90,
				VolunteerTuid = 9,
				Date = DateTime.Parse("11/22/2021"),
				Incident = "Classroom closed due to covid",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 91,
				VolunteerTuid = 9,
				Date = DateTime.Parse("12/7/2021"),
				Incident = "Off for funeral",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 92,
				VolunteerTuid = 9,
				Date = DateTime.Parse("12/13/2021"),
				Incident = "Out early",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 93,
				VolunteerTuid = 9,
				Date = DateTime.Parse("1/12/2022"),
				Incident = "Closed due to covid",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 94,
				VolunteerTuid = 9,
				Date = DateTime.Parse("1/14/2022"),
				Incident = "Closed due to covid",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 95,
				VolunteerTuid = 9,
				Date = DateTime.Parse("3/9/2022"),
				Incident = "Not in either day",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 96,
				VolunteerTuid = 9,
				Date = DateTime.Parse("4/18/2022"),
				Incident = "Not feeling well",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 97,
				VolunteerTuid = 10,
				Date = DateTime.Parse("9/16/2021"),
				Incident = "Absent for inservice meeting",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 98,
				VolunteerTuid = 10,
				Date = DateTime.Parse("1/18/2022"),
				Incident = "absent",
				Initial = "TRS"
			});
			modelBuilder.Entity<ActivityLog>().HasData(new ActivityLog()
			{
				Tuid = 99,
				VolunteerTuid = 10,
				Date = DateTime.Parse("2/24/2022"),
				Incident = "back issues",
				Initial = "TRS"
			});
		}
	}
}
