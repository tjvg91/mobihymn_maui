using System;
using System.Collections.Generic;
using CommunityToolkit.Maui.Views;

namespace MobiHymnMaui.Views.Popups
{	
	public partial class SyncPopup : Popup
    {	
		public SyncPopup ()
		{
			InitializeComponent ();

            var mainDispalyInfo = DeviceDisplay.MainDisplayInfo;
            var width = Math.Min(450, (mainDispalyInfo.Width / mainDispalyInfo.Density) - (mainDispalyInfo.Width * 0.1));
            var height = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 340 : 300;
            Size = new Size(width, height);
        }

        void btnLater_Clicked(System.Object sender, System.EventArgs e)
        {
            Close(null);
        }

        void btnResync_Clicked(System.Object sender, System.EventArgs e)
        {
            Close("sync");
        }
    }
}

