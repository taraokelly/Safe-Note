using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SafeNote
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class APIKey : Page
    {
        public APIKey()
        {
            this.InitializeComponent();
        }

        private void keyBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (keyBox.Text.Contains(" "))
            {
                submit.IsEnabled = false;
                keyErrorBox.Text = "Key cannot contain any spaces.";
            }
            else if (keyBox.Text.Length < 32)
            {
                submit.IsEnabled = false;
                keyErrorBox.Text = "Key is too short.";
            }
            else if (keyBox.Text.Length > 32)
            {
                submit.IsEnabled = false;
                keyErrorBox.Text = "Key is too long.";
            }
            else
            {
                submit.IsEnabled = true;
                keyErrorBox.Text = "";
            }
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            localSettings.Values["key"] = keyBox.Text;

            next.IsEnabled = true;
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            keyBox.Text = localSettings.Values["key"].ToString();

            if((string)localSettings.Values["key"] != "")
            {
                next.IsEnabled = true;
            }
        }

        private void continue_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Settings), null);
        }
    }
}
