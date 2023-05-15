using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


/// <summary>
/// The Purpose of this file is to provide the schema for the InKindExpenseTypeItems table.
/// Stores information of what the expense was for, the amount, when it was made, and what 
/// type of expense it was.
/// </summary>
/// <author>Tyler P. Moody</author>
/// <created>1/20/23</created>
namespace A_FGMS.DataLayer.Entities
{
#pragma warning disable CS8618
    public class InKindExpenseTypeItem
    {
        [Key]
        public int Tuid { get; set; }

        [Column(TypeName = "varchar(45)")]
        public string? Name { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public double Value { get; set; }
        [Required]
        [ForeignKey("ExpenseTypeItem")]
        public int ExpenseTypeItemTuid { get; set; }
        public ExpenseTypeItem? ExpenseTypeItem { get; set; }
    }
}
