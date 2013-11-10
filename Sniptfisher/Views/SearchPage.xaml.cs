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
using System.Windows.Data;

namespace Sniptfisher.Views
{
    public partial class SearchPage : PhoneApplicationPage
    {
        private string _searchTerm;
        private ViewModel.SearchViewModel context;

        public SearchPage()
        {
            InitializeComponent();
            this.context = this.DataContext as ViewModel.SearchViewModel;
            Loaded += new RoutedEventHandler(this.OnLoaded);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var progressIndicator = SystemTray.ProgressIndicator;
            if (progressIndicator != null)
                return;

            progressIndicator = new ProgressIndicator();

            SystemTray.SetProgressIndicator(this, progressIndicator);

            Binding binding = new Binding("IsLoading") { Source = this.context };
            BindingOperations.SetBinding(
                progressIndicator, ProgressIndicator.IsVisibleProperty, binding);

            binding = new Binding("IsLoading") { Source = this.context };
            BindingOperations.SetBinding(
                progressIndicator, ProgressIndicator.IsIndeterminateProperty, binding);

            progressIndicator.Text = "Searching snipts...";
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

                this.context.DoSearchCommand.Execute(_searchTerm);
            }
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchTextBox.SelectAll();
        }
    }
}