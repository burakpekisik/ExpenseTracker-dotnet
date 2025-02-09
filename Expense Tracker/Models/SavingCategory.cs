using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Tracker.Models
{
    public class SavingCategory
    {
        [Key]
        public int CategoryId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Title is Required.")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(5)")]
        public string Icon { get; set; } = "";

        [Column(TypeName = "nvarchar(10)")]
        public string Type { get; set; }

        public float Amount { get; set; } = 0;
        public float TotalCost { get; set; } = 0;
        public float? CostPerUnit { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string? Code { get; set; }

        public float? CurrentPrice { get; set; }
        public float? TotalValue { get; set; }

        [NotMapped]
        public string? TitleWithIcon
        {
            get
            {
                return this.Icon + " " + this.Title;
            }
        }
    }
}
