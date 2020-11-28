using System;

namespace ImageViewer.Common
{
    internal static class InteractionRequestEventArgsNullObject
    {
        public static readonly InteractionRequestedEventArgs Empty = new InteractionRequestedEventArgs(new Notification(), (Action) (() => {}));
    }
}
