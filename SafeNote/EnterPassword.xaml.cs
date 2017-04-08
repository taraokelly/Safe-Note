using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private void login_Click(object sender, RoutedEventArgs e)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            if (passwordBox.Password.Equals((string)localSettings.Values["password"]))
            {
                this.Frame.Navigate(typeof(Notes), null);
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

        private async void OnBackKeyPress(object sender, CancelEventArgs e)
        {
            //Code to disable the back button
            e.Cancel = true;
            var dialog = new MessageDialog("Do you wish to exit app?");
            dialog.Title = "SafeNote";

            dialog.Commands.Add(new UICommand { Label = "Yes", Id = 0 });
            dialog.Commands.Add(new UICommand { Label = "No", Id = 1 });
            var result = await dialog.ShowAsync();

            if ((int)result.Id == 0)
            {
                Application.Current.Exit();
                
            }
        }

        #endregion
    }
}
