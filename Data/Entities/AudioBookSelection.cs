namespace ABP_Backend.Data.Entities
{
    public class AudioBookSelection
    {
        public int AudioBookId { get; set; }
        public int BookSelectionId { get; set; }
        public AudioBook AudioBook { get; set; }
        public BookSelection BookSelection { get; set; }
    }
}
