namespace QDev.GamePointer.Wpf
{
    public class ApplicationFocusChangedEventArgs
    {
        public ApplicationFocusChangedEventArgs(int processId)
        {
            ProcessId = processId;
        }

        public ApplicationFocusChangedEventArgs(int processId, string processExecutionPath)
            : this(processId)
        {
            ProcessExecutionPath = processExecutionPath;
        }

        public int ProcessId { get; }
        public string ProcessExecutionPath { get; }
    }
}
