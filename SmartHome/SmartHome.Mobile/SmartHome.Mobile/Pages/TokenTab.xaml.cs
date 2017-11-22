using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartHome.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TokenTab : ContentPage
    {
        public TokenTab()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            try
            {
                var generator = DependencyService.Get<IQRCodeGenerator>();

                var qrstream = generator.CreateBarcode("test");

                BarCodeImage.Source = ImageSource.FromStream(() => qrstream);
            }
            catch (Exception exception)
            {
                DisplayAlert("Error", "An error occ", "Ok");
            }

        }
    }
}