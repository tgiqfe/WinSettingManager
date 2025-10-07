namespace WinSettingManager.Lib.TuneVolume
{
    internal class VolumeSummary
    {
        public int Level { get; set; }
        public bool IsMute { get; set; }

        public static VolumeSummary Load()
        {
            return new VolumeSummary()
            {
                Level = (int)(Math.Round(Sound.GetVolume(), 2, MidpointRounding.AwayFromZero) * 100),
                IsMute = Sound.GetMute()
            };
        }

        public static VolumeSummary Create(int level, bool isMute)
        {
            return new VolumeSummary()
            {
                Level = level,
                IsMute = isMute,
            };
        }
    }
}
