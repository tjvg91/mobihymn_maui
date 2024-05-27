using System;

namespace MobiHymnMaui.Models
{
	public interface IAppVersionBuild
	{
        string GetVersionNumber();
        string GetBuildNumber();
    }
}

