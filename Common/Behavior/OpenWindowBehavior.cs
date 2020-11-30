using System;
using System.Windows;
using System.Windows.Input;

namespace ImageViewer.Common
{
    // 別ウィンドウを開く添付ビヘイビア
    public class OpenWindowBehavior
    {
        // ウィンドウのタイトル
        public static readonly DependencyProperty TitleProperty =
          DependencyProperty.RegisterAttached("Title", typeof(String), typeof(OpenWindowBehavior), new PropertyMetadata("Window"));

        public static String GetTitle(DependencyObject obj)
        {
            return (String)obj.GetValue(TitleProperty);
        }

        public static void SetTitle(DependencyObject obj, String value)
        {
            obj.SetValue(TitleProperty, value);
        }

        // モーダルウィンドウを表示
        public static readonly DependencyProperty IsModalProperty =
          DependencyProperty.RegisterAttached("IsModal", typeof(Boolean), typeof(OpenWindowBehavior), new PropertyMetadata(true));

        public static Boolean GetIsModal(DependencyObject obj)
        {
            return (Boolean)obj.GetValue(IsModalProperty);
        }

        public static void SetIsModal(DependencyObject obj, Boolean value)
        {
            obj.SetValue(IsModalProperty, value);
        }

        // 親ウィンドウかを設定
        public static readonly DependencyProperty HasOwnerProperty =
           DependencyProperty.RegisterAttached("HasOwner", typeof(Boolean), typeof(OpenWindowBehavior), new PropertyMetadata(false));

        public static Boolean GetHasOwner(DependencyObject obj)
        {
            return (Boolean)obj.GetValue(HasOwnerProperty);
        }

        public static void SetHasOwner(DependencyObject obj, Boolean value)
        {
            obj.SetValue(HasOwnerProperty, value);
        }

        // ダイアログを閉じる
        public static readonly DependencyProperty CloseCommandProperty =
           DependencyProperty.RegisterAttached("CloseCommand", typeof(ICommand), typeof(OpenWindowBehavior), new PropertyMetadata(null));

        public static ICommand GetCloseCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CloseCommandProperty);
        }

        public static void SetCloseCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CloseCommandProperty, value);
        }

        // ウィンドウテンプレート
        public static readonly DependencyProperty WindowTemplateProperty =
           DependencyProperty.RegisterAttached("WindowTemplate", typeof(DataTemplate), typeof(OpenWindowBehavior), new PropertyMetadata(null));

        public static DataTemplate GetWindowTemplate(DependencyObject obj)
        {
            return (DataTemplate)obj.GetValue(WindowTemplateProperty);
        }

        public static void SetWindowTemplate(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(WindowTemplateProperty, value);
        }

        // ウィンドウに対するViewModelのインスタンス取得
        public static readonly DependencyProperty WindowViewModelProperty =
           DependencyProperty.RegisterAttached("WindowViewModel", typeof(Object), typeof(OpenWindowBehavior), new PropertyMetadata(null, OnWindowViewModelChanged));

        public static object GetWindowViewModel(DependencyObject obj)
        {
            return (object)obj.GetValue(WindowViewModelProperty);
        }

        public static void SetWindowViewModel(DependencyObject obj, Object value)
        {
            obj.SetValue(WindowViewModelProperty, value);
        }

        private static void OnWindowViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as FrameworkElement;

            if (element == null)
            {
                return;
            }

            var template = GetWindowTemplate(d);
            var viewModel = GetWindowViewModel(d);

            // テンプレートがセットされているときのみウィンドウを表示
            if (template != null)
            {
                if (viewModel != null)
                {
                    // ViewModel がセットされたらウィンドウを表示
                    OpenWindow(element);
                }
                else
                {
                    // ViewModel が null になったら、ウィンドウを閉じる
                    CloseDialog(element);
                }
            }
        }

        private static void OpenWindow(FrameworkElement element)
        {
            var title = GetTitle(element);
            var isModal = GetIsModal(element);
            var window = GetWindow(element);
            var command = GetCloseCommand(element);
            var template = GetWindowTemplate(element);
            var viewModel = GetWindowViewModel(element);
            var owner = Window.GetWindow(element);
            var hasOwner = GetHasOwner(element);

            if (window == null)
            {
                window = new Window()
                {
                    Title = title,
                    ContentTemplate = template,
                    Content = viewModel,
                    SizeToContent = SizeToContent.WidthAndHeight,
                    Owner = hasOwner ? owner : null,
                };

                // ウィンドウの終了処理追加
                // イベントハンドラの引数からは添付ビヘイビアのプロパティにアクセスできないので、
                // ラムダ式でキャプチャする
                window.Closed += (s, e) =>
                {
                    if (command != null)
                    {
                        // ダイアログのViewModelを引数にCloseCommand実行
                        if (command.CanExecute(viewModel))
                        {
                            command.Execute(viewModel);
                        }
                    }

                    SetWindow(element, null);
                };

                // ウィンドウの表示処理
                SetWindow(element, window);

                if (isModal)
                {
                    window.ShowDialog();
                }
                else
                {
                    window.Show();
                }
            }
            else
            {
                // すでにウィンドウが表示されているので、アクティブ化で前面に表示
                window.Activate();
            }
        }

        private static void CloseDialog(FrameworkElement element)
        {
            var window = GetWindow(element);

            if (window != null)
            {
                window.Close();
                SetWindow(element, null);
            }
        }

        // 添付ビヘイビアで使用する内部プロパティ
        public static readonly DependencyProperty WindowProperty =
           DependencyProperty.RegisterAttached("Window", typeof(Window), typeof(OpenWindowBehavior), new PropertyMetadata(null));

        public static Window GetWindow(DependencyObject obj)
        {
            return (Window)obj.GetValue(WindowProperty);
        }

        public static void SetWindow(DependencyObject obj, Window value)
        {
            obj.SetValue(WindowProperty, value);
        }
    }
}
