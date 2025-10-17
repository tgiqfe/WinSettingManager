using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static WinSettingManager.Lib.SoundVolume.PInvoke;

namespace WinSettingManager.Lib.SoundVolume
{
    public class Sound
    {
        private static IAudioEndpointVolume Vol()
        {
            IMMDeviceEnumerator enumerator = new MMDeviceEnumeratorComObject() as IMMDeviceEnumerator;
            IMMDevice dev = null;
            Marshal.ThrowExceptionForHR(enumerator.GetDefaultAudioEndpoint(0, 1, out dev));
            IAudioEndpointVolume epv = null;
            Guid epvid = typeof(IAudioEndpointVolume).GUID;
            Marshal.ThrowExceptionForHR(dev.Activate(ref epvid, 23, 0, out epv));
            return epv;
        }

        #region Directory.

        /// <summary>
        /// Set the volume directly.
        /// </summary>
        public static float Volume
        {
            get
            {
                Marshal.ThrowExceptionForHR(Vol().GetMasterVolumeLevelScalar(out float volume));
                return volume;
            }
            set
            {
                Marshal.ThrowExceptionForHR(Vol().SetMasterVolumeLevelScalar(value, Guid.Empty));
            }
        }

        public static bool Mute
        {
            get
            {
                Marshal.ThrowExceptionForHR(Vol().GetMute(out bool mute));
                return mute;
            }
            set
            {
                Marshal.ThrowExceptionForHR(Vol().SetMute(value, Guid.Empty));
            }
        }

        #endregion

        /// <summary>
        /// Set volume from float parameter.
        /// </summary>
        /// <param name="volume"></param>
        public static void SetVolume(float volume)
        {
            Marshal.ThrowExceptionForHR(Vol().SetMasterVolumeLevelScalar(volume, Guid.Empty));
        }

        /// <summary>
        /// Get volume as float.
        /// </summary>
        /// <returns></returns>
        public static float GetVolume()
        {
            Marshal.ThrowExceptionForHR(Vol().GetMasterVolumeLevelScalar(out float volume));
            return volume;
        }

        /// <summary>
        /// Set mute.
        /// </summary>
        /// <param name="mute"></param>
        public static void SetMute(bool mute)
        {
            Marshal.ThrowExceptionForHR(Vol().SetMute(mute, Guid.Empty));
        }

        /// <summary>
        /// Get mute.
        /// </summary>
        /// <returns></returns>
        public static bool GetMute()
        {
            Marshal.ThrowExceptionForHR(Vol().GetMute(out bool mute));
            return mute;
        }
    }
}
