using System;
using System.Threading;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Threading;

namespace ImageViewer.Common
{
    public abstract class DispatcherTriggerAction : TriggerAction<DependencyObject>
    {
        private Dispatcher dispatcher;

        protected override void OnAttached()
        {
            base.OnAttached();
            this.dispatcher = this.AssociatedObject.Dispatcher;
        }

        protected override void OnDetaching()
        {
            this.dispatcher = (Dispatcher)null;
            base.OnDetaching();
        }

        protected override sealed void Invoke(Object parameter)
        {
            InteractionRequestedEventArgs e = parameter as InteractionRequestedEventArgs ?? InteractionRequestEventArgsNullObject.Empty;

            if (this.dispatcher == null || this.dispatcher.Thread == Thread.CurrentThread)
            {
                this.InvokeAction(e);
            }
            else
            {
                this.dispatcher.Invoke((Delegate)new Action<InteractionRequestedEventArgs>(this.InvokeAction), (Object)e);
            }
        }

        protected abstract void InvokeAction(InteractionRequestedEventArgs e);
    }
}
