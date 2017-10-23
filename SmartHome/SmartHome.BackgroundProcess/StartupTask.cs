using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Devices.Enumeration;
using Windows.Graphics.Imaging;
using Windows.Media;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage.Streams;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace SmartHome.BackgroundProcess
{
    public sealed class StartupTask : IBackgroundTask
    {
        private BackgroundTaskDeferral _defferal;

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            _defferal = taskInstance.GetDeferral();

            try
            {
                using (var capture = new MediaCapture())
                {
                    capture.Failed +=CaptureOnFailed;
                    capture.RecordLimitationExceeded += CaptureOnRecordLimitationExceeded;

                    DeviceInformationCollection devices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);

                    var settings = new MediaCaptureInitializationSettings()
                    {
                        VideoDeviceId = devices.First().Id,
                        StreamingCaptureMode = StreamingCaptureMode.Video,
                        PhotoCaptureSource = PhotoCaptureSource.VideoPreview,
                        AudioDeviceId = "",
                    };


                    await capture.InitializeAsync(settings);

                    using (var stream = new InMemoryRandomAccessStream())
                    {
                        //await capture.StartRecordToStreamAsync(MediaEncodingProfile.CreateAvi(VideoEncodingQuality.Vga), stream);
                        await capture.CapturePhotoToStreamAsync(ImageEncodingProperties.CreateJpeg(), stream);
                        var bytes = new byte[stream.Size];

                        await stream.ReadAsync(bytes.AsBuffer(), (uint)stream.Size, InputStreamOptions.None);

                        var tmp = bytes.Where(x => x != 0).ToArray();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            while (true)
            {
                System.Diagnostics.Debug.WriteLine("Tick");
                await Task.Delay(10);
            }
        }

        private void CaptureOnRecordLimitationExceeded(MediaCapture sender)
        {
            var message = sender;
        }

        private void CaptureOnFailed(MediaCapture sender, MediaCaptureFailedEventArgs errorEventArgs)
        {
            var message = errorEventArgs.Message;
        }
    }
}
