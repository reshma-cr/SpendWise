using SpendWise.Enums;
using System.ComponentModel.DataAnnotations;

namespace SpendWise.Models.ViewModels
{
    public class CreateExpenseViewModel
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string? Description { get; set; } = string.Empty;
        public TransactionType Type { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
