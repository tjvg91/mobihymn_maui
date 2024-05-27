using System;
using CommunityToolkit.Maui.Views;
using MobiHymnMaui.Utils;
using MobiHymnMaui.ViewModels;

namespace MobiHymnMaui.Views.Popups
{
    public partial class SettingsPopup : Popup
    {
        private Globals globalInstance = Globals.Instance;
        ReadViewModel model;

        public SettingsPopup()
        {
            InitializeComponent();

            var mainDispalyInfo = DeviceDisplay.MainDisplayInfo;
            var width = Math.Min(450,(mainDispalyInfo.Width / mainDispalyInfo.Density) - (mainDispalyInfo.Width * 0.1));
            var height = 230;
            Size = new Size(width, height);

            BindingContext = ReadViewModel.Instance;

            model = (ReadViewModel)this.BindingContext;

            globalInstance.ActiveReadThemeChanged += Globals_ActiveReadThemeChanged;
        }

        private void Globals_ActiveReadThemeChanged(object? sender, EventArgs e)
        {
            model.ActiveColor = (Color)sender;
        }

        void rbTheme_CheckedChanged(System.Object sender, CheckedChangedEventArgs e)
        {
            var rbTheme = (RadioButton)sender;
            if(rbTheme.IsChecked)
                globalInstance.ActiveReadTheme = (Color)rbTheme.Value;
        }

        void rbAlignment_CheckedChanged(System.Object sender, CheckedChangedEventArgs e)
        {
            var rbAlignment = (RadioButton)sender;
            if(rbAlignment.IsChecked)
                globalInstance.ActiveAlignment = (TextAlignment)rbAlignment.Value;
        }

        void rbFont_CheckedChanged(System.Object sender, CheckedChangedEventArgs e)
        {
            var rbFont = (RadioButton)sender;
            if(rbFont.IsChecked)
                globalInstance.ActiveFont = (string)rbFont.Value;
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            //this. = Visibility.Hidden;

            Task.Delay(250);
        }
    }
}

