
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using CommunityToolkit.Maui.Views;
using MobiHymnMaui.Utils;
using MobiHymnMaui.ViewModels;

namespace MobiHymnMaui.Views
{
    public partial class NumSearchPage : ContentPage
    {
        bool isNewInput = true;

        NumSearchViewModel model;

        private Globals globalInstance = Globals.Instance;

        Popups.DownloadPopup downloadPopup = new ();

        public NumSearchPage()
        {
            InitializeComponent();

            model = ((NumSearchViewModel)BindingContext);
            model.OnHymnInputChanged += Model_OnHymnInputChanged;
            
        }

        private void Popup_Closed(object? sender, CommunityToolkit.Maui.Core.PopupClosedEventArgs e)
        {
            Globals.LogAppCenter("Finished Intro Popup", "Last Type", (string)e.Result);

            globalInstance.DownloadStarted += GlobalInstance_DownloadStarted;
            globalInstance.DownloadError += GlobalInstance_DownloadStarted;

            Preferences.Set(PreferencesVar.IS_NEW, false);

            globalInstance.Init();
        }

        private async void GlobalInstance_DownloadStarted(object sender, EventArgs e)
        {
            await Application.Current.MainPage.ShowPopupAsync(downloadPopup);
        }

        private void Model_OnHymnInputChanged(object sender, EventArgs e)
        {
            btnHymnNum.Text = (string)sender;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            model.Orientation = DeviceDisplay.MainDisplayInfo.Orientation;
        }

        async void Key_Clicked(Object sender, EventArgs e)
        {
            if (sender.Equals(FindByName("btnHymnNum"))) isNewInput = true;
            else
            {
                string key = (string)((Button)sender).BindingContext;

                if (new Regex("[0-9]").IsMatch(key))
                {
                    if (isNewInput)
                    {
                        model.HymnNum = key;
                        isNewInput = false;
                    }
                    else model.HymnNum += key;
                }
                else if (new Regex("[stf]").IsMatch(key))
                {
                    model.HymnNum += key;
                    isNewInput = false;
                }
                else
                {
                    if (key == "b")
                    {
                        model.HymnNum = model.HymnNum.Remove(model.HymnNum.Length - 1);
                        if (model.HymnNum.Length == 0)
                        {
                            model.HymnNum = "1";
                            isNewInput = true;
                        }
                    }
                    else if (key == "e" && globalInstance.HymnList.Any(h => h.Number == model.HymnNum))
                    {
                        globalInstance.ActiveHymn = globalInstance.HymnList[model.HymnNum];
                        isNewInput = true;
                        await Shell.Current.GoToAsync($"//{Routes.READ}");
                    }
                    else
                    {
                        if (DeviceInfo.Platform == DevicePlatform.Android)
                            Globals.ShowToastPopup(
                                "not-found",
                                "Hymn not found",
                                130,
                                new RectangleF(0.3f, 0, 0.8f, 0.9f)
                            );
                        else
                            Globals.ShowToastPopup(
                                "not-found",
                                "Hymn not found",
                                0.7,
                                new RectangleF(0.3f, -0.5f, 0.8f, 0.9f)
                            );
                        isNewInput = true;
                    }
                }
            }

        }

        void Toggle_Clicked(Object sender, EventArgs e)
        {
            var tempVal = ((int)globalInstance.HymnInputType) + 1;
            globalInstance.HymnInputType = (Utils.InputType)(tempVal % 2);
        }

        async void tbSearch_Clicked(Object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{Routes.SEARCH}");
        }

        async void tbSettings_Clicked(System.Object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{Routes.SETTINGS}");
        }

        void Hundreds_Clicked(Object sender, EventArgs e)
        {
            model.HundredSelected.Execute(((Button)sender).CommandParameter);
        }

        void Tens_Clicked(System.Object sender, System.EventArgs e)
        {
            model.TenSelected.Execute(((Button)sender).CommandParameter);
        }

        void Ones_Clicked(System.Object sender, System.EventArgs e)
        {
            model.OneSelected.Execute(((Button)sender).CommandParameter);
        }

        async void root_Appearing(System.Object sender, System.EventArgs e)
        {
            Preferences.Set(PreferencesVar.IS_NEW, true);
            var isNew = Preferences.Get(PreferencesVar.IS_NEW, true);
            downloadPopup.Todo = globalInstance.Init;
            if (isNew)
            {
                Popups.IntroPopup popup = new();
                popup.Closed += Popup_Closed;
                try
                {
                    await Task.Delay(5000);
                    await Shell.Current.ShowPopupAsync(popup);
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.Message);
                }
            }
        }
    }
}

