using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Splitwise_clone.Models
{
    public class TransactionViewModel
    {
        [Required]
        public double Amount { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public List<ParticipantViewModel> Participants { get; set; } = new List<ParticipantViewModel>();
    }

    public class ParticipantViewModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public double PaidAmount { get; set; }
    }
}
