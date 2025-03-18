namespace SPT.Fika.Models
{
    public class FikaPlayer
    {
        public string nickname { get; set; } = string.Empty;
        public int level { get; set; }
        public int activity { get; set; }
        public long activityStartedTimeStamp { get; set; }
        public FikaRaid RaidInformation { get; set; } = null!;
    }
}
