using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_FGMS.DataLayer.Entities
{
#pragma warning disable CS8618
    public class Classroom
    {        
        [Key]
        public int Tuid { get; set; }

        [Required]
        [ForeignKey("School")]
        public int SchoolTuid { get; set; }
        public virtual School? School { get; set; }

        [Column(TypeName = "varchar(45)")]
        public string? ClassroomNumber { get; set; }

        public int? ClassroomSize { get; set;}

        [Column(TypeName = "varchar(45)")]
        public string? GradeLevel { get; set;}
        
        [Column(TypeName = "varchar(45)")]
        public string? TeacherName { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
