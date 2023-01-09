namespace web.Models {
    public class PostModel {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}