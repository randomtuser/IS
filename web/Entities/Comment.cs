namespace web.Entities 
{
    public class Comment 
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public User CreatedBy { get; set; }
    }
}