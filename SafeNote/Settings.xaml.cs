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
        CameraCaptureUI captureUI = new CameraCaptureUI();
        StorageFile photo;
        IRandomAccessStream imageStream;

        const string APIKEY = "fe355e1480a24916aa8a641b6cf29c7b";
        FaceServiceClient serviceClient = new FaceServiceClient(APIKEY);

        public Settings()
        {
            this.InitializeComponent();

            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Windows.Foundation.Size(130, 130);
        }

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
                                // outputBox.Text = "FaceID : " + id;
                                outputBox.Text = "Face Detected.";
                        }
                        else
                        {
                            outputBox.Text = "Error detecting face. Are you visibile in the photo? ";
                        }
                    }
                    catch
                    {
                        outputBox.Text = "Error detecting face. Have you entered the correct key?";
                    }
                }
            }
            catch
            {
                outputBox.Text = "Error taking photo";
            }

        }
    }
}
