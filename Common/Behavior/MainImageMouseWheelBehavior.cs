using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace ImageViewer.Common
{
    public class MainImageMouseWheelBehavior : Behavior<Image>
    {
        public static readonly DependencyProperty MainImageMouseWheelProperty =
            DependencyProperty.Register(
                nameof(CanScaleImage),
                typeof(Boolean),
                typeof(MainImageMouseWheelBehavior),
                new UIPropertyMetadata(null));

        public Boolean CanScaleImage
        {
            get { return (Boolean)GetValue(MainImageMouseWheelProperty); }
            set { SetValue(MainImageMouseWheelProperty, value); }
        }

        // 要素にアタッチされたときの処理(イベントハンドラ登録)
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.PreviewMouseWheel += OnPreviewMouseWheel;
        }

        // 要素にデタッチされたときの処理(イベントハンドラ登録解除)
        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.PreviewMouseWheel -= OnPreviewMouseWheel;
        }

        private void OnPreviewMouseWheel(Object sender, MouseWheelEventArgs e)
        {
            if (!this.CanScaleImage)
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

            var thumbnail = VisualTree.Descendants<Image>(topGrid)
                .FirstOrDefault(d => d.Name == "thumbnail");

            var mainImage = VisualTree.Descendants<Image>(topGrid)
                .FirstOrDefault(d => d.Name == "mainImage");

            if (thumbnail == null || mainImage == null)
            {
                return;
            }

            var scale = 1.0;

            // ホイール上に回す→拡大 / 下に回す→縮小
            if (e.Delta > 0)
            {
                scale = 1.25;
            }
            else
            {
                scale = 1 / 1.25;
            }

            // 拡大・縮小実施
            var thumbnailMatrix = (thumbnail.RenderTransform as MatrixTransform).Matrix;

            thumbnailMatrix.Scale(scale, scale);
            thumbnail.RenderTransform = new MatrixTransform(thumbnailMatrix);

            var mainImageMatrix = (mainImage.RenderTransform as MatrixTransform).Matrix;

            mainImageMatrix.Scale(scale, scale);
            mainImage.RenderTransform = new MatrixTransform(mainImageMatrix);
        }
    }
}
