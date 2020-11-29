using System;
using System.Windows;
using System.Windows.Controls;

namespace ImageViewer.Common
{
    public class CustomTreeView : TreeView
    {
        public static DependencyProperty CustomSelectedItemProperty =
            DependencyProperty.RegisterAttached(
                nameof(CustomSelectedItem),
                typeof(Object),
                typeof(CustomTreeView),
                new UIPropertyMetadata(null));

        public Object CustomSelectedItem
        {
            get { return GetValue(CustomSelectedItemProperty); }
            set { SetValue(CustomSelectedItemProperty, value); }
        }

        protected override void OnSelectedItemChanged(RoutedPropertyChangedEventArgs<Object> e)
        {
            base.OnSelectedItemChanged(e);
            SetValue(CustomSelectedItemProperty, SelectedItem);
        }
    }
}
