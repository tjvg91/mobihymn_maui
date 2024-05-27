using System;

namespace MobiHymnMaui.Models
{
	public class FontSettings
	{
        public string Name { get; set; }
        public double CharacterSpacing { get; set; }

        public FontSettings()
        {
            CharacterSpacing = 0;
        }
    }
}

