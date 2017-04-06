using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Devices.Enumeration;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.Graphics.Display;
using Windows.Graphics.Imaging;
using Windows.Media;
using Windows.Media.Core;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Phone.UI.Input;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.System.Display;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using Windows.Media.FaceAnalysis;
using Windows.UI;
using System.Collections.Generic;
using Windows.UI.Popups;
using Microsoft.ProjectOxford.Face;
using System.Collections.Concurrent;
using Microsoft.ProjectOxford.Face.Contract;
using System.IO;
using Windows.UI.Xaml.Media.Imaging;
using OpenCvSharp;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SafeNote
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Global Variables

        // Get the app's local folder.
        StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
        StorageFile newFile;

        // Create a new subfolder in the current folder.
        // Raise an exception if the folder already exists.
        string fileName = "authentication.jpg";

        StorageFile photo;

        CameraCaptureUI captureUI = new CameraCaptureUI();

        // queue that will contain the API call tasks. 
        BlockingCollection<Task> taskQueue = new BlockingCollection<Task>();

        IRandomAccessStream imageStream;
        FaceServiceClient serviceClient;

        String id;

        bool relocate = false;

        MediaCapture _mediaCapture;
        bool _isPreviewing;

        DisplayRequest _displayRequest;

        #endregion  Global Variables 

        #region Constructor
        public MainPage()
        {
            this.InitializeComponent();

            // Do not cache the state of the UI when suspending/navigating
            NavigationCacheMode = NavigationCacheMode.Disabled;

            // Useful to know when to initialize/clean up the camera
            Application.Current.Suspending += Application_Suspending;
            Application.Current.Resuming += Application_Resuming;
        }
        #endregion Constructor

        #region General Functions

        private void doSuff() { }

        private void CloseApp()
        {
            Application.Current.Exit();
        }

