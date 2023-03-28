﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using static System.Net.Mime.MediaTypeNames;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BeatlesApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MusicianInfo : Page
    {
        string apiKey = "a03f8addf761480613b779db817e4c0e";
        bool isAboutMinimized = false;
        int minimizedAboutHeight = 120;

        Musician mainCharacter;
        public MusicianInfo()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            mainCharacter = (Musician)e.Parameter;
            SetInformationForPage();
        }

        private async void SetInformationForPage()
        {

            try { TextBlockName.Text = mainCharacter.FirstLastName; }
            catch (Exception ex) { TextBlockName.Text = "Error: " + ex; }

            try { TextBlockFullName.Text = mainCharacter.FullName; }
            catch (Exception ex) { TextBlockName.Text = "Error: " + ex; }

            try { ImageProfileImage.Source = new BitmapImage(new Uri("ms-appx://" + mainCharacter.ProfileImage, UriKind.RelativeOrAbsolute)); }
            catch (Exception) { ImageProfileImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/logo.png", UriKind.RelativeOrAbsolute)); }

            try
            {
                // Get Json for Artist
                var httpClient = new HttpClient();
                var requestUri = new Uri($"http://ws.audioscrobbler.com/2.0/?method=artist.getinfo&artist={mainCharacter.FirstLastName}&api_key={apiKey}&format=json");

                var artistInfoJson = await httpClient.GetStringAsync(requestUri);

                dynamic result = JsonConvert.DeserializeObject(artistInfoJson);

                // Set About Text
                try { TextBlockAbout.Text = result.artist.bio.content; }
                catch (Exception ex) { TextBlockAbout.Text = "Exception " + ex; }
                // If About Text is too long, minimize it
                if (TextBlockAbout.ActualHeight > 10)
                {
                    TextBlockAbout.Height = minimizedAboutHeight;
                    ButtonShowMore.Visibility = Visibility.Visible;
                    isAboutMinimized = true;
                }
                else
                {
                    ButtonShowMore.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception)
            {

            }
        }

        private void ButtonShowMore_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if(isAboutMinimized)
            {
                TextBlockAbout.Height = Double.NaN;
            }
            else
            {
                TextBlockAbout.Height = minimizedAboutHeight;
            }
            isAboutMinimized = !isAboutMinimized;
            ButtonShowMore.Content = isAboutMinimized ? "Show More" : "Show Less";
        }
    }
}