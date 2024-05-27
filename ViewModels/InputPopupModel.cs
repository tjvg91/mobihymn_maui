using System;

namespace MobiHymnMaui.ViewModels
{
	public class InputPopupModel : BaseViewModel
	{
        private static InputPopupModel instance = null;
        public static InputPopupModel Instance
        {
            get
            {
                instance ??= new InputPopupModel();
                return instance;
            }
        }

        private string actionString = "OK";
        public string ActionString
        {
            get => actionString;
            set
            {
                actionString = value;
                SetProperty(ref actionString, value, nameof(ActionString));
                OnPropertyChanged();
            }
        }

        private string errorString = "";
        public string ErrorString
        {
            get => errorString;
            set
            {
                errorString = value;
                SetProperty(ref errorString, value, nameof(ErrorString));
                OnPropertyChanged();
            }
        }

        public InputPopupModel ()
		{
			
		}
	}
}


