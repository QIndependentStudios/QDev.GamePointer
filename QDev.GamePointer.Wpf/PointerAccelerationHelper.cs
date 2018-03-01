using System;
using System.Runtime.InteropServices;

namespace QDev.GamePointer.Wpf
{
    public class SystemPointerHelper
    {
        private const UInt32 SPI_GETMOUSE = 0x0003;
        private const UInt32 SPI_SETMOUSE = 0x0004;
        private const int EnhancePointerPrecisionParamIndex = 2;

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo", SetLastError = true)]
        private static extern bool SystemParametersInfo(uint action, uint param, IntPtr vparam, SPIF fWinIni);

        private static int[] GetMouseParams()
        {
            var mouseParams = new int[3];
            SystemParametersInfo(SPI_GETMOUSE, 0, GCHandle.Alloc(mouseParams, GCHandleType.Pinned).AddrOfPinnedObject(), 0);
            return mouseParams;
        }

        public static bool GetEnhancePointerPrecision()
        {
            var mouseParams = GetMouseParams();
            return mouseParams[EnhancePointerPrecisionParamIndex] != 0;
        }

        public static void SetEnhancePointerPrecision(bool isEnabled)
        {
            int[] mouseParams = GetMouseParams();
            mouseParams[EnhancePointerPrecisionParamIndex] = isEnabled ? 1 : 0;

            SystemParametersInfo(SPI_SETMOUSE, 0, GCHandle.Alloc(mouseParams, GCHandleType.Pinned).AddrOfPinnedObject(), SPIF.SPIF_SENDCHANGE);
        }

        private enum SPIF
        {
            None = 0x00,
            /// <summary>Writes the new system-wide parameter setting to the user profile.</summary>
            SPIF_UPDATEINIFILE = 0x01,
            /// <summary>Broadcasts the WM_SETTINGCHANGE message after updating the user profile.</summary>
            SPIF_SENDCHANGE = 0x02,
            /// <summary>Same as SPIF_SENDCHANGE.</summary>
            SPIF_SENDWININICHANGE = 0x02
        }
    }
}
