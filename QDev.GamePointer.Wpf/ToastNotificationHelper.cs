using Microsoft.Toolkit.Uwp.Notifications;
using System;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace QDev.GamePointer.Wpf
{
    public class ToastNotificationHelper
    {
        public static void Show(string title, string body)
        {
            ToastVisual visual = new ToastVisual()
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
            ToastContent toastContent = new ToastContent() { Visual = visual };

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(toastContent.GetContent());

            var toast = new ToastNotification(doc)
            {
                ExpirationTime = DateTime.Now.AddSeconds(3)
            };
            ToastNotificationManager.History.Clear();
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
