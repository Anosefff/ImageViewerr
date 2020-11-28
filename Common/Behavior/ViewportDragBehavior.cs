using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;

namespace ImageViewer.Common
{
    public class ViewportDragBehavior : Behavior<Thumb>
    {
        public static readonly DependencyProperty ViewportDragProperty =
            DependencyProperty.Register(
                nameof(CanDragViewport),
                typeof(Boolean),
                typeof(ViewportDragBehavior),
                new UIPropertyMetadata(null));

        public Boolean CanDragViewport
        {
            get { return (Boolean)GetValue(ViewportDragProperty); }
            set { SetValue(ViewportDragProperty, value); }
        }

        // 要素にアタッチされたときの処理(イベントハンドラ登録)
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.DragDelta += OnDragDelta;
        }

        // 要素にデタッチされたときの処理(イベントハンドラ登録解除)
        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.DragDelta -= OnDragDelta;
        }

        private void OnDragDelta(Object sender, DragDeltaEventArgs e)
        {
            if (!this.CanDragViewport)
            {
                return;
            }

            var element = sender as FrameworkElement;

            if (element == null)
            {
                return;
            }

            // 親Grid取得
            var topGrid = element.Parents<Grid>().FirstOrDefault(p => p.Name == "topGrid");          

            if (topGrid == null)
            {
                return;
            }

            // 親Gridが持つ子要素をそれぞれ取得

            var scrollViewer = VisualTree.Descendants<ScrollViewer>(topGrid)
                .FirstOrDefault(d => d.Name == "mainImageScrollViewer");

            var thumbnail = VisualTree.Descendants<Image>(topGrid)
                .FirstOrDefault(d => d.Name == "thumbnail");

            if (scrollViewer == null || thumbnail == null)
            {
                return;
            }

            scrollViewer.ScrollToHorizontalOffset(
                scrollViewer.HorizontalOffset + (e.HorizontalChange * scrollViewer.ExtentWidth / thumbnail.ActualWidth));

            scrollViewer.ScrollToVerticalOffset(
                scrollViewer.VerticalOffset + (e.VerticalChange * scrollViewer.ExtentHeight / thumbnail.ActualHeight));
        }
    }
}
