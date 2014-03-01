using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.Media;
using Windows.System;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Runtime.Serialization.Json;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RadioRivendell
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        SystemMediaTransportControls systemControls;

        async void App_Suspending(
       Object sender,
       Windows.ApplicationModel.SuspendingEventArgs e)
        {
            // TODO: This is the time to save app data in case the process is terminated
            //msRadioPlayer.Stop();
        }

        public MainPage()
        {
            this.InitializeComponent();
            msRadioPlayer.Source = new Uri("http://88.191.235.145:8003");
          
            // Hook up app to system transport controls.
            systemControls = SystemMediaTransportControls.GetForCurrentView();
            systemControls.ButtonPressed += SystemControls_ButtonPressed;

            // Register to handle the following system transpot control buttons.
            systemControls.IsPlayEnabled = true;
            systemControls.IsPauseEnabled = true;
            Application.Current.Suspending += new SuspendingEventHandler(App_Suspending);
            SettingsPane.GetForCurrentView().CommandsRequested += SettingsCommandsRequested;


        }

        private void SettingsCommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            var privacyStatement = new SettingsCommand("privacy", "Privacy Statement", x => Launcher.LaunchUriAsync(
                    new Uri("https://www.radiorivendell.com/page/terms-of-use/")));

            args.Request.ApplicationCommands.Clear();
            args.Request.ApplicationCommands.Add(privacyStatement);
        }
       
        private void playMusic()
        {

          
            msRadioPlayer.Play();

        }
        

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            playMusic();
        }

        private void msRadioPlayer_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            msRadioPlayer.Pause();
        }


        void msRadioPlayer_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            switch (msRadioPlayer.CurrentState)
            {
                case MediaElementState.Playing:
                    systemControls.PlaybackStatus = MediaPlaybackStatus.Playing;
                    break;
                case MediaElementState.Paused:
                    systemControls.PlaybackStatus = MediaPlaybackStatus.Paused;
                    break;
                case MediaElementState.Stopped:
                    systemControls.PlaybackStatus = MediaPlaybackStatus.Stopped;
                    break;
                case MediaElementState.Closed:
                    systemControls.PlaybackStatus = MediaPlaybackStatus.Closed;
                    break;
                
            }
        }

        void SystemControls_ButtonPressed(SystemMediaTransportControls sender,
            SystemMediaTransportControlsButtonPressedEventArgs args)
        {
            switch (args.Button)
            {
                case SystemMediaTransportControlsButton.Play:
                    PlayMedia();
                    break;
                case SystemMediaTransportControlsButton.Pause:
                    PauseMedia();
                    break;
               
            }
        }

        async void PlayMedia()
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                msRadioPlayer.Play();
            });
        }

        async void PauseMedia()
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                msRadioPlayer.Pause();
            });
        }

      

         void msRadioPlayer_MediaOpened(object sender, RoutedEventArgs e)
         {
             
         }
     

    } 
}
