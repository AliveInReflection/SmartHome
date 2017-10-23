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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SmartHome.Device.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const string DefaultText = "Please enter your password";
        private const string ClearCommand = "C";
        private const string CorrectCommand = "<";
        private IEnumerable<string> DigitsCommands = Enumerable.Range(0, 9).Select(d => d.ToString()).ToList();
        private StringBuilder CurrentText = new StringBuilder(DefaultText);

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
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

        private void Submit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateScreen()
        {
            Screeen.Text = CurrentText.ToString();
        }
    }
}
