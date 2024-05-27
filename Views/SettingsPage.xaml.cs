
using CommunityToolkit.Maui.Views;
using MobiHymnMaui.Utils;

namespace MobiHymnMaui.Views
{
    public partial class SettingsPage : ContentPage
    {
        private Globals globalInstance = Globals.Instance;

        CancellationTokenSource source = new CancellationTokenSource();

        public SettingsPage()
        {
            InitializeComponent();
            if (globalInstance.IsFetchingSyncDetails) RotateBusy();
            globalInstance.IsFetchingSyncDetailsChanged += GlobalInstance_IsFetchingSyncDetailsChanged;
        }

        private void GlobalInstance_IsFetchingSyncDetailsChanged(object sender, EventArgs e)
        {
            if (!(bool)sender)
                source.Cancel();
        }

        void swDarkMode_Toggled(System.Object sender, ToggledEventArgs e)
        {
            globalInstance.DarkMode = e.Value;
        }

        void swKeepAwake_Toggled(System.Object sender, ToggledEventArgs e)
        {
            globalInstance.KeepAwake = e.Value;
        }

        void swOrientationLock_Toggled(System.Object sender, ToggledEventArgs e)
        {
            globalInstance.IsOrientationLocked = e.Value;
        }

        async void swResync_Clicked(System.Object sender, System.EventArgs e)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
                Globals.ShowToastPopup(Application.Current.UserAppTheme == AppTheme.Light ?
                                "no-internet-light" : "no-internet-dark", "Please connect to download resources");
            else
            {
                var sure = await DisplayAlert("Sync?", "This will sync all hymns and take time. Are you sure you want to continue?",
                                "Yes", "No");

                if (sure)
                {
                    Popups.DownloadPopup downloadPopup = new ();
                    downloadPopup.Todo = ForceSyncHymns;
                    //downloadPopup.Dismissed += DownloadPopup_Dismissed;

                    ForceSyncHymns();
                    await Application.Current.MainPage.ShowPopupAsync(downloadPopup);
                }
            }
        }

        async void btnResync_Clicked(System.Object sender, System.EventArgs e)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
                Globals.ShowToastPopup(Application.Current.UserAppTheme == AppTheme.Light ?
                                "no-internet-light" : "no-internet-dark", "Please connect to download resources");
            else
            {
                var sure = await DisplayAlert("Sync?", "Are you sure you want to sync?",
                                "Yes", "No");

                if(sure)
                {
                    Popups.DownloadPopup downloadPopup = new Popups.DownloadPopup();
                    downloadPopup.Todo = SyncHymns;
                    //downloadPopup.Dismissed += DownloadPopup_Dismissed;

                    SyncHymns();
                    await Application.Current.MainPage.ShowPopupAsync(downloadPopup);
                }
            }
        }

        async void ForceSyncHymns()
        {
            if (await globalInstance.DownloadReadHymns(true))
                globalInstance.OnInitFinished("sync");
        }

        async void SyncHymns()
        {
            if (await globalInstance.ResyncHymns())
            {
                globalInstance.OnInitFinished("sync");
            }
        }

        async void RotateBusy()
        {
            while (!source.IsCancellationRequested)
            {
                for (int i = 1; i < 7; i++)
                {
                    if (swResync.Rotation >= 360f) swResync.Rotation = 0;
                    await swResync.RotateTo(i * (360 / 6), 500, Easing.Linear);
                }
            }
        }
    }
}

