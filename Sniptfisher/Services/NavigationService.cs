using System;
using System.Collections.Generic;
using Sniptfisher.ViewModel.Interfaces;
using Microsoft.Phone.Controls;
using System.Windows;

namespace Sniptfisher.Services
{
    public class NavigationService : Interfaces.INavigationService
    {
        private static Dictionary<Type, string> viewModelRouting = new Dictionary<Type, string>()
        {
            { typeof(IMainViewModel), "/Views/MainPage.xaml" },
            { typeof(IDetailViewModel), "/Views/DetailPage.xaml" },
            { typeof(ISearchViewModel), "/Views/SearchPage.xaml" }
        };

        private object _navigationContext;

        public void NavigateTo<TDestinationViewModel>()
        {
            PhoneApplicationFrame rootFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            rootFrame.Navigate(new Uri(viewModelRouting[typeof(TDestinationViewModel)], UriKind.Relative));
        }

        public void NavigateTo<TDestinationViewModel>(object navigationContext)
        {
            // Preparo el objeto complejo pasado por navegación
            this._navigationContext = navigationContext;

            PhoneApplicationFrame rootFrame = Application.Current.RootVisual as PhoneApplicationFrame;

            // Avísame cuando llegues a la página destino
            rootFrame.Navigated += rootFrame_Navigated;

            // Empieza a navegar
            rootFrame.Navigate(new Uri(viewModelRouting[typeof(TDestinationViewModel)], UriKind.Relative));
        }

        // Hemos llegado a la página a la que queremos pasarle el parámetro
        void rootFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            PhoneApplicationFrame rootFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            rootFrame.Navigated -= rootFrame_Navigated;

            // Dame el contexto de la página y establece el objeto pasado
            var page = e.Content as PhoneApplicationPage;
            var context = page.DataContext as ViewModel.Interfaces.INavigable;
            context.NavigationContext = this._navigationContext;
        }

        public void NavigateBack()
        {
            PhoneApplicationFrame rootFrame = Application.Current.RootVisual as PhoneApplicationFrame;

            if (rootFrame.CanGoBack)
                rootFrame.GoBack();
        }

        public void NavigateBack(object navigationContext)
        {
            this._navigationContext = navigationContext;
            PhoneApplicationFrame rootFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            rootFrame.Navigated += rootFrame_Navigated;

            if (rootFrame.CanGoBack)
                rootFrame.GoBack();
        }

        public void ClearNavigationHistory()
        {
            PhoneApplicationFrame rootFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            while (rootFrame.RemoveBackEntry() != null) ;
        }
    }
}
