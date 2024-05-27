using System;
using System.Collections.Generic;
using SkiaSharp.Extended.UI.Controls;

namespace MobiHymnMaui.Elements
{
    public partial class AnimatingView : ContentView
    {
        public AnimatingView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty SizeProperty = BindableProperty.Create
            (nameof(Size), typeof(double), typeof(AnimatingView), 0.0, BindingMode.OneWay, propertyChanged: SizePropertyChanged);
        public static readonly BindableProperty SourceProperty = BindableProperty.Create
            (nameof(Source), typeof(string), typeof(AnimatingView), "", BindingMode.OneWay, propertyChanged: SourcePropertyChanged);
        public static readonly BindableProperty SpeedProperty = BindableProperty.Create
            (nameof(Speed), typeof(float), typeof(AnimatingView), 1f, BindingMode.OneWay, propertyChanged: SpeedPropertyChanged);

        public double Size
        {
            get => (double)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        public string Source
        {
            get => (string)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public float Speed
        {
            get => (float)GetValue(SpeedProperty);
            set => SetValue(SpeedProperty, value);
        }

        private static void SizePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (AnimatingView)bindable;
            var val = (double)newValue;
            if(DeviceInfo.Platform == DevicePlatform.iOS || DeviceInfo.Platform == DevicePlatform.MacCatalyst)
                control.gif.Scale = val;
            else
                control.animation.WidthRequest = control.animation.HeightRequest = val;
        }

        private static void SourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (AnimatingView)bindable;
            var val = (string)newValue;
            if (DeviceInfo.Platform == DevicePlatform.iOS || DeviceInfo.Platform == DevicePlatform.MacCatalyst)
                control.gif.Source = $"resource://MobiHymnMaui.Images.{val}.gif";
            else {
                var file = new SKFileLottieImageSource();
                file.File = $"{val}.json";
                control.animation.Source = file;
            }
        }

        private static void SpeedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            //var control = (AnimatingView)bindable;
            //var val = (float)newValue;
        }
    }
}

