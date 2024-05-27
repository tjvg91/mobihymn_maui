using System;
using System.ComponentModel;
using CommunityToolkit.Maui.Views;

namespace MobiHymnMaui.Views
{
    public partial class AboutPage : ContentPage
    {
        Popups.IntroPopup popup;
        public AboutPage()
        {
            InitializeComponent();
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            popup = new Popups.IntroPopup();
            await Application.Current.MainPage.ShowPopupAsync(popup);
        }
    }
}