/*========================================================================================================================================================*/
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
/*========================================================================================================================================================*/


        #endregion

        #region Capture Frame
        private async Task GetPreviewFrameAsSoftwareBitmapAsync()
        {
           while(true)
            {
            //await Task.Delay(2000);
            // Get information about the preview

            //var d = new MessageDialog("In Frame.");
            //await d.ShowAsync();

            var previewProperties = _mediaCapture.VideoDeviceController.GetMediaStreamProperties(MediaStreamType.VideoPreview) as VideoEncodingProperties;

                    // Create the video frame to request a SoftwareBitmap preview frame
                    var videoFrame = new VideoFrame(BitmapPixelFormat.Bgra8, (int)previewProperties.Width, (int)previewProperties.Height);

                    // Capture the preview frame
                    using (var currentFrame = await _mediaCapture.GetPreviewFrameAsync(videoFrame))
                    {

                            //d= new MessageDialog("Frame Captured.");
                            //await d.ShowAsync();
                            // Collect the resulting frame
                            SoftwareBitmap bitmap = currentFrame.SoftwareBitmap;

                            newFile = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

                            //var d = new MessageDialog("Checking For Face...");
                            //await d.ShowAsync();

                            SaveSoftwareBitmapToFile(bitmap, newFile);
                           // var d = new MessageDialog("Checking For Face...");
                            //await d.ShowAsync();
                            await Task.Delay(1000);
                    }
                    using (Stream s = File.OpenRead(newFile.Path))
                    {
                        try
                        {
                            Face[] faces = await serviceClient.DetectAsync(s);

                            if (faces.Length > 0)
                            {
                                
                                outputBox.Text = "Face Detected.";

                             Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

                             try
                             {
                                 VerifyResult res = await serviceClient.VerifyAsync(new Guid(faces[0].FaceId.ToString()), (new Guid((string)localSettings.Values["faceID"])));

                                 if (res.IsIdentical == true)
                                 {
                                     var dialog = new MessageDialog("Authentication Successful!.");
                                     await dialog.ShowAsync();
                                 }
                                 else
                                 {
                                     var dialog = new MessageDialog("Intruder Alert!.");
                                     await dialog.ShowAsync();
                                 }
                             }
                             catch
                             {
                                 outputBox.Text = "Error detecting face.";
                             }
                        }
                        else
                            {
                                outputBox.Text = "Error detecting face. Are you visibile in the photo? ";
                            }
                        }
                        catch
                        {
                            outputBox.Text = "Invalid key or quota had been used. Please enter password.";
                        }
                    }
            }
        }
        #endregion

        #region Click Events

        private void Password_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EnterPassword), null);
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(APIKey), null);
        }

        #endregion Click Events

        #region Preview Handlers 
        private async Task StartPreviewAsync()
        {
            try
            {
                if (_mediaCapture == null)
                {
                    // Attempt to get the front camera if one is available, but use any camera device if not
                    var cameraDevice = await FindCameraDeviceByPanelAsync(Windows.Devices.Enumeration.Panel.Front);

                    if (cameraDevice == null)
                    {
                        Debug.WriteLine("No camera device found!");
                        return;
                    }

                    _mediaCapture = new MediaCapture();
                    await _mediaCapture.InitializeAsync();

                     PreviewControl.Source = _mediaCapture;
                     await _mediaCapture.StartPreviewAsync();
                     _isPreviewing = true;

                     _displayRequest.RequestActive();
                     DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;
                }
            }
            catch (UnauthorizedAccessException)
            {
                // This will be thrown if the user denied access to the camera in privacy settings
                System.Diagnostics.Debug.WriteLine("The app was denied access to the camera");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("MediaCapture initialization failed. {0}", ex.Message);
            }
        }

        private async Task CleanupPreviewAsync()
        {
            if (_mediaCapture != null)
            {
                if (_isPreviewing)
                {
                    await _mediaCapture.StopPreviewAsync();
                }

                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    PreviewControl.Source = null;
                    if (_displayRequest != null)
                    {
                        _displayRequest.RequestRelease();
                    }

                    _mediaCapture.Dispose();
                    _mediaCapture = null;
                });
            }
        }

        private static async Task<DeviceInformation> FindCameraDeviceByPanelAsync(Windows.Devices.Enumeration.Panel desiredPanel)
        {
            // Get available devices for capturing pictures
            var allVideoDevices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);

            // Get the desired camera by panel
            DeviceInformation desiredDevice = allVideoDevices.FirstOrDefault(x => x.EnclosureLocation != null && x.EnclosureLocation.Panel == desiredPanel);

            // If there is no device mounted on the desired panel, return the first device found
            return desiredDevice ?? allVideoDevices.FirstOrDefault();
        }
        #endregion Preview Handlers 

        #region UI Handlers

        private void settingsUI()
        {
            outputBox.Visibility = Visibility.Collapsed;
            passwordButton.Visibility = Visibility.Collapsed;
            settingsButton.Visibility = Visibility.Visible;
        }

        private async Task SetupUiAsync()
        {
            // Attempt to lock page to landscape orientation to prevent the CaptureElement from rotating, as this gives a better experience
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;

            // Set the MediaCapture to a variable in App.xaml.cs to handle suspension.
            //(App.Current as App).MediaCapture = _mediaCapture;

            // Hide the status bar
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                await Windows.UI.ViewManagement.StatusBar.GetForCurrentView().HideAsync();
            }
        }
        private async Task CleanupUiAsync()
        {

            // Show the status bar
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                await Windows.UI.ViewManagement.StatusBar.GetForCurrentView().ShowAsync();
            }

            // Revert orientation preferences
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.None;
        }
        #endregion UI Handlers

        #region Navigation Handlers
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {

            await SetupUiAsync();

            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

             if ((string)localSettings.Values["UserDetails"] == null || (string)localSettings.Values["UserDetails"] == "false")
             { 
                 relocate = true;
                 localSettings.Values["UserDetails"] = "false";
                 localSettings.Values["faceID"] = "";
                 localSettings.Values["password"] = "";
                 localSettings.Values["key"] = ""; 
                 //hide buttons for verification
                 //show logo and get started button
                 settingsUI();
            }
            else
            {
                serviceClient = new FaceServiceClient(localSettings.Values["key"].ToString());

                //start preview and user verification
                await StartPreviewAsync();

                await GetPreviewFrameAsSoftwareBitmapAsync();

            } 
        }

        protected async override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (!relocate)
            {
                await CleanupPreviewAsync();

            }
            await CleanupUiAsync();
        }
        #endregion Navigation Handlers

        #region Suspension Handlers
        private async void Application_Suspending(object sender, SuspendingEventArgs e)
        {
            if (!relocate)
            {
                if (Frame.CurrentSourcePageType == typeof(MainPage))
                {
                    var deferral = e.SuspendingOperation.GetDeferral();
                    await CleanupPreviewAsync();
                    await CleanupUiAsync();
                    deferral.Complete();
                }
            }
            else
            {
                await CleanupUiAsync();
            }
        }
        private async void Application_Resuming(object sender, object o)
        {
            if (!relocate)
            {
                if (Frame.CurrentSourcePageType == typeof(MainPage))
                {
                    await SetupUiAsync();
                    await StartPreviewAsync();
                }
            }
            else
            {
                await SetupUiAsync();
            }
        }
        #endregion Suspension Handlers
    }
}
