using CommunityToolkit.Maui;
using FFImageLoading.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using MobiHymnMaui.Utils;
using PanCardView;

namespace MobiHymnMaui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.UseCardsView()
            .UseMauiCompatibility()
            .UseFFImageLoading()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("ChelseaMarket-Regular.ttf", "ChelseaMarket");
				fonts.AddFont("Cookie-Regular.ttf", "Cookie");
                fonts.AddFont("DancingScript.ttf", "DancingScript");
                fonts.AddFont("faBrands.ttf", "FAB");
                fonts.AddFont("faRegular.ttf", "FAR");
                fonts.AddFont("faSolid.ttf", "FAS");
                fonts.AddFont("Frosty.ttf", "Frosty");
                fonts.AddFont("ionicons.ttf", "ion");
                fonts.AddFont("KGKissMeSlowly.ttf", "KGKissMeSlowly");
                fonts.AddFont("KGMelonheadz.ttf", "KGMelonheadz");
                fonts.AddFont("KGWhattheTeacherWants.ttf", "KGWhattheTeacherWants");
                fonts.AddFont("logo.ttf", "logo");
                fonts.AddFont("NotoSerif-Regular.ttf", "NotoSerif");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("SFPro.ttf", "SFPro");
                fonts.AddFont("StyleScript-Regulard.ttf", "StyleScript");
                fonts.AddFont("UnifrakturMaguntia-Regulard.ttf", "UnifrakturMaguntia");
                fonts.AddFont("VaudDisplay-Ultra.ttf", "VaudDisplay");
            });
#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
