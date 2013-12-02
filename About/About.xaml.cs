using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.Xml;
using System.Windows.Resources;
using System.IO;
using System.Windows.Shapes;

namespace About
{
    public partial class About : PhoneApplicationPage
    {
        StackPanel licenses;

        public About()
        {
            InitializeComponent();
        }

        private void HyperlinkButon_Click(object sender, RoutedEventArgs e)
        {
            string s = ((Button)sender).Tag as string;

            switch (s)
            {
                case "Review":
                    var task = new MarketplaceReviewTask();
                    task.Show();
                    break;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            XmlReaderSettings settings = new XmlReaderSettings()
            {
                XmlResolver = new XmlXapResolver()
            };

            using (var reader = XmlReader.Create("WMAppManifest.xml", settings))
            {
                reader.ReadToDescendant("App");

                var appVersion = reader.GetAttribute("Version");
                _versionText.Text = appVersion;
            }

            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// Parse the LICENSE.txt file and display its content in the
        /// "legal" page section.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Pivot pivot = (Pivot)sender;
            if (pivot.SelectedIndex > 0 && licenses == null)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    licenses = new StackPanel();

                    // Retrieve license file stream and read it to display it
                    StreamResourceInfo sri = Application.GetResourceStream(
                        new Uri("LICENSE.txt", UriKind.Relative));

                    if (sri != null)
                    {
                        using (StreamReader reader = new StreamReader(sri.Stream))
                        {
                            string line;
                            bool lastWasEmpty = true;
                            do
                            {
                                line = reader.ReadLine();

                                // Empty string == New section
                                if (line == string.Empty)
                                {
                                    Rectangle rec = new Rectangle()
                                    {
                                        Height = 20
                                    };
                                    licenses.Children.Add(rec);
                                    lastWasEmpty = true;
                                }
                                else
                                {
                                    TextBlock tb = new TextBlock()
                                    {
                                        TextWrapping = TextWrapping.Wrap,
                                        Text = line,
                                        Style = (Style)Application.Current.Resources["PhoneTextNormalStyle"]
                                    };
                                    if (!lastWasEmpty) // If this line is not the first, it won't be totally opaque
                                    {
                                        tb.Opacity = 0.7;
                                    }

                                    lastWasEmpty = false;
                                    licenses.Children.Add(tb);
                                }
                            } while (line != null);
                        }
                    }

                    licenseScrollViewer.Content = licenses;
                });
            }
        }
    }
}