using System;
using System.Collections.Generic;
using MobiHymnMaui.Models;
using MobiHymnMaui.ViewModels;

namespace MobiHymnMaui.Elements
{
    public partial class TimelineItem : ContentView
    {
        public Timeline Item
        {
            get => (Timeline)GetValue(ItemProperty);
            set => SetValue(ItemProperty, value);
        }

        public TimelineItem()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty ItemProperty = BindableProperty.Create(
                                                         propertyName: nameof(Item),
                                                         returnType: typeof(Timeline),
                                                         declaringType: typeof(TimelineItem),
                                                         defaultValue: null,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: ItemChanged);


        private static void ItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(newValue != null && newValue.Equals(oldValue))
            {
                var control = (TimelineItem)bindable;
                control.Item = (Timeline)newValue;
            }
        }

    }
}

