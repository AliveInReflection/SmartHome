using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SmartHome.Mobile.Droid.Platform;
using ZXing;
using ZXing.Rendering;

[assembly: Xamarin.Forms.Dependency(typeof(QRCodeGenerator))]
namespace SmartHome.Mobile.Droid.Platform
{
    public class QRCodeGenerator : IQRCodeGenerator
    {
        public Stream CreateBarcode(string content)
        {
            var barcodeWriter = new ZXing.Mobile.BarcodeWriter
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = 400,
                    Height = 400,
                    Margin = 10
                }
            };

            barcodeWriter.Renderer = new ZXing.Mobile.BitmapRenderer();
            var bitmap = barcodeWriter.Write(content);
            var stream = new MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);  // this is the diff between iOS and Android
            stream.Position = 0;
            return stream;
        }
    }
}