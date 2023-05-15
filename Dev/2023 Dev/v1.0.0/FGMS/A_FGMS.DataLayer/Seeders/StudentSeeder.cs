using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Seeder for Student
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>2/23/2023</created>
namespace A_FGMS.DataLayer.Seeders
{
    /// <summary>
    /// Seeds data to the Student table
    /// </summary>
    /// <author>Nathan VanSnepson</author>
    /// <created>2/23/2023</created>
    public class StudentSeeder : ISeeder
	{
		public void SeedData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 1, Identifier = "997", IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 2, Identifier = "4087", IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 3, Identifier = "2317", IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 4, Identifier = "192", IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 5, Identifier = "5702", IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 6, Identifier = "7822", IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 7, Identifier = "3626", IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 8, Identifier = "938", IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 9, Identifier = "2617", IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 10, Identifier = "9870", IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 11, Identifier = "1374", IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 12, Identifier = "553" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 13, Identifier = "7078" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 14, Identifier = "8844" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 15, Identifier = "8240" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 16, Identifier = "5337" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 17, Identifier = "8869" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 18, Identifier = "6233" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 19, Identifier = "6969" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 20, Identifier = "9890" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 21, Identifier = "5804" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 22, Identifier = "3310" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 23, Identifier = "1945" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 24, Identifier = "2916" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 25, Identifier = "9492" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 26, Identifier = "2472" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 27, Identifier = "2762" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 28, Identifier = "7881" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 29, Identifier = "469" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 30, Identifier = "4953" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 31, Identifier = "3176" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 32, Identifier = "300" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 33, Identifier = "4340" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 34, Identifier = "7528" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 35, Identifier = "9311" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 36, Identifier = "776" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 37, Identifier = "5289" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 38, Identifier = "7" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 39, Identifier = "2410" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 40, Identifier = "3519" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 41, Identifier = "7697" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 42, Identifier = "305" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 43, Identifier = "2920" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 44, Identifier = "7816" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 45, Identifier = "685" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 46, Identifier = "4872" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 47, Identifier = "7250" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 48, Identifier = "2111" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 49, Identifier = "6056" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 50, Identifier = "1609" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 51, Identifier = "8251" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 52, Identifier = "6453" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 53, Identifier = "3533" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 54, Identifier = "5666" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 55, Identifier = "1371" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 56, Identifier = "2593" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 57, Identifier = "9693" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 58, Identifier = "7240" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 59, Identifier = "4679" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 60, Identifier = "5643" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 61, Identifier = "3301" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 62, Identifier = "1527" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 63, Identifier = "4676" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 64, Identifier = "1036" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 65, Identifier = "5405" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 66, Identifier = "2492" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 67, Identifier = "9514" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 68, Identifier = "7601" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 69, Identifier = "8731" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 70, Identifier = "745" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 71, Identifier = "2362" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 72, Identifier = "9062" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 73, Identifier = "2117" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 74, Identifier = "1844" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 75, Identifier = "7931" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 76, Identifier = "3821" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 77, Identifier = "6635" , IsAge5To12 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 78, Identifier = "5079" , IsAgeBirthTo5 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 79, Identifier = "3174" , IsAgeBirthTo5 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 80, Identifier = "6550" , IsAgeBirthTo5 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 81, Identifier = "4790" , IsAgeBirthTo5 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 82, Identifier = "4492" , IsAgeBirthTo5 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 83, Identifier = "6058", IsAgeBirthTo5 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 84, Identifier = "7091" , IsAgeBirthTo5 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 85, Identifier = "652" , IsAgeBirthTo5 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 86, Identifier = "5321" , IsAgeBirthTo5 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 87, Identifier = "5076" , IsAgeBirthTo5 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 88, Identifier = "482" , IsAgeBirthTo5 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 89, Identifier = "2544" , IsAgeBirthTo5 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 90, Identifier = "7451" , IsAgeBirthTo5 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 91, Identifier = "5338" , IsAgeBirthTo5 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 92, Identifier = "1019" , IsAgeBirthTo5 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 93, Identifier = "1666" , IsAgeBirthTo5 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 94, Identifier = "4323" , IsAgeBirthTo5 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 95, Identifier = "897" , IsAgeBirthTo5 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 96, Identifier = "8780" , IsAgeBirthTo5 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 97, Identifier = "6557" , IsAgeBirthTo5 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 98, Identifier = "3903" , IsAgeBirthTo5 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 99, Identifier = "4091" , IsAgeBirthTo5 = true });
			modelBuilder.Entity<Student>().HasData(new Student() { Tuid = 100, Identifier = "6908" , IsAgeBirthTo5 = true });
		}
	}
}
