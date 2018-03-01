using System;
using System.Runtime.InteropServices;
using System.Text;

namespace QDev.GamePointer.Wpf
{
    public class ProcessHelper
    {
        [DllImport("kernel32.dll")]
        private static extern bool QueryFullProcessImageName(IntPtr hprocess, int dwFlags, StringBuilder lpExeName, out int size);
        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hHandle);

        public static string GetExecutionPath(int processId)
        {
            var buffer = new StringBuilder(1024);
            var hprocess = OpenProcess(ProcessAccessFlags.PROCESS_QUERY_LIMITED_INFORMATION, false, processId);
            if (hprocess != IntPtr.Zero)
            {
                try
                {
                    var size = buffer.Capacity;
                    if (QueryFullProcessImageName(hprocess, 0, buffer, out size))
                    {
                        return buffer.ToString();
                    }
                }
                finally
                {
                    CloseHandle(hprocess);
                }
            }
            return string.Empty;
        }

        [Flags]
        private enum ProcessAccessFlags : uint
        {
            PROCESS_QUERY_LIMITED_INFORMATION = 0x1000
        }
    }
}