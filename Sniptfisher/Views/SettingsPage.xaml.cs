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

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
            (this.DataContext as ViewModel.SettingsViewModel).NavigateBackCommand.Execute(null);
        }

        private void PivotItemsVisibilityHandler(object sender, RoutedEventArgs e)
        {
            this.dataContext = this.DataContext as ViewModel.SettingsViewModel;

            // Elimino los PivotItem del árbol de widgets, no se
            // pueden agregar a otro widget si antes ya son hijos de algún otro
            this.ContentPanel.Children.Remove(NotLoggedPivotItem);
            this.ContentPanel.Children.Remove(LoggedPivotItem);

            if (this.dataContext.IsLogged)
            {
                // Escondo y elimino el PivotItem que pide usuario y
                // contraseña ya que en este ámbito el usuario está logueado
                NotLoggedPivotItem.Visibility = System.Windows.Visibility.Collapsed;
                AccountPivot.Items.Remove(NotLoggedPivotItem);

                this.dataContext.TrySetLoggedUserCommand.Execute(null);

                // Inserto el PivotItem con el perfil del usuario logueado
                // y lo vuelvo visible
                AccountPivot.Items.Insert(AccountPivot.SelectedIndex, LoggedPivotItem);
                LoggedPivotItem.Visibility = System.Windows.Visibility.Visible;
                AccountPivot.SelectedItem = LoggedPivotItem;
            }
            else
            {
                // En este bloque se hace exactamente lo contratio
                // que en el anterior
                LoggedPivotItem.Visibility = System.Windows.Visibility.Collapsed;
                AccountPivot.Items.Remove(LoggedPivotItem);

                AccountPivot.Items.Insert(AccountPivot.SelectedIndex, NotLoggedPivotItem);
                NotLoggedPivotItem.Visibility = System.Windows.Visibility.Visible;
                AccountPivot.SelectedItem = NotLoggedPivotItem;
            }

            // Asocio un evento que hace lo mismo que este método cuando
            // el estado de login del usuario cambia.
            // Esto contempla el escenario de login y logout en la pantalla
            // de preferencias.
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

                    this.dataContext.TrySetLoggedUserCommand.Execute(null);

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