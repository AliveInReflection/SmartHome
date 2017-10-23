using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Capture;

namespace SmartHome.Device.Streaming
{
    public class StreamingHost
    {
        private const int Port = 8888;
        private MediaCapture _camera;

        public StreamingHost(MediaCapture camera)
        {
            _camera = camera;
        }
    }
}
