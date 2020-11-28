using System;

namespace ImageViewer.Common
{
    public class InteractionRequestedEventArgs : EventArgs
    {
        public InteractionRequestedEventArgs(Notification context, Action callback)
        {
            this.Context = context;
            this.Callback = callback;
        }

        public Notification Context { get; private set; }

        public Action Callback { get; private set; }
    }
}
