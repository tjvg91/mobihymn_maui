﻿using System;
using System.Collections.Generic;

namespace MobiHymnMaui.Models
{
	public class BookmarksGroup : List<ShortHymn>
	{
		public string Name { get; set; }

		public BookmarksGroup(string name, List<ShortHymn> shortHymns) : base(shortHymns)
		{
			Name = name;
		}
    }
}

