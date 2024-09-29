namespace Splitwise_clone.Models{

public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<Participant> Participants { get; set; }
    }
}