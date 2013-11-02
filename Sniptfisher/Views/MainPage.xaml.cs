using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Sniptfisher.Resources;
using System.Windows.Data;

namespace Sniptfisher
{
    public partial class MainPage : PhoneApplicationPage
    {
        private const int itemsAfterNextLoad = 7;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();

            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as ViewModel.MainViewModel;
            var progressIndicator = SystemTray.ProgressIndicator;
            if (progressIndicator != null)
            {
                return;
            }

            progressIndicator = new ProgressIndicator();

            SystemTray.SetProgressIndicator(this, progressIndicator);

            Binding binding = new Binding("IsLoading") { Source = context };
            BindingOperations.SetBinding(
                progressIndicator, ProgressIndicator.IsVisibleProperty, binding);

            binding = new Binding("IsLoading") { Source = context };
            BindingOperations.SetBinding(
                progressIndicator, ProgressIndicator.IsIndeterminateProperty, binding);

            progressIndicator.Text = "Loading new snipts...";
        }

        async protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var context = this.DataContext as ViewModel.MainViewModel;
            if (!context.IsDataLoaded)
                await context.LoadDataAsync();
            base.OnNavigatedTo(e);
        }

        async private void LongListSelector_ItemRealized(object sender, ItemRealizationEventArgs e)
        {
            var context = this.DataContext as ViewModel.MainViewModel;
            if (!context.IsLoading && context.Items != null && context.Items.Count >= itemsAfterNextLoad)
            {
                if (e.ItemKind == LongListSelectorItemKind.Item)
                {
                    if ((e.Container.Content as Sniptfisher.Model.Public.SniptModel).Equals(context.Items[context.Items.Count - itemsAfterNextLoad]))
                    {
                        await context.LoadExtraItems();
                    }
                }
            }
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}