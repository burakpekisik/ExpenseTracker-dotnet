using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Tracker.Models
{
    public class Saving
    {
        [Key]
        public int SavingId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please Select a Category.")]
        public int SavingCategoryId { get; set; }
        public SavingCategory? SavingCategory { get; set; }

        public float CostPerUnit { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Amount Should be Greater Than 0.")]
        public float Amount { get; set; }

        [Column(TypeName = "nvarchar(75)")]
        public string? Description { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [NotMapped]
        public string? CategoryTitleWithIcon
        {
            get
            {
                return SavingCategory == null ? "" : SavingCategory.Icon + " " + SavingCategory.Title;
            }
        }

        [NotMapped]
        public float Total
        {
            get
            {
                return Amount * CostPerUnit;
            }
        }
    }
}
