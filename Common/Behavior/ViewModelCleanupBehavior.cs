using System;
using System.Windows;
using System.Windows.Interactivity;

namespace ImageViewer.Common
{
    // Windowクローズ時にViewModelのDisposeメソッドを呼び出す添付ビヘイビア
    class ViewModelCleanupBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.Closed += this.WindowClosed;
        }
        
        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.Closed -= this.WindowClosed;
        }
        
        private void WindowClosed(Object sender, EventArgs e)
        {
            (this.AssociatedObject.DataContext as IDisposable)?.Dispose();
        }
    }
}
