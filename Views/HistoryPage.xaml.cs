﻿
using MobiHymnMaui.Utils;


namespace MobiHymnMaui.Views
{
    public partial class HistoryPage : ContentPage
    {
        private Globals globalInstance = Globals.Instance;

        public HistoryPage()
        {
            InitializeComponent();
        }

        async void MyListView_ChildAdded(Object sender, ElementEventArgs e)
        {
            var item = e.Element as StackLayout;
            if(item != null)
            {
                item.Opacity = 0;
                await item.FadeTo(1, globalInstance.Duration);
            }
        }
    }
}

