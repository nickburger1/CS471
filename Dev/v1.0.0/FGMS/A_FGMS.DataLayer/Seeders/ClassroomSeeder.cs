using A_FGMS.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


/// <summary>
/// The Purpose of this file is to provide the schema for the Classroom table.
/// </summary>
/// <author>Nathan VanSnepson</author>
/// <created>3/30/23</created>
namespace A_FGMS.DataLayer.Seeders
{
#pragma warning disable CS8618
    public class ClassroomSeeder
    {
        public void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 1,
                SchoolTuid = 1,
                ClassroomNumber = "104",
                ClassroomSize = 25,
                GradeLevel = "1st",
                TeacherName = "Regina Alford",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 2,
                SchoolTuid = 1,
                ClassroomNumber = "103",
                ClassroomSize = 16,
                GradeLevel = "1st",
                TeacherName = "Cheryl Moore",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 3,
                SchoolTuid = 2,
                ClassroomNumber = "114",
                ClassroomSize = 24,
                GradeLevel = "KDG",
                TeacherName = "Amy Rutlege",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 4,
                SchoolTuid = 2,
                ClassroomNumber = "105",
                ClassroomSize = 12,
                GradeLevel = "1st",
                TeacherName = "Shaye Cousineau",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 5,
                SchoolTuid = 2,
                ClassroomNumber = "5",
                ClassroomSize = 28,
                GradeLevel = "1st",
                TeacherName = "Ridenour",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 6,
                SchoolTuid = 3,
                ClassroomNumber = "",
                ClassroomSize = 28,
                GradeLevel = "1st",
                TeacherName = "Terry",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 7,
                SchoolTuid = 3,
                ClassroomNumber = "",
                ClassroomSize = 28,
                GradeLevel = "1st",
                TeacherName = "Meyers",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 8,
                SchoolTuid = 4,
                ClassroomNumber = "31",
                ClassroomSize = 28,
                GradeLevel = "1st",
                TeacherName = "Mrs. Cowan",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 9,
                SchoolTuid = 5,
                ClassroomNumber = "",
                ClassroomSize = 28,
                GradeLevel = "KDG",
                TeacherName = "Warren",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 10,
                SchoolTuid = 5,
                ClassroomNumber = "",
                ClassroomSize = 28,
                GradeLevel = "KDG",
                TeacherName = "Dobrowolsky",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 11,
                SchoolTuid = 6,
                ClassroomNumber = "",
                ClassroomSize = 28,
                GradeLevel = "KDG",
                TeacherName = "S. Baston",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 12,
                SchoolTuid = 6,
                ClassroomNumber = "",
                ClassroomSize = 28,
                GradeLevel = "KDG",
                TeacherName = "C. Hutter",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 13,
                SchoolTuid = 7,
                ClassroomNumber = "",
                ClassroomSize = 28,
                GradeLevel = "KDG",
                TeacherName = "Hauser / Aspin",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 14,
                SchoolTuid = 7,
                ClassroomNumber = "",
                ClassroomSize = 28,
                GradeLevel = "",
                TeacherName = "Fitzgerald",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 15,
                SchoolTuid = 7,
                ClassroomNumber = "",
                ClassroomSize = 28,
                GradeLevel = "KDG",
                TeacherName = "",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 16,
                SchoolTuid = 8,
                ClassroomNumber = "5",
                ClassroomSize = 28,
                GradeLevel = "Waddlers",
                TeacherName = "Ms. Kristy",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 17,
                SchoolTuid = 9,
                ClassroomNumber = "",
                ClassroomSize = 28,
                GradeLevel = "Pre K",
                TeacherName = "Tindell",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 18,
                SchoolTuid = 9,
                ClassroomNumber = "",
                ClassroomSize = 28,
                GradeLevel = "Pre K",
                TeacherName = "Mrs. Jaqueline Fuller",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 19,
                SchoolTuid = 9,
                ClassroomNumber = "",
                ClassroomSize = 28,
                GradeLevel = "",
                TeacherName = "Bouchinger",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 20,
                SchoolTuid = 9,
                ClassroomNumber = "",
                ClassroomSize = 28,
                GradeLevel = "2nd",
                TeacherName = "Mrs. Bates",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 21,
                SchoolTuid = 10,
                ClassroomNumber = "",
                ClassroomSize = 28,
                GradeLevel = "Pre K",
                TeacherName = "Ms. Shana",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 22,
                SchoolTuid = 10,
                ClassroomNumber = "",
                ClassroomSize = 28,
                GradeLevel = "Pre K",
                TeacherName = "Amy Fetter",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 23,
                SchoolTuid = 10,
                ClassroomNumber = "",
                ClassroomSize = 28,
                GradeLevel = "1st",
                TeacherName = "Ms. Gabby Moreno",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 24,
                SchoolTuid = 11,
                ClassroomNumber = "",
                ClassroomSize = 28,
                GradeLevel = "1st",
                TeacherName = "Howell",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 25,
                SchoolTuid = 12,
                ClassroomNumber = "",
                ClassroomSize = 28,
                GradeLevel = "2nd",
                TeacherName = "Shatz",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 26,
                SchoolTuid = 12,
                ClassroomNumber = "",
                ClassroomSize = 28,
                GradeLevel = "KDG",
                TeacherName = "Winchester",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 27,
                SchoolTuid = 12,
                ClassroomNumber = "",
                ClassroomSize = 28,
                GradeLevel = "1st",
                TeacherName = "Tata",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 28,
                SchoolTuid = 12,
                ClassroomNumber = "",
                ClassroomSize = 28,
                GradeLevel = "KDG",
                TeacherName = "Winchester",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 29,
                SchoolTuid = 12,
                ClassroomNumber = "",
                ClassroomSize = 28,
                GradeLevel = "4th",
                TeacherName = "Mata",
                IsDeleted = false
            });
            modelBuilder.Entity<Classroom>().HasData(new Classroom()
            {
                Tuid = 30,
                SchoolTuid = 12,
                ClassroomNumber = "",
                ClassroomSize = 0,
                GradeLevel = "",
                TeacherName = "M. Hill",
                IsDeleted = false
            });
        }
    }
}
