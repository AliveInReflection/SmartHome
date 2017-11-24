using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Common;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHome.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GuestTab : ContentPage
    {
        private TokenGenerator _tokenGenerator;

        public GuestTab()
        {
            InitializeComponent();
            _tokenGenerator = new TokenGenerator();
        }

        private void GenerateButton_OnClicked(object sender, EventArgs e)
        {
            var date = ExpirationDate.Date;
            var time = ExpirationTime.Time;

            var datetime = date + time;
            var token = _tokenGenerator.Generate("Me", datetime);

            var generator = DependencyService.Get<IQRCodeGenerator>();
            var qrstream = generator.CreateBarcode(token);
            BarCodeImage.Source = ImageSource.FromStream(() => qrstream);
        }

        private void BarCodeSendButton_OnClicked(object sender, EventArgs e)
        {
            DisplayAlert("Info", "Not implemented yet", "Ok");
        }
    }
}