using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for Assignment
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the Assignment table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class AssignmentSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 1,
				ClassroomTuid = 1,
				VolunteerTuid = 1,
				Schedule = "[" +
                "{\"day\":\"M\", \"startTime\": \"8:00 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"T\", \"startTime\": \"8:00 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"8:00 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"R\", \"startTime\": \"8:00 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"F\", \"startTime\": \"8:00 am\", \"endtime\": \"3:00 pm\"}" +
                "]"
			}) ;
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 2,
				ClassroomTuid = 2,
				VolunteerTuid = 10,
                Schedule = "[" +
                "{\"day\":\"M\", \"startTime\": \"8:30 am\", \"endtime\": \"3:15 pm\"}," +
                "{\"day\":\"T\", \"startTime\": \"8:30 am\", \"endtime\": \"3:15 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"8:30 am\", \"endtime\": \"3:15 pm\"}," +
                "{\"day\":\"R\", \"startTime\": \"8:30 am\", \"endtime\": \"3:15 pm\"}," +
                "{\"day\":\"F\", \"startTime\": \"8:30 am\", \"endtime\": \"3:15 pm\"}" +
                "]"
			});
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 3,
				VolunteerTuid = 3,
				ClassroomTuid = 3,
				Schedule = "[" +
                "{\"day\":\"M\", \"startTime\": \"8:30 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"T\", \"startTime\": \"8:30 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"8:30 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"R\", \"startTime\": \"8:30 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"F\", \"startTime\": \"8:30 am\", \"endtime\": \"3:00 pm\"}" +
                "]"
			});
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 4,
				ClassroomTuid = 4,
				VolunteerTuid = 5,
				Schedule = "[" +
                "{\"day\":\"M\", \"startTime\": \"8:30 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"T\", \"startTime\": \"8:30 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"8:30 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"R\", \"startTime\": \"8:30 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"F\", \"startTime\": \"8:30 am\", \"endtime\": \"3:00 pm\"}" +
                "]"
			});
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 5,
				ClassroomTuid= 5,
				VolunteerTuid = 3,
				Schedule= "[" +
                "{\"day\":\"M\", \"startTime\": \"8:30 am\", \"endtime\": \"3:20 pm\"}," +
                "{\"day\":\"T\", \"startTime\": \"8:30 am\", \"endtime\": \"3:20 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"8:30 am\", \"endtime\": \"3:20 pm\"}," +
                "{\"day\":\"R\", \"startTime\": \"8:30 am\", \"endtime\": \"3:20 pm\"}," +
                "{\"day\":\"F\", \"startTime\": \"8:30 am\", \"endtime\": \"3:20 pm\"}" +
                "]"
			});
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 6,
				ClassroomTuid = 6,
				VolunteerTuid = 1,
				Schedule= "[" +
                "{\"day\":\"M\", \"startTime\": \"8:30 am\", \"endtime\": \"3:30 pm\"}," +
                "{\"day\":\"T\", \"startTime\": \"8:30 am\", \"endtime\": \"3:30 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"8:30 am\", \"endtime\": \"3:30 pm\"}," +
                "{\"day\":\"R\", \"startTime\": \"8:30 am\", \"endtime\": \"3:30 pm\"}," +
                "{\"day\":\"F\", \"startTime\": \"8:30 am\", \"endtime\": \"3:30 pm\"}" +
                "]"
			});
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 7,
				ClassroomTuid = 7,
				VolunteerTuid = 1,
				Schedule = "[" +
                "{\"day\":\"M\", \"startTime\": \"8:30 am\", \"endtime\": \"3:30 pm\"}," +
                "{\"day\":\"T\", \"startTime\": \"8:30 am\", \"endtime\": \"3:30 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"8:30 am\", \"endtime\": \"3:30 pm\"}," +
                "{\"day\":\"R\", \"startTime\": \"8:30 am\", \"endtime\": \"3:30 pm\"}," +
                "{\"day\":\"F\", \"startTime\": \"8:30 am\", \"endtime\": \"3:30 pm\"}" +
                "]"
			});
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 8,
				ClassroomTuid = 8,
				VolunteerTuid = 1,
				Schedule= "[" +
                "{\"day\":\"M\", \"startTime\": \"8:30 am\", \"endtime\": \"3:30 pm\"}," +
                "{\"day\":\"T\", \"startTime\": \"8:30 am\", \"endtime\": \"3:30 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"8:30 am\", \"endtime\": \"3:30 pm\"}," +
                "{\"day\":\"R\", \"startTime\": \"8:30 am\", \"endtime\": \"3:30 pm\"}," +
                "{\"day\":\"F\", \"startTime\": \"8:30 am\", \"endtime\": \"3:30 pm\"}" +
                "]"
			});
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 9,
				ClassroomTuid = 9,
				VolunteerTuid = 1,
				Schedule = "[" +
                "{\"day\":\"M\", \"startTime\": \"8:30 am\", \"endtime\": \"2:30 pm\"}," +
                "{\"day\":\"T\", \"startTime\": \"8:30 am\", \"endtime\": \"2:30 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"8:30 am\", \"endtime\": \"2:30 pm\"}," +
                "{\"day\":\"R\", \"startTime\": \"8:30 am\", \"endtime\": \"2:30 pm\"}," +
                "{\"day\":\"F\", \"startTime\": \"8:30 am\", \"endtime\": \"2:30 pm\"}" +
                "]"
			});
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 10,
				ClassroomTuid = 10,
				VolunteerTuid = 1,
				Schedule = "[" +
                "{\"day\":\"M\", \"startTime\": \"8:30 am\", \"endtime\": \"3:30 pm\"}," +
                "{\"day\":\"T\", \"startTime\": \"8:30 am\", \"endtime\": \"3:30 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"8:30 am\", \"endtime\": \"3:30 pm\"}," +
                "{\"day\":\"R\", \"startTime\": \"8:30 am\", \"endtime\": \"3:30 pm\"}," +
                "{\"day\":\"F\", \"startTime\": \"8:30 am\", \"endtime\": \"3:30 pm\"}" +
                "]"
            });
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 11,
				ClassroomTuid = 11,
				VolunteerTuid = 9,
				Schedule = "[" +
                "{\"day\":\"M\", \"startTime\": \"8:45 am\", \"endtime\": \"3:40 pm\"}," +
                "{\"day\":\"T\", \"startTime\": \"8:45 am\", \"endtime\": \"3:40 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"8:45 am\", \"endtime\": \"3:40 pm\"}," +
                "{\"day\":\"R\", \"startTime\": \"8:45 am\", \"endtime\": \"3:40 pm\"}," +
                "{\"day\":\"F\", \"startTime\": \"8:45 am\", \"endtime\": \"3:40 pm\"}" +
                "]"
			});
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 12,
				ClassroomTuid = 12,
				VolunteerTuid = 9,
				Schedule = "[" +
                "{\"day\":\"M\", \"startTime\": \"8:45 am\", \"endtime\": \"1:00 pm\"}," +
                "{\"day\":\"T\", \"startTime\": \"8:45 am\", \"endtime\": \"1:00 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"8:45 am\", \"endtime\": \"1:00 pm\"}," +
                "{\"day\":\"R\", \"startTime\": \"8:45 am\", \"endtime\": \"1:00 pm\"}" +
                "]"
			});
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 13,
				ClassroomTuid = 13,
				VolunteerTuid = 6,
				Schedule = "[" +
                "{\"day\":\"M\", \"startTime\": \"8:00 am\", \"endtime\": \"2:00 pm\"}," +
                "{\"day\":\"T\", \"startTime\": \"8:00 am\", \"endtime\": \"2:00 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"8:00 am\", \"endtime\": \"2:00 pm\"}," +
                "{\"day\":\"R\", \"startTime\": \"8:00 am\", \"endtime\": \"2:00 pm\"}" +
                "]"
			});
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 14,
				ClassroomTuid = 14,
				VolunteerTuid = 2,
				Schedule = "[" +
                "{\"day\":\"M\", \"startTime\": \"8:00 am\", \"endtime\": \"2:00 pm\"}," +
                "{\"day\":\"T\", \"startTime\": \"8:00 am\", \"endtime\": \"2:00 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"8:00 am\", \"endtime\": \"2:00 pm\"}," +
                "{\"day\":\"R\", \"startTime\": \"8:00 am\", \"endtime\": \"2:00 pm\"}," +
                "{\"day\":\"F\", \"startTime\": \"8:00 am\", \"endtime\": \"2:00 pm\"}" +
                "]"
			});
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 15,
				ClassroomTuid = 15,
				VolunteerTuid = 2,
				Schedule = "[" +
                "{\"day\":\"M\", \"startTime\": \"7:15 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"T\", \"startTime\": \"7:15 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"7:15 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"R\", \"startTime\": \"7:15 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"F\", \"startTime\": \"7:15 am\", \"endtime\": \"3:00 pm\"}" +
                "]"
			});
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 16,
				ClassroomTuid = 16,
				VolunteerTuid = 6,
				Schedule = "[" +
                "{\"day\":\"M\", \"startTime\": \"7:00 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"7:00 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"R\", \"startTime\": \"7:00 am\", \"endtime\": \"3:00 pm\"}" +
                "]"
			});
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 17,
				ClassroomTuid = 17,
				VolunteerTuid = 4,
				Schedule = "[" +
                "{\"day\":\"M\", \"startTime\": \"8:00 am\", \"endtime\": \"3:15 pm\"}," +
                "{\"day\":\"T\", \"startTime\": \"8:00 am\", \"endtime\": \"3:15 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"8:00 am\", \"endtime\": \"3:15 pm\"}," +
                "{\"day\":\"R\", \"startTime\": \"8:00 am\", \"endtime\": \"3:15 pm\"}," +
                "{\"day\":\"F\", \"startTime\": \"8:00 am\", \"endtime\": \"3:15 pm\"}" +
                "]"
			});
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 18,
				ClassroomTuid = 18,
				VolunteerTuid = 2,
				Schedule = "[" +
                "{\"day\":\"M\", \"startTime\": \"8:00 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"T\", \"startTime\": \"8:00 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"8:00 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"R\", \"startTime\": \"8:00 am\", \"endtime\": \"3:00 pm\"}" +
                "]"
			});
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 19,
				ClassroomTuid = 19,
				VolunteerTuid = 7,
				Schedule = "[" +
                "{\"day\":\"M\", \"startTime\": \"8:00 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"T\", \"startTime\": \"8:00 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"8:00 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"R\", \"startTime\": \"8:00 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"F\", \"startTime\": \"8:00 am\", \"endtime\": \"3:00 pm\"}" +
                "]"
			});
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 20,
				ClassroomTuid = 20,
				VolunteerTuid = 8,
				Schedule = "[" +
                "{\"day\":\"M\", \"startTime\": \"9:00 am\", \"endtime\": \"2:30 pm\"}," +
                "{\"day\":\"T\", \"startTime\": \"9:00 am\", \"endtime\": \"2:30 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"9:00 am\", \"endtime\": \"2:30 pm\"}," +
                "{\"day\":\"R\", \"startTime\": \"9:00 am\", \"endtime\": \"2:30 pm\"}" +
                "]"
			});
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 21,
				ClassroomTuid = 21,
				VolunteerTuid = 9,
                Schedule = "[" +
                "{\"day\":\"M\", \"startTime\": \"8:30 am\", \"endtime\": \"1:30 pm\"}," +
                "{\"day\":\"T\", \"startTime\": \"8:30 am\", \"endtime\": \"1:30 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"8:30 am\", \"endtime\": \"1:30 pm\"}," +
                "{\"day\":\"R\", \"startTime\": \"8:30 am\", \"endtime\": \"1:30 pm\"}," +
                "{\"day\":\"F\", \"startTime\": \"8:30 am\", \"endtime\": \"1:30 pm\"}" +
                "]"
			});
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 22,
				ClassroomTuid = 22,
				VolunteerTuid = 9,
				Schedule = "[" +
                "{\"day\":\"M\", \"startTime\": \"8:00 am\", \"endtime\": \"1:30 pm\"}," +
                "{\"day\":\"T\", \"startTime\": \"8:00 am\", \"endtime\": \"1:30 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"8:00 am\", \"endtime\": \"1:30 pm\"}," +
                "{\"day\":\"R\", \"startTime\": \"8:00 am\", \"endtime\": \"1:30 pm\"}," +
                "{\"day\":\"F\", \"startTime\": \"8:00 am\", \"endtime\": \"1:30 pm\"}" +
                "]"
			});
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 23,
				ClassroomTuid = 23,
				VolunteerTuid = 4,
                Schedule = "[" +
                "{\"day\":\"M\", \"startTime\": \"9:30 am\", \"endtime\": \"2:30 pm\"}," +
                "{\"day\":\"T\", \"startTime\": \"10:30 am\", \"endtime\": \"3:30 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"9:30 am\", \"endtime\": \"2:30 pm\"}," +
                "{\"day\":\"R\", \"startTime\": \"10:30 am\", \"endtime\": \"3:30 pm\"}" +
                "]"
			});
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 24,
				ClassroomTuid = 24,
				VolunteerTuid = 4,
				Schedule= "[" +
                "{\"day\":\"T\", \"startTime\": \"8:30 am\", \"endtime\": \"1:30 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"8:30 am\", \"endtime\": \"1:30 pm\"}," +
                "{\"day\":\"R\", \"startTime\": \"8:30 am\", \"endtime\": \"1:30 pm\"}" +
                "]"
			});
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 25,
				ClassroomTuid = 25,
				VolunteerTuid = 3,
				Schedule = "[" +
                "{\"day\":\"M\", \"startTime\": \"8:00 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"T\", \"startTime\": \"8:00 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"8:00 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"R\", \"startTime\": \"8:00 am\", \"endtime\": \"3:00 pm\"}" +
                "]"
			});
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 26,
				ClassroomTuid = 26,
				VolunteerTuid = 6,
				Schedule = "[" +
                "{\"day\":\"M\", \"startTime\": \"9:00 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"T\", \"startTime\": \"9:00 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"9:00 am\", \"endtime\": \"3:00 pm\"}" +
                "]"
			});
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 27,
				ClassroomTuid = 27,
				VolunteerTuid = 7,
				Schedule = "[" +
                "{\"day\":\"T\", \"startTime\": \"10:00 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"10:00 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"R\", \"startTime\": \"10:00 am\", \"endtime\": \"3:00 pm\"}" +
                "]"
			});
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 28,
				ClassroomTuid = 28,
				VolunteerTuid = 10,
				Schedule = "[" +
                "{\"day\":\"M\", \"startTime\": \"8:00 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"T\", \"startTime\": \"8:00 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"8:00 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"R\", \"startTime\": \"8:00 am\", \"endtime\": \"3:00 pm\"}," +
                "{\"day\":\"F\", \"startTime\": \"8:00 am\", \"endtime\": \"3:00 pm\"}" +
                "]"
			});
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 29,
				ClassroomTuid = 29,
				VolunteerTuid = 10,
				Schedule = "[" +
                "{\"day\":\"M\", \"startTime\": \"9:30 am\", \"endtime\": \"2:30 pm\"}," +
                "{\"day\":\"T\", \"startTime\": \"9:30 am\", \"endtime\": \"2:30 pm\"}," +
                "{\"day\":\"W\", \"startTime\": \"9:30 am\", \"endtime\": \"2:30 pm\"}," +
                "{\"day\":\"R\", \"startTime\": \"9:30 am\", \"endtime\": \"2:30 pm\"}" +
                "]"
			});
			modelBuilder.Entity<Assignment>().HasData(new Assignment()
			{
				Tuid = 30,
				ClassroomTuid = 30,
				VolunteerTuid = 2,
				Schedule = "[]"
			});

		}
	}
}
