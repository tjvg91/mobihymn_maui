using System;
using MobiHymnMaui.Utils;

namespace MobiHymnMaui.Models
{
	public class ResyncDetail
	{
		public CRUD Mode { get; set; }
		public string Number { get; set; }
		public ResyncType Type { get; set; }
		public ResyncDetail()
		{
		}
	}
}

