using System;
using System.ComponentModel;
using System.Windows;

namespace ImageViewer.Common
{
    /// <summary>
    /// 弱いイベントパターンを用いたリスナー登録機能を持つ
    /// ViewModel用基底クラス
    /// </summary>
    public class WeakEventViewModelBase : NotificationObject
    {
        /// <summary>
        /// PropertyChangedEventManager へ弱いイベントのリスナーを登録
        /// </summary>
        /// <param name="notificationObject">WeakPropertyChangedを発火するオブジェクト</param>
        /// <param name="weakEventListener">弱いイベントの発火を待ち受けるオブジェクト</param>
        public void AddListener(INotifyPropertyChanged notificationObject, IWeakEventListener weakEventListener)
        {
            PropertyChangedEventManager.AddListener(
                notificationObject,
                weakEventListener,
                String.Empty);
        }
    }
}
