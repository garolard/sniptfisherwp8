using System.Windows;

namespace Sniptfisher.Services
{
    public class DialogService : Interfaces.IDialogService
    {
        public void Show(string message)
        {
            MessageBox.Show(message);
        }

        public void Show(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK);
        }
    }
}
