using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SmartHome.Device.UI.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PasswordPage : Page
    {
        private const string DefaultText = "Flat number";
        private const string ClearCommand = "C";
        private const string CorrectCommand = "<";
        private IEnumerable<string> DigitsCommands = Enumerable.Range(0, 10).Select(d => d.ToString()).ToList();
        private StringBuilder CurrentText = new StringBuilder(DefaultText);

        public PasswordPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var command = button.Content?.ToString();

            if (DigitsCommands.Contains(command))
            {
                if (CurrentText.ToString() == DefaultText)
                {
                    CurrentText.Clear();
                }

                CurrentText.Append(command);
            }

            if (command == ClearCommand)
            {
                CurrentText.Clear();
                CurrentText.Append(DefaultText);
            }

            if (command == CorrectCommand)
            {
                if (CurrentText.ToString() != DefaultText)
                {
                    var text = CurrentText.ToString();
                    CurrentText = new StringBuilder(text.Substring(0, text.Length - 1));

                    if (CurrentText.Length == 0)
                    {
                        CurrentText.Append(DefaultText);
                    }
                }
            }

            UpdateScreen();
        }

        private void UpdateScreen()
        {
            FlatTextBox.Text = CurrentText.ToString();
        }

        private void BackButton_Clicked(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void CallButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentText.Clear();
            CurrentText.Append(DefaultText);
            UpdateScreen();

            Frame.Navigate(typeof(RejectedPage));
        }
    }
}
