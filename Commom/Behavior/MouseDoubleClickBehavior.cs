using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ImageViewer.Common
{
    public class MouseDoubleClickBehavior
    {
        public static DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(MouseDoubleClickBehavior),
                new UIPropertyMetadata(OnCommandChanged));

        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        private static void OnCommandChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var control = obj as Control;

            if (control == null)
            {
                return;
            }

            if (e.NewValue != null)
            {
                control.MouseDoubleClick += OnMouseDoubleClick;
            }
            else
            {
                control.MouseDoubleClick -= OnMouseDoubleClick;
            }
        }

        private static void OnMouseDoubleClick(Object sender, RoutedEventArgs e)
        {
            var control = sender as Control;
            var command = (ICommand)control.GetValue(CommandProperty);
            var commandParameter = control.GetValue(CommandParameterProperty);

            command.Execute(commandParameter);
        }

        public static DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached(
                "CommandParameter",
                typeof(Object),
                typeof(MouseDoubleClickBehavior),
                new UIPropertyMetadata(null));

        public static Object GetCommandParameter(DependencyObject obj)
        {
            return obj.GetValue(CommandParameterProperty);
        }

        public static void SetCommandParameter(DependencyObject obj, Object value)
        {
            obj.SetValue(CommandParameterProperty, value);
        }
    }
}
