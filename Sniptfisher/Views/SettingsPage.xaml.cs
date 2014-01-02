using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Sniptfisher.Views
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        private ViewModel.SettingsViewModel dataContext;

        public SettingsPage()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(this.PivotItemsVisibilityHandler);
        }

        private void PivotItemsVisibilityHandler(object sender, RoutedEventArgs e)
        {
            this.dataContext = this.DataContext as ViewModel.SettingsViewModel;
            this.ContentPanel.Children.Remove(NotLoggedPivotItem);
            this.ContentPanel.Children.Remove(LoggedPivotItem);

            if (this.dataContext.IsLogged)
            {
                NotLoggedPivotItem.Visibility = System.Windows.Visibility.Collapsed;
                AccountPivot.Items.Remove(NotLoggedPivotItem);

                AccountPivot.Items.Insert(AccountPivot.SelectedIndex, LoggedPivotItem);
                LoggedPivotItem.Visibility = System.Windows.Visibility.Visible;
                AccountPivot.SelectedItem = LoggedPivotItem;
            }
            else
            {
                LoggedPivotItem.Visibility = System.Windows.Visibility.Collapsed;
                AccountPivot.Items.Remove(LoggedPivotItem);

                AccountPivot.Items.Insert(AccountPivot.SelectedIndex, NotLoggedPivotItem);
                NotLoggedPivotItem.Visibility = System.Windows.Visibility.Visible;
                AccountPivot.SelectedItem = NotLoggedPivotItem;
            }

            this.dataContext.PropertyChanged += dataContext_PropertyChanged;
        }

        void dataContext_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("IsLogged"))
            {
                AccountPivot.SelectedIndex = 0;
                if (this.dataContext.IsLogged)
                {
                    NotLoggedPivotItem.Visibility = System.Windows.Visibility.Collapsed;
                    AccountPivot.Items.Remove(NotLoggedPivotItem);

                    AccountPivot.Items.Insert(0, LoggedPivotItem);
                    LoggedPivotItem.Visibility = System.Windows.Visibility.Visible;
                    AccountPivot.SelectedItem = LoggedPivotItem;
                }
                else
                {
                    LoggedPivotItem.Visibility = System.Windows.Visibility.Collapsed;
                    AccountPivot.Items.Remove(LoggedPivotItem);

                    AccountPivot.Items.Insert(0, NotLoggedPivotItem);
                    NotLoggedPivotItem.Visibility = System.Windows.Visibility.Visible;
                    AccountPivot.SelectedItem = NotLoggedPivotItem;
                }
            }
        }
    }
}