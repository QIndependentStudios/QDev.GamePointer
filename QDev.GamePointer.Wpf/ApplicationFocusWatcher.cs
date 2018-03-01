using System;
using System.Diagnostics;
using System.Windows.Automation;

namespace QDev.GamePointer.Wpf
{
    public sealed class ApplicationFocusWatcher
    {
        public event EventHandler<ApplicationFocusChangedEventArgs> ApplicationFocusChanged;

        private int _focusedProcessId;

        public void Start()
        {
            Automation.AddAutomationFocusChangedEventHandler(FocusChangedHandler);
        }

        public void Stop()
        {
            Automation.RemoveAutomationFocusChangedEventHandler(FocusChangedHandler);
        }

        private void FocusChangedHandler(object src, AutomationFocusChangedEventArgs args)
        {
            var element = src as AutomationElement;
            try
            {
                if (element != null && _focusedProcessId != element.Current.ProcessId)
                {
                    var previousFocusedProcessId = _focusedProcessId;
                    _focusedProcessId = element.Current.ProcessId;
                    OnApplicationFocusChanged(previousFocusedProcessId, _focusedProcessId);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.GetType().Name}, {ex.Message}");
            }
        }

        private void OnApplicationFocusChanged(int oldProcessId, int newProcessId)
        {
            if (ApplicationFocusChanged == null || oldProcessId == newProcessId)
                return;

            var args = new ApplicationFocusChangedEventArgs(newProcessId);
            try
            {
                args = new ApplicationFocusChangedEventArgs(newProcessId, ProcessHelper.GetExecutionPath(newProcessId));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.GetType().Name}, {ex.Message}");
            }
            finally
            {
                ApplicationFocusChanged(this, args);
            }
        }
    }
}