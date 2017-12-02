using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System.Display;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SmartHome.Common;
using ZXing;
using ZXing.Mobile;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SmartHome.Device.UI.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TokenPage : Page
    {
        private readonly DisplayRequest _displayRequest = new DisplayRequest();


        private MediaCapture _mediaCapture;
        private bool _isInitialized;
        private bool _isPreviewing;

        public TokenPage()
        {
            this.InitializeComponent();
        }

        private async void ScanButton_Click(object sender, RoutedEventArgs e)
        {
            var scanner = new MobileBarcodeScanner(this.Dispatcher)
            {
                UseCustomOverlay = false,
                TopText = "Hold camera up to QR code",
                BottomText = "Camera will automatically scan QR code",
            };

            var scanningOptions = new MobileBarcodeScanningOptions()
            {
                DelayBetweenAnalyzingFrames = 100,
                PossibleFormats = new List<BarcodeFormat>()
                {
                    BarcodeFormat.QR_CODE
                },
                DelayBetweenContinuousScans = 100
            };

            //scanner.ScanContinuously(scanningOptions, result =>
            //{
            //    var tokenGenerator = new TokenGenerator();

            //    if (tokenGenerator.Validate(result.Text))
            //    {
            //        Debug.WriteLine("Unlocked");
            //    }

            //    Debug.WriteLine("Wrong token");
            //});

            //await InitializeCameraAsync();
            var result = await scanner.Scan(scanningOptions);
            //await CleanupCameraAsync();

        }

        private void BackButton_Clicked(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        #region MediaCapture methods

        private async Task InitializeCameraAsync()
        {
            if (_mediaCapture == null)
            {
                var cameraDevice = (await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture)).FirstOrDefault();

                if (cameraDevice == null)
                {
                    Debug.WriteLine("No camera device found!");
                    return;
                }

                _mediaCapture = new MediaCapture();

                _mediaCapture.Failed += MediaCapture_Failed;

                var settings = new MediaCaptureInitializationSettings { VideoDeviceId = cameraDevice.Id };

                try
                {
                    await _mediaCapture.InitializeAsync(settings);
                    _isInitialized = true;
                }
                catch (UnauthorizedAccessException)
                {
                    Debug.WriteLine("The app was denied access to the camera");
                }

                if (_isInitialized)
                {
                    await StartPreviewAsync();

                    UpdateCaptureControls();
                }
            }
        }

        private async Task StartPreviewAsync()
        {
            _displayRequest.RequestActive();

            PreviewControl.Source = _mediaCapture;

            await _mediaCapture.StartPreviewAsync();
            _isPreviewing = true;
        }

        private async Task StopPreviewAsync()
        {
            _isPreviewing = false;
            await _mediaCapture.StopPreviewAsync();

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                PreviewControl.Source = null;

                _displayRequest.RequestRelease();
            });
        }


        private async Task CleanupCameraAsync()
        {
            Debug.WriteLine("CleanupCameraAsync");

            if (_isInitialized)
            {
                if (_isPreviewing)
                {
                    await StopPreviewAsync();
                }

                _isInitialized = false;
            }

            if (_mediaCapture != null)
            {
                _mediaCapture.Failed -= MediaCapture_Failed;
                _mediaCapture.Dispose();
                _mediaCapture = null;
            }
        }

        private async void MediaCapture_Failed(MediaCapture sender, MediaCaptureFailedEventArgs errorEventArgs)
        {
            Debug.WriteLine("MediaCapture_Failed: (0x{0:X}) {1}", errorEventArgs.Code, errorEventArgs.Message);

            await CleanupCameraAsync();
        }

        private void UpdateCaptureControls()
        {
            ScanButton.IsEnabled = false;
        }

        #endregion MediaCapture methods

    }
}
