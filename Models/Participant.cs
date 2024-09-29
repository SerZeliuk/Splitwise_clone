namespace Splitwise_clone.Models {


 public class Participant
    {
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public double ShareAmount { get; set; }
        public double PaidAmount { get; set; }
    }
}
