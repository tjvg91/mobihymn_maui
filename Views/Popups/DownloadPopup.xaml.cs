﻿using CommunityToolkit.Maui.Views;
using MobiHymnMaui.Utils;
using MobiHymnMaui.ViewModels;
using CommunityToolkit.Maui.Core;

namespace MobiHymnMaui.Views.Popups
{	
	public partial class DownloadPopup : Popup
	{
        private Globals globalInstance = Globals.Instance;
        DownloadViewModel model;

        public Action Todo
        {
            get => model.Todo;
            set => model.Todo = value;
        }

        public DownloadPopup ()
		{
			InitializeComponent ();
            BindingContext = DownloadViewModel.Instance;

            var mainDispalyInfo = DeviceDisplay.MainDisplayInfo;
            var width = Math.Min((mainDispalyInfo.Width / mainDispalyInfo.Density) - (mainDispalyInfo.Width * 0.1), 400);
            //var height = (mainDispalyInfo.Height / mainDispalyInfo.Density) - (mainDispalyInfo.Height * 0.8);
            var height = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 320 : 280;
            Size = new Size(width, height);

            model = BindingContext as DownloadViewModel;

            globalInstance.InitFinished += GlobalInstance_InitFinished;
            this.Opened += DownloadPopup_Opened;
		}

        private void DownloadPopup_Opened(object sender, PopupOpenedEventArgs e)
        {
            if(model.DownloadStatus == DownloadStatus.Success)
            {
                model = new DownloadViewModel();
            }
        }

        private async void GlobalInstance_InitFinished(object sender, EventArgs e)
        {
            await new TaskFactory().StartNew(() => { Thread.Sleep(2000); });
            Close();
        }
    }
}

