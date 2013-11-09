using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Devices;

namespace Sniptfisher.Views
{
    public partial class SearchPage : PhoneApplicationPage
    {
        private string _searchTerm;

        public SearchPage()
        {
            InitializeComponent();
        }

        private void SearchTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                _searchTerm = SearchTextBox.Text.Trim();

                if (string.IsNullOrEmpty(_searchTerm))
                {
                    VibrateController.Default.Start(TimeSpan.FromMilliseconds(200));
                    return;
                }

                this.Focus();

                (this.DataContext as ViewModel.SearchViewModel).DoSearchCommand.Execute(_searchTerm);
            }
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchTextBox.SelectAll();
        }
    }
}