using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace ImageViewer.Common
{
    public class WindowCloseBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            var closeCommandBinding = new CommandBinding(
                ApplicationCommands.Close,
                this.CloseCommandExecuted,
                this.CloseCommandCanExecute);

            this.AssociatedObject.CommandBindings.Add(closeCommandBinding);
        }

        private void CloseCommandExecuted(Object sender, ExecutedRoutedEventArgs e)
        {
            this.AssociatedObject.Close();
            e.Handled = true;
        }

        private void CloseCommandCanExecute(Object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }
    }
}
