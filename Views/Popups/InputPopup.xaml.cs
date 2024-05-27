using System;
using System.ComponentModel;
using CommunityToolkit.Maui.Views;
using MobiHymnMaui.ViewModels;

namespace MobiHymnMaui.Views.Popups
{	
	public partial class InputPopup : Popup, INotifyPropertyChanged
	{
        private InputPopupModel model;

        public string Title
        {
            get => model.Title;
            set => model.Title = value;
        }

        public string ActionString
        {
            get => model.ActionString;
            set => model.ActionString = value;
        }

        private Func<string, string> validation;
        public Func<string, string> Validation
        {
            get => validation;
            set => validation = value;
        }

        public event EventHandler OK;
        public event EventHandler Cancel;

        public InputPopup()
        {
            InitializeComponent();

            BindingContext = InputPopupModel.Instance;

            model = (InputPopupModel)BindingContext;

            var mainDispalyInfo = DeviceDisplay.MainDisplayInfo;
            var width = (mainDispalyInfo.Width / mainDispalyInfo.Density) - (mainDispalyInfo.Width * 0.1);
            var height = 175;
            this.Size = new Size(width, height);

            model.ErrorString = "";
        }

        void btnOK_Clicked(System.Object sender, System.EventArgs e)
        {
            string val = entInput.Text;
            model.ErrorString = Validation?.Invoke(val);
            if (Validation == null || model.ErrorString == "")
            {
                OK?.Invoke(val, EventArgs.Empty);
                Close(val);
            }
        }

        void btnCancel_Clicked(System.Object sender, System.EventArgs e)
        {
            string val = entInput.Text;
            Cancel?.Invoke(val, EventArgs.Empty);
            Close(val);
        }
    }
}

