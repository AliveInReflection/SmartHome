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
    public partial class TokenTab : ContentPage
    {
        private TokenGenerator _tokenGenerator;

        public TokenTab()
        {
            InitializeComponent();
            _tokenGenerator = new TokenGenerator();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            try
            {
                var generator = DependencyService.Get<IQRCodeGenerator>();

                var token = _tokenGenerator.Generate("Me", DateTime.UtcNow.AddMinutes(15));

                var qrstream = generator.CreateBarcode(token);

                BarCodeImage.Source = ImageSource.FromStream(() => qrstream);
            }
            catch (Exception exception)
            {
                DisplayAlert("Error", "An error occ", "Ok");
            }

        }
    }
}