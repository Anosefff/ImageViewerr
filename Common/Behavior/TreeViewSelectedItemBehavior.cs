using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace ImageViewer.Common
{
    public class TreeViewSelectedItemBehavior : Behavior<TreeView>
    {
        public static readonly DependencyProperty TreeViewSelectedItemProperty =
            DependencyProperty.Register(
                nameof(SelectedItem),
                typeof(Object),
                typeof(TreeViewSelectedItemBehavior),
                new UIPropertyMetadata(null, OnSelectedItemChanged));

        public Object SelectedItem
        {
            get { return (Object)GetValue(TreeViewSelectedItemProperty); }
            set { SetValue(TreeViewSelectedItemProperty, value); }
        }

        private static void OnSelectedItemChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var item = e.NewValue as TreeViewItem;

            if (item == null)
            {
                return;
            }

            item.SetValue(TreeViewItem.IsSelectedProperty, true);
        }

        // 要素にアタッチされたときの処理(イベントハンドラ登録)
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.SelectedItemChanged += OnTreeViewSelectedItemChanged;
        }

        // 要素にデタッチされたときの処理(イベントハンドラ登録解除)
        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.SelectedItemChanged -= OnTreeViewSelectedItemChanged;
        }

        private void OnTreeViewSelectedItemChanged(Object sender, RoutedPropertyChangedEventArgs<Object> e)
        {
            this.SelectedItem = e.NewValue;
        }
    }
}
