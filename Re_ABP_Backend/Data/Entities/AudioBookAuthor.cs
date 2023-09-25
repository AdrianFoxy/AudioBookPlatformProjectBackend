namespace Re_ABP_Backend.Data.Entities
{
    public class AudioBookAuthor
    {
        public int AudioBookId { get; set; }
        public int AuthorId { get; set; }
        public AudioBook AudioBook { get; set; }
        public Author Author { get; set; }
    }
}
