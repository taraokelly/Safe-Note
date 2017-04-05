using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Microsoft.ProjectOxford.Face.Contract;

namespace SafeNote
{
    public sealed partial class Settings : Page
    {
        #region Global Variables

        bool passwordValid, passwordCheckValid, photoValid;

        CameraCaptureUI captureUI = new CameraCaptureUI();
        StorageFile photo;
        IRandomAccessStream imageStream;

        const string APIKEY = "fe355e1480a24916aa8a641b6cf29c7b";
        FaceServiceClient serviceClient = new FaceServiceClient(APIKEY);

        #endregion

        #region Constructor

        public Settings()
        {
            this.InitializeComponent();

            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Windows.Foundation.Size(130, 130);
        }

        #endregion

        #region Click Events

        private async void Camera_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

                if (photo == null)
                {
                    //user cancelled photo
                    return;
                }
                else
                {
                    imageStream = await photo.OpenAsync(FileAccessMode.Read);
                    BitmapDecoder decoder = await BitmapDecoder.CreateAsync(imageStream);
                    SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync();

                    SoftwareBitmap softwareBitmapBGR8 = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
                    SoftwareBitmapSource bitmapSource = new SoftwareBitmapSource();
                    await bitmapSource.SetBitmapAsync(softwareBitmapBGR8);

                    image.Source = bitmapSource;
                    try
                    {
                        Face[] faces = await serviceClient.DetectAsync(imageStream.AsStream());
 
                        if (faces.Length > 0)
                        {     
                            var id = faces[0].FaceId;
                            // save FaceID here
                            //imageErrorBox.Text = "Face Detected.";
                            photoValid = true;
                            if(passwordCheckValid==true && passwordValid == true)
                            {
                                submit.IsEnabled = true;
                            }
                                
                        }
                        else
                        {
                            // disable submit button
                            submit.IsEnabled = false;
                            imageErrorBox.Text = "Error detecting face. Are you visibile in the photo? ";
                        }
                    }
                    catch
                    {
                        // disable submit button
                        submit.IsEnabled = false;
                        imageErrorBox.Text = "Error detecting face. Have you entered the correct key?";
                    }
                }
            }
            catch
            {
                // disable submit button here
                imageErrorBox.Text = "Error taking photo";
            }

        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Property Changed Events

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password.Contains(" "))
            {
                passwordValid = false;
                submit.IsEnabled = false;
                passwordErrorBox.Text = "Password cannot contain any spaces.";
            }
            else if (passwordBox.Password.Length <= 7)
            {
                passwordValid = false;
                submit.IsEnabled = false;
                passwordErrorBox.Text = "Password must be 8 characters long.";
            }
            else
            {
                passwordValid = true;
                if(passwordCheckValid == true && photoValid == true)
                {
                    submit.IsEnabled = true;
                }
                passwordErrorBox.Text = "";
            }
        }

        private void checkPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            int check = checkPasswordBox.Password.CompareTo(passwordBox.Password);

            if (check != 0)
            {
                passwordCheckValid = false;
                submit.IsEnabled = false;
                checkPasswordErrorBox.Text = "Passwords do not match.";
            }
            else
            {
                passwordCheckValid = true;
                if (passwordValid == true && photoValid == true)
                {
                    submit.IsEnabled = true;
                }
                checkPasswordErrorBox.Text = "";
            }
        }

        #endregion

        #region Navigation Handlers

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            passwordValid = false;
            passwordCheckValid = false;
            photoValid = false;
            submit.IsEnabled = false;
        }

        #endregion
    }
}
