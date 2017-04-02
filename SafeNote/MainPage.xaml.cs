using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Media;
using Windows.Media.Capture;
using Windows.Media.Core;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.System.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SafeNote
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // Receive notifications about rotation of the device and UI and apply any necessary rotation to the preview stream and UI controls
           private readonly DisplayInformation _displayInformation = DisplayInformation.GetForCurrentView();
           private readonly SimpleOrientationSensor _orientationSensor = SimpleOrientationSensor.GetDefault();
           private SimpleOrientation _deviceOrientation = SimpleOrientation.NotRotated;
           private DisplayOrientations _displayOrientation = DisplayOrientations.Portrait;

           // Rotation metadata to apply to the preview stream and recorded videos (MF_MT_VIDEO_ROTATION)
           // Reference: http://msdn.microsoft.com/en-us/library/windows/apps/xaml/hh868174.aspx
           private static readonly Guid RotationKey = new Guid("C380465D-2271-428C-9B83-ECEA3B4A85C1");

           // Folder in which the captures will be stored (initialized in SetupUiAsync)
           private StorageFolder _captureFolder = null;

           // Prevent the screen from sleeping while the camera is running
           private readonly DisplayRequest _displayRequest = new DisplayRequest();

           // For listening to media property changes
           private readonly SystemMediaTransportControls _systemMediaControls = SystemMediaTransportControls.GetForCurrentView();

           // MediaCapture and its state variables
           private MediaCapture _mediaCapture;
           private IMediaEncodingProperties _previewProperties;
           private bool _isInitialized;
           private bool _isRecording;

           // Information about the camera device
           private bool _mirroringPreview;
           private bool _externalCamera; 

           private FaceDetectionEffect _faceDetectionEffect; 

        public MainPage()
        {
            this.InitializeComponent();
            // Do not cache the state of the UI when suspending/navigating
            NavigationCacheMode = NavigationCacheMode.Disabled;

            // Useful to know when to initialize/clean up the camera
            //Application.Current.Suspending += Application_Suspending;
           // Application.Current.Resuming += Application_Resuming;
        }

        private void VideoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PhotoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FaceDetectionButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
