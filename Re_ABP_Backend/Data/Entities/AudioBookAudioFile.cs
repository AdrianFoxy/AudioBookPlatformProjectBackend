namespace Re_ABP_Backend.Data.Entities
{
    public class AudioBookAudioFile
    {
        public int AudioBookId { get; set; }
        public int BookAudioFileId { get; set; }
        public AudioBook AudioBook { get; set; }
        public BookAudioFile BookAudioFile { get; set; }
    }
}
