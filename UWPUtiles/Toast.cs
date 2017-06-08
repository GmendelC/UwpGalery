using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace UWPUtiles
{
     public class Toast
    {
        public static void SendToToast(Exception e)
        {
            try
            {

                var xmlToastTemplate = "<toast launch=\"app-defined-string\">" +
                              "<visual>" +
                                "<binding template =\"ToastGeneric\">" +
                                  "<text>" + "Erro" + "</text>" +
                                  "<text>" +
                                   e.ToString() +
                                  "</text>" +
                                "</binding>" +
                              "</visual>" +
                            "</toast>";

                // load the template as XML document
                Windows.Data.Xml.Dom.XmlDocument xmlDocument = new Windows.Data.Xml.Dom.XmlDocument();
                xmlDocument.LoadXml(xmlToastTemplate);

                // create the toast notification and show to user
                var toastNotification = new ToastNotification(xmlDocument);
                var notification = ToastNotificationManager.CreateToastNotifier();
                toastNotification.ExpirationTime = DateTime.Now.AddSeconds(10);
                notification.Show(toastNotification);

            }
            catch (Exception ex)
            {
            }
        }
    }
}
