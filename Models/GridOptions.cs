using System;

namespace MobiHymnMaui.Models
{
	public class GridOptions
	{
		private string text;
		public string Text
		{
			get => text;
			set => text = value;
		}

		private object index;
		public object Index
		{
			get => index;
			set => index = value;
		}

		private bool isActive;
		public bool IsActive
		{
			get => isActive;
			set => isActive = value;
			}

		public GridOptions()
		{
			Index = -1;
		}
	}
}

