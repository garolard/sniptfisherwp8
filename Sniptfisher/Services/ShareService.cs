using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sniptfisher.Services
{
    public class ShareService : Interfaces.IShareService
    {
        private enum ShareTypeEnum
        {
            ShareLink,
            ShareImage,
            ShareStatus,
            ShareByEmail
        }

        public void Share(string title, string message, string link = "", string image = "")
        {
            var availableShareTypes = GetAvailableShareTypes(title, message, link, image);
            OpenShareTypeSelector(availableShareTypes, title, message, link, image);
        }

        /// <summary>
        /// Devuelve una lista con los distintos métodos
        /// para compartir actualmente disponibles basándose en
        /// la validez de los datos que se van a compartir.
        /// </summary>
        /// <param name="title">El título para compartir.</param>
        /// <param name="message">El mensaje para compartir.</param>
        /// <param name="link">La URL del vínculo para compartir.</param>
        /// <param name="image">La URL de la imágen para compartir.</param>
        /// <returns>Una lista con todas las formas disponibles para compartir.</returns>
        private List<ShareTypeEnum> GetAvailableShareTypes(string title, string message, string link = "", string image = "")
        {
            var result = new List<ShareTypeEnum>();

            if (!string.IsNullOrEmpty(title)
                && !string.IsNullOrEmpty(message)
                && Uri.IsWellFormedUriString(link, UriKind.Absolute))
            {
                result.Add(ShareTypeEnum.ShareLink);
            }

            if (!string.IsNullOrEmpty(title)
                || !string.IsNullOrEmpty(message))
            {
                result.Add(ShareTypeEnum.ShareStatus);
            }

            if (!string.IsNullOrEmpty(title)
                && !string.IsNullOrEmpty(message))
            {
                result.Add(ShareTypeEnum.ShareByEmail);
            }

            return result;
        }

        /// <summary>
        /// Crea un popup a pantalla completa y le incrusta un <see cref="ListBox"/>
        /// con las distintas formas disponibles para compartir.
        /// </summary>
        /// <param name="availableShareTypes">Una lista que contiene las formas para compartir disponibles.</param>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="link"></param>
        /// <param name="image"></param>
        private void OpenShareTypeSelector(List<ShareTypeEnum> availableShareTypes, string title, string message, string link = "", string image = "")
        {
            // Hago una lista con las opciones disponibles
            var listSelector = new System.Windows.Controls.ListBox
            {
                ItemsSource = availableShareTypes,
                ItemTemplate = App.Current.Resources["SelectorItemTemplate"] as DataTemplate
            };

            // Cuando se seleccione alguna ejecuto alguno
            // de los servicios para compartir definidos más abajo
            listSelector.SelectionChanged += (sender, e) =>
                {
                    var selectedItem = (ShareTypeEnum)e.AddedItems[0];
                    switch (selectedItem)
                    {
                        case ShareTypeEnum.ShareLink:
                            ShareLink(title, message, link);
                            break;
                        case ShareTypeEnum.ShareStatus:
                            //ShareStatus(string.IsNullOrEmpty(message) ? title : message);
                            break;
                        case ShareTypeEnum.ShareByEmail:
                            ShareByEmail(title, message, link);
                            break;
                        default:
                            break;
                    }
                };

            var customMessageBox = new CustomMessageBox 
            { 
                Message = "Comparte un snipt",
                Content = listSelector,
                IsLeftButtonEnabled = false,
                IsRightButtonEnabled = false,
                IsFullScreen = true
            };

            customMessageBox.Show();
        }

        private void ShareLink(string title, string message, string link = "")
        {
            title = string.IsNullOrEmpty(title) ? string.Empty : title;
            message = string.IsNullOrEmpty(message) ? string.Empty : message;
            var linkUri = string.IsNullOrEmpty(link) ? new Uri(string.Empty) : new Uri(link, UriKind.Absolute);

            var shareLinkTask = new ShareLinkTask 
            {
                Title = title,
                Message = message,
                LinkUri = linkUri
            };

            shareLinkTask.Show();
        }

        private void ShareStatus(string status)
        {
            throw new NotImplementedException();
        }

        private void ShareByEmail(string subject, string body, string link = "")
        {
            subject = string.IsNullOrEmpty(subject) ? string.Empty : subject;
            body = string.IsNullOrEmpty(body) ? string.Empty : body;
            var linkUri = string.IsNullOrEmpty(link) ? new Uri(string.Empty) : new Uri(link, UriKind.Absolute);

            var emailComposeTask = new EmailComposeTask();

            emailComposeTask.Subject = subject;

            if (string.IsNullOrEmpty(link))
                emailComposeTask.Body = body;
            else
                emailComposeTask.Body = string.Format("{0}{1}{1}{2}", body, Environment.NewLine, link);

            emailComposeTask.Show();
        }
    }
}
