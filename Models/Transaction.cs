namespace Splitwise_clone.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public int CreatedBy { get; set; } 
        public User Creator { get; set; } 

        public List<Participant> Participants { get; set; }
    }
}
