using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;

namespace ImageViewer.Common
{
    public class MainImageScrollBehavior : Behavior<ScrollViewer>
    {
        public static readonly DependencyProperty ScrollViewerProperty =
            DependencyProperty.Register(
                nameof(CanResizeViewport),
                typeof(Boolean),
                typeof(MainImageScrollBehavior),
                new UIPropertyMetadata(null));

        public Boolean CanResizeViewport
        {
            get { return (Boolean)GetValue(ScrollViewerProperty); }
            set { SetValue(ScrollViewerProperty, value); }
        }

        // 要素にアタッチされたときの処理(イベントハンドラ登録)
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.ScrollChanged += OnScrollChanged;
        }

        // 要素にデタッチされたときの処理(イベントハンドラ登録解除)
        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.ScrollChanged -= OnScrollChanged;
        }

        // スクロールされたときの処理
        private void OnScrollChanged(Object sender, ScrollChangedEventArgs e)
        {
            if (!this.CanResizeViewport)
            {
                return;
            }

            var element = sender as FrameworkElement;

            if (element == null)
            {
                return;
            }

            // 親Gridを取得
            var topGrid = element.Parents<Grid>().FirstOrDefault(p => p.Name == "topGrid");

            if (topGrid == null)
            {
                return;
            }

            // 親Gridが持つ子要素をそれぞれ取得

            var thumbnail = topGrid.Descendants<Image>()
                .FirstOrDefault(d => d.Name == "thumbnail");

            var viewport = topGrid.Descendants<Thumb>()
                .FirstOrDefault(d => d.Name == "viewport");

            if (thumbnail == null || viewport == null)
            {
                return;
            }

            var xFactor = thumbnail.ActualWidth / e.ExtentWidth;
            var yFactor = thumbnail.ActualHeight / e.ExtentHeight;

            var left = e.HorizontalOffset * xFactor;
            var top = e.VerticalOffset * yFactor;

            var width = e.ViewportWidth * xFactor;

            if (width > thumbnail.ActualWidth)
            {
                width = thumbnail.ActualWidth;
            }

            var height = e.ViewportHeight * yFactor;

            if (height > thumbnail.ActualHeight)
            {
                height = thumbnail.ActualHeight;
            }

            // サムネイルの幅・高さを設定

            Canvas.SetLeft(viewport, left);
            Canvas.SetTop(viewport, top);

            viewport.Width = width;
            viewport.Height = height;
        }
    }
}
