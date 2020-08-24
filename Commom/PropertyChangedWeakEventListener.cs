using System;
using System.ComponentModel;
using System.Windows;

namespace ImageViewer.Common
{
    // 弱いイベントパターンのリスナ
    public class PropertyChangedWeakEventListener : IWeakEventListener
    {
        // 弱いイベントパターンを用いたプロパティ変更通知のイベントハンドラ
        public event PropertyChangedEventHandler WeakPropertyChanged;

        /// <summary>
        /// イベント マネージャーからのイベント受信
        /// </summary> 
        /// <param name="managerType"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public Boolean ReceiveWeakEvent(Type managerType, Object sender, EventArgs e)
        {
            // PropertyChangedEventManager からのイベント通知であることを確認
            if (typeof(PropertyChangedEventManager) != managerType)
            {
                return false;
            }

            // PropertyChangedEventArgs であることを確認
            var eventArgs = e as PropertyChangedEventArgs;

            if (eventArgs == null)
            {
                return false;
            }

            // コールバック呼び出し
            var handler = WeakPropertyChanged;

            if (handler != null)
            {
                handler(sender, eventArgs);
            }

            return true;
        }
    }
}
