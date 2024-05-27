using System;
using System.Linq;
using System.Collections.Generic;
using MobiHymnMaui.Models;
using MobiHymnMaui.Utils;
using MobiHymnMaui.ViewModels;
using MobiHymnMaui.Views.Popups;
using System.Drawing;
using CommunityToolkit.Maui.Views;

namespace MobiHymnMaui.Views
{
    [QueryProperty(nameof(Name), "name")]
	public partial class BookmarksItemsPage : ContentPage
	{
        private Globals globalInstance = Globals.Instance;
        private BookmarksViewModel model;

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                Title = value.Capitalize();
                model.BookmarksPerKey = model.BookmarksList.Where(bk => bk.Key == value).First()
                    .OrderBy(bk => bk.Line).ToObservableRangeCollection();
            }
        }

        public BookmarksItemsPage ()
		{
			InitializeComponent ();
            model = (BookmarksViewModel)BindingContext;
            model.IsBookmarkGroupsShown = false;
            model.OnBookmarksChanged += Model_OnBookmarksChanged;
        }

        private async void Model_OnBookmarksChanged(object sender, EventArgs e)
        {
            var initQuery = model.BookmarksList.Where(bk => bk.Key == Name);
            if (initQuery.Count() == 0)
            {
                await Task.Delay(500);
                await Shell.Current.GoToAsync("..");
            }
            else
                model.BookmarksPerKey = initQuery.First().OrderBy(bk => bk.Line).ToObservableRangeCollection();
        }

        async void SwipeItem_Invoked(System.Object sender, System.EventArgs e)
        {
            try
            {
                var swipeItem = (SwipeItem)sender;
                var shortHymn = (ShortHymn)swipeItem.CommandParameter;

                var answer = await DisplayAlert("Delete?", $"Are you sure you want to delete Hymn #{shortHymn.Number} as bookmark?", "Yes", "No");
                if (answer)
                {
                    globalInstance.RemoveBookmark(shortHymn);
                    Globals.ShowToastPopup(
                        "bookmark-deleted",
                        "Bookmark deleted.",
                        DeviceInfo.Platform == DevicePlatform.Android ? 100 : 0.4,
                        DeviceInfo.Platform == DevicePlatform.Android ? new RectangleF(0.5f, -0.3f, 1, 1) :
                        new RectangleF(0.7f, -0.5f, 1, 0.8f)
                    );
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Globals.TrackError(ex);
            }
        }

        async void MyListView_ChildAdded(System.Object sender, ElementEventArgs e)
        {
            StackLayout item = e.Element as StackLayout;
            if (item != null)
            {
                item.Opacity = 0;
                await item.FadeTo(1, globalInstance.Duration);
            }
        }

        async void SwipeItem_Invoked_1(System.Object sender, System.EventArgs e)
        {
            var swipeItem = (SwipeItem)sender;
            var shortHymn = (ShortHymn)swipeItem.CommandParameter;

            model.GroupKeysExceptCurrent = (from key in model.GroupKeys
                                              where key.Name != shortHymn.BookmarkGroup
                                            select key).ToObservableRangeCollection();

            if (model.GroupKeysExceptCurrent.Count > 0)
            {
                await Task.Delay(500);
                model.IsBookmarkGroupsShown = true;
            }
            else
            {
                initPopup();
            }
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            var groupName = (string)((TappedEventArgs)e).Parameter;
            SetNewGroup(groupName);
        }

        void btnAddNewGroup_Clicked(System.Object sender, System.EventArgs e)
        {
            model.IsBookmarkGroupsShown = false;

            initPopup();
        }

        private void InpPopup_OK(object sender, EventArgs e)
        {
            var groupName = (string)sender;
            SetNewGroup(groupName);
        }

        void btnGrpCancel_Clicked(System.Object sender, System.EventArgs e)
        {
            model.IsBookmarkGroupsShown = false;
        }

        void SwipeView_SwipeStarted(System.Object sender, SwipeStartedEventArgs e)
        {
            model.ActiveBookmark = ((SwipeView)sender).BindingContext as ShortHymn;
        }

        async void SetNewGroup(string groupName)
        {
            globalInstance.BookmarkList.Where(bk => bk.Number == model.ActiveBookmark.Number)
                .First().BookmarkGroup = groupName;

            globalInstance.ForceBookmarkChangedEvent();
            await Task.Delay(500);
            Globals.ShowToastPopup("bookmark-saved", "Bookmard moved.",
                    DeviceInfo.Platform == DevicePlatform.Android ? 120 : 0.5,
                    DeviceInfo.Platform == DevicePlatform.Android ? new RectangleF(0.5f, -0.1f, 2, 2) :
                    new RectangleF(0.8f, -0.8f, 1, 0.9f));
        }

        async void tbHome_Clicked(System.Object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{Routes.READ}");
        }

        async void initPopup()
        {
            var inpPopup = new InputPopup
            {
                Title = "Move To",
                ActionString = "Move",
                Validation = (newKey) =>
                {
                    return model.GroupKeys.Where(key => key.Name.ToLower().Equals(newKey.ToLower())).Count() > 0 ?
                            "Group already exists." : "";
                }
            };
            inpPopup.OK += InpPopup_OK; ;
            await Application.Current.MainPage.ShowPopupAsync(inpPopup);
        }
    }
}

