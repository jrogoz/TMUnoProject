using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Uno.Extensions;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TMUnoProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public const string Thumbsup = "👍";
        public const string Disappointed = "😞";
        
        public MainPage()
        {
            this.InitializeComponent();
            elli.Fill = new SolidColorBrush(Windows.UI.Colors.SteelBlue);

        }
        //For future implementation
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            startDetect();
        }
        private async void startRec_Click(object sender, RoutedEventArgs e)
        {
            startRecAsync();
        }
        private async void startDetect()
        {
            // Create an instance of SpeechRecognizer.
            var speechRecognizer = new Windows.Media.SpeechRecognition.SpeechRecognizer();
            string[] responses = { "start","quit" };
            // Add a list constraint to the recognizer.
            var listConstraint = new Windows.Media.SpeechRecognition.SpeechRecognitionListConstraint(responses, "startOrStart");
            speechRecognizer.Constraints.Add(listConstraint);

            // Compile the constraint.
            await speechRecognizer.CompileConstraintsAsync();

            // Start recognition.
            //textBlock1.Text = "Say Start";
            //Recognise with UI
            Windows.Media.SpeechRecognition.SpeechRecognitionResult speechRecognitionResult = await speechRecognizer.RecognizeWithUIAsync();
            
            //Recognise without UI
            //Windows.Media.SpeechRecognition.SpeechRecognitionResult speechRecognitionResult = await speechRecognizer.RecognizeAsync();

            if (speechRecognitionResult.Text == "start")
            {
                //textBlock2.Text = "Start detected";
                await Task.Delay(2000);
                startRecAsync();
            }
            if (speechRecognitionResult.Text == "quit")
            {
                CoreApplication.Exit();
            }
        }
        private async void startRecAsync()
        {
            startRec.IsEnabled = false;
            var speechRecognizer = new Windows.Media.SpeechRecognition.SpeechRecognizer();
            //speechRecognizer.Timeouts.EndSilenceTimeout = TimeSpan.FromSeconds(1.2);
            // Compile the constraint.
            await speechRecognizer.CompileConstraintsAsync();

            Random rnd = new Random();
            int colors_number = rnd.Next(1, 7);

            string correct_colors = "";
            for (int i = 0; i < colors_number; i++)
            {
                int temp_rnd = rnd.Next(1, 5);
                if (temp_rnd == 1)
                {
                    correct_colors = correct_colors + "green ";
                    green.Fill= new SolidColorBrush(Windows.UI.Colors.Green);
                    await Task.Delay(1000);
                    green.Fill = new SolidColorBrush(Windows.UI.Colors.DarkGreen);
                    await Task.Delay(200);
                }
                if (temp_rnd == 2)
                {
                    correct_colors = correct_colors + "red ";
                    red.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
                    await Task.Delay(1000);
                    red.Fill = new SolidColorBrush(Windows.UI.Colors.DarkRed);
                    await Task.Delay(200);
                }
                if (temp_rnd == 3)
                {
                    correct_colors = correct_colors + "blue ";
                    blue.Fill = new SolidColorBrush(Windows.UI.Colors.Blue);
                    await Task.Delay(1000);
                    blue.Fill = new SolidColorBrush(Windows.UI.Colors.DarkBlue);
                    await Task.Delay(200);
                }
                if (temp_rnd == 4)
                {
                    correct_colors = correct_colors + "yellow ";
                    yellow.Fill = new SolidColorBrush(Windows.UI.Colors.Yellow);
                    await Task.Delay(1000);
                    yellow.Fill = new SolidColorBrush(Windows.UI.Colors.DarkOrange);
                    await Task.Delay(200);
                }
            }
            string new_correct_colors=correct_colors.TrimEnd(" ");

            //indicates that speech recognition is on
            elli.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
            // Start recognition.
            Windows.Media.SpeechRecognition.SpeechRecognitionResult speechRecognitionResult = await speechRecognizer.RecognizeAsync();

            //Recognition with UI
            //Windows.Media.SpeechRecognition.SpeechRecognitionResult speechRecognitionResult = await speechRecognizer.RecognizeWithUIAsync();
            
            // Do something with the recognition result.
            
            textBlock1.Text = speechRecognitionResult.Text;
            elli.Fill = new SolidColorBrush(Windows.UI.Colors.SteelBlue);
            
            if (new_correct_colors== speechRecognitionResult.Text)
            {                
                textBlock2.Text ="Good work "+ Thumbsup;
            }
            else
            {
                textBlock2.Text = "Wrong! Try again " + Disappointed;
            }
            await Task.Delay(3000);
            startRec.IsEnabled = true;
            startDetect();
        }
    }
}
