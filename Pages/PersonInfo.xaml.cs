﻿using System;
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
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BeatlesApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PersonInfo : Page
    {
        Person mainCharacter;
        public PersonInfo()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            mainCharacter = (Person)e.Parameter; 
            SetInformationForPage();
        }

        private void SetInformationForPage()
        {

            try { TextBlockName.Text = mainCharacter.FirstLastName; }
            catch (Exception ex) { TextBlockName.Text = "Error: " + ex; }

            try { TextBlockFullName.Text = mainCharacter.FullName; }
            catch (Exception ex) { TextBlockName.Text = "Error: " + ex; }

            try { ImageProfileImage.Source = new BitmapImage(new Uri("ms-appx://" + mainCharacter.ProfileImage, UriKind.RelativeOrAbsolute)); }
            catch (Exception) { ImageProfileImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/logo.png", UriKind.RelativeOrAbsolute)); }
        }


    }
}
