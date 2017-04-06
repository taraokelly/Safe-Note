using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace SafeNote
{
    public sealed partial class EnterPassword : Page
    {
        #region Constructor

        public EnterPassword()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Navigation Events

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Camera.IsEnabled = true;
        }

        #endregion

        #region Click Events

        private void Camera_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), null);
        }

        private async void login_Click(object sender, RoutedEventArgs e)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            if (passwordBox.Password.Equals((string)localSettings.Values["password"]))
            {
                var dialog = new MessageDialog("Authentication Successful!");
                await dialog.ShowAsync();
            }
            if (passwordBox.Password.Equals(""))
            {
                // Ignore - invalid input
            }
            else
            {
                outputBox.Text = "Invalid Password. Access Denied.";
            }
        }

        #endregion
    }
}
