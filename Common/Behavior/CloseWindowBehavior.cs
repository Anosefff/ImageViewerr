using System;
using System.Windows;

namespace ImageViewer.Common
{
    // 別ウィンドウを閉じる添付ビヘイビア
    public class CloseWindowBehavior
    {
        public static readonly DependencyProperty CloseProperty =
          DependencyProperty.RegisterAttached("Close", typeof(Boolean), typeof(CloseWindowBehavior), new PropertyMetadata(false, OnCloseChanged));

        public static Boolean GetClose(DependencyObject obj)
        {
            return (Boolean)obj.GetValue(CloseProperty);
        }
        public static void SetClose(DependencyObject obj, Boolean value)
        {
            obj.SetValue(CloseProperty, value);
        }

        private static void OnCloseChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = d as Window;

            if (window == null)
            {
                // Window以外のコントロールにこの添付ビヘイビアが付けられていた場合は、
                // コントロールの属しているWindowを取得
                window = Window.GetWindow(d);
            }

            if (GetClose(d))
            {
                window.Close();
            }
        }
    }
}
