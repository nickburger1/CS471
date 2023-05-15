using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


/// <summary>
/// The Purpose of this file is to provide the schema for the ExpenseTypeItems table. This table will hold 
/// types of expenses to be used for in-kind expense items. An expense type can be associated with many InKindExpenseItemTypes.
/// </summary>
/// <author>Tyler P. Moody</author>
/// <created>1/20/23</created>
namespace A_FGMS.DataLayer.Entities
{
#pragma warning disable CS8618
    public class ExpenseTypeItem
    {
        [Key]
        public int Tuid { get; set; }

        [Required]
        [Column(TypeName = "varchar(45)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Description { get; set; }
    }
}
