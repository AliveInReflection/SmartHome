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
    public partial class MainTab : ContentPage
    {
        private bool _locked = true;
        public MainTab()
        {
            InitializeComponent();
        }

        private void WatchButton_OnClicked(object sender, EventArgs e)
        {
            DisplayAlert("Info", "Not implemented yet", "Ok");
        }

        private async void LockButton_OnClicked(object sender, EventArgs e)
        {
            if (_locked == false)
            {
                _locked = true;
                LockerLabel.Text = "LOCKED";
                LockerLabel.TextColor = Color.DarkBlue;
                LockerButton.Text = "Unlock";
                return;
            }

            var confirmed = await DisplayAlert("Confirmation", "A you sure you want to unlock?", "Yes", "No");

            if (confirmed)
            {
                _locked = false;
                LockerLabel.Text = "UNLOCKED";
                LockerLabel.TextColor = Color.DarkRed;
                LockerButton.Text = "Lock";
            }

        }
    }
}