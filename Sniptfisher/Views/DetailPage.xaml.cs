using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Sniptfisher
{
    public partial class DetailPage : PhoneApplicationPage
    {
        public DetailPage()
        {
            InitializeComponent();
        }

        async protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var context = this.DataContext as ViewModel.DetailViewModel;
            if (!context.IsRelatedDataLoaded)
                await context.LoadRelatedItems();
            base.OnNavigatedTo(e);
        }
    }
}