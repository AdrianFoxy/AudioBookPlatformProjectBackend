namespace Re_ABP_Backend.Data.Dtos.AdminManagmentDtos.AudioBooksDtos.AudioFiles
{
    public class AddAudioFile
    {
        public string Name { get; set; }
        public string AudioFileUrl { get; set; }
        public int Duration { get; set; }
        public int PlaybackQueue { get; set; }
    }
}
