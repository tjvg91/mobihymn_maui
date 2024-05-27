using MobiHymnMaui.Utils;
using MobiHymnMaui.Views;

namespace MobiHymnMaui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(Routes.HOME, typeof(NumSearchPage));
		Routing.RegisterRoute(Routes.READ, typeof(ReadPage));
		Routing.RegisterRoute(Routes.SEARCH, typeof(SearchPage));
		Routing.RegisterRoute(Routes.HISTORY, typeof(HistoryPage));
		Routing.RegisterRoute(Routes.BOOKMARKS_GROUP, typeof(BookmarksGroupPage));
		Routing.RegisterRoute(Routes.BOOKMARKS_LIST.Split('?')[0], typeof(BookmarksItemsPage));
		Routing.RegisterRoute(Routes.SETTINGS, typeof(SettingsPage));
		Routing.RegisterRoute(Routes.ABOUT, typeof(AboutPage));

		CurrentItem = NavHome;
	}
}
