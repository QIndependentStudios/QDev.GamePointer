using Microsoft.Toolkit.Uwp.Notifications;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace QDev.GamePointer.Wpf
{
    public class ToastNotificationHelper
    {
        public static void Show(string title, string body)
        {
            var visual = new ToastVisual
            {
                BindingGeneric = new ToastBindingGeneric()
                {
                    Children =
                    {
                        new AdaptiveText() { Text = title },
                        new AdaptiveText() { Text = body }
                    }
                }
            };
            var audio = new ToastAudio
            {
                Silent = true
            };
            ToastContent toastContent = new ToastContent()
            {
                Visual = visual,
                Audio = audio
            };

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(toastContent.GetContent());

            var toast = new ToastNotification(doc);
            ToastNotificationManager.History.Clear();
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
