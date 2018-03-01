namespace QDev.GamePointer.Wpf
{
    public class ApplicationFocusChangedEventArgs
    {
        public ApplicationFocusChangedEventArgs(int processId)
        {
            ProcessId = processId;
        }

        public ApplicationFocusChangedEventArgs(int processId, string processName)
            : this(processId)
        {
            ProcessName = processName;
        }

        public int ProcessId { get; }
        public string ProcessName { get; }
    }
}
