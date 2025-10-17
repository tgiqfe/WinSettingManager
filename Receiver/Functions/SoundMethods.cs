using Receiver.DataContact;
using WinSettingManager.Lib.SoundVolume;

namespace Receiver.Functions
{
    public class SoundMethods
    {
        public static async Task<SoundVolumeDataContact> GetSoundVolume()
        {
            return await Task.Run(() =>
            {
                var volSummary = VolumeSummary.Load(); ;
                return new SoundVolumeDataContact()
                {
                    Level = volSummary.Level,
                    IsMute = volSummary.IsMute
                };
            });
        }

        public static async Task<SoundVolumeDataContact> SetSoundVolume(SoundVolumeDataContact contact)
        {
            return await Task.Run(() =>
            {
                if (contact.Level != null) { Sound.SetVolume((float)contact.Level / 100); }
                if (contact.IsMute != null) { Sound.SetMute((bool)contact.IsMute); }

                var volSummary = VolumeSummary.Load();
                return new SoundVolumeDataContact()
                {
                    Level = volSummary.Level,
                    IsMute = volSummary.IsMute
                };
            });
        }
    }
}
