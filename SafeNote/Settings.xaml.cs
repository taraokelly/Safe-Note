using Microsoft.ProjectOxford.Face;
using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Microsoft.ProjectOxford.Face.Contract;

namespace SafeNote
{
    public sealed partial class Settings : Page
    {
        #region Global Variables

        bool faceIDChanged = false;
        DateTime faceDate;
        String faceID;

        // Get the app's local folder.
        StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
        StorageFile newFile;

        // Name of file.
        string fileName = "user.jpg";

        bool passwordValid = true, passwordCheckValid = true, photoValid = true;

        CameraCaptureUI captureUI = new CameraCaptureUI();
        StorageFile photo;
        SoftwareBitmap softwareBitmap;
        IRandomAccessStream imageStream;

        FaceServiceClient serviceClient;

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
                    softwareBitmap = await decoder.GetSoftwareBitmapAsync();

                    SoftwareBitmap softwareBitmapBGR8 = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
                    SoftwareBitmapSource bitmapSource = new SoftwareBitmapSource();
                    await bitmapSource.SetBitmapAsync(softwareBitmapBGR8);

                    image.Source = bitmapSource;

                    //DateTime to local var
                    faceDate = DateTime.Now;

                    try
                    {
                        Face[] faces = await serviceClient.DetectAsync(imageStream.AsStream());
 
                        if (faces.Length > 0)
                        {
                            faceID = faces[0].FaceId.ToString();
                            faceIDChanged = true;
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
                        imageErrorBox.Text = "Error detecting face. Invalid key has been entered.";
                    }
                }
            }
            catch
            {
                // disable submit button here
                imageErrorBox.Text = "Error taking photo";
            }

        }

        private async void submit_Click(object sender, RoutedEventArgs e)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            localSettings.Values["password"] = passwordBox.Password;
            localSettings.Values["UserDetails"] = "true";
            if (faceIDChanged)
            {
                localSettings.Values["faceID"] = faceID;
                localSettings.Values["faceExpiryDate"] = faceDate.AddDays(1).ToString();
                newFile = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                SaveSoftwareBitmapToFile(softwareBitmap, newFile);
            }
        }

        #endregion

        #region General Functions

        private async void SaveSoftwareBitmapToFile(SoftwareBitmap softwareBitmap, StorageFile outputFile)
        {
            using (IRandomAccessStream stream = await outputFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                // Create an encoder with the desired format
                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream);

                // Set the software bitmap
                encoder.SetSoftwareBitmap(softwareBitmap);

                // Set additional encoding parameters, if needed
                encoder.BitmapTransform.ScaledWidth = 320;
                encoder.BitmapTransform.ScaledHeight = 240;
                encoder.BitmapTransform.InterpolationMode = BitmapInterpolationMode.Fant;
                encoder.IsThumbnailGenerated = true;

                try
                {
                    await encoder.FlushAsync();
                }
                catch (Exception err)
                {
                    switch (err.HResult)
                    {
                        case unchecked((int)0x88982F81): 
                            encoder.IsThumbnailGenerated = false;
                            break;
                        default:
                            throw err;
                    }
                }

                if (encoder.IsThumbnailGenerated == false)
                {
                    await encoder.FlushAsync();
                }


            }
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

            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            if ((string)localSettings.Values["UserDetails"] != "false")
            {
                passwordBox.Password = localSettings.Values["password"].ToString();

                checkPasswordBox.Password = localSettings.Values["password"].ToString();

                image.Source = new BitmapImage(new Uri(localFolder.Path + "/"+ fileName, UriKind.Absolute));

                serviceClient = new FaceServiceClient(localSettings.Values["key"].ToString());


            }
            else
            {
                passwordValid = false;
                passwordCheckValid = false;
                photoValid = false;
                submit.IsEnabled = false;
            }
        }

        #endregion
    }
}
