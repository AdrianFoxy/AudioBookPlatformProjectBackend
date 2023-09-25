namespace Re_ABP_Backend.Data.Entities
{
    public class AudioBookGenre
    {
        public int AudioBookId { get; set; }
        public int GenreId { get; set; }
        public AudioBook AudioBook { get; set; }
        public Genre Genre { get; set; }
    }
}
