using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ImageViewer.Common
{
    // INotifyPropertyChanged インターフェースを実装した基底クラス
    public class NotificationObject : INotifyPropertyChanged, IDisposable
    {
        // コンストラクタ
        public NotificationObject()
        {
        }

        // デストラクタ
        ~NotificationObject()
        {
            this.Dispose(false);
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName]String propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// 元の値と異なるなら変更
        /// </summary>
        /// <typeparam name="TResult">プロパティの型</typeparam>
        /// <param name="source">元の値</param>
        /// <param name="value">新しい値</param>
        /// <param name="propertyName">プロパティ名</param>
        /// <returns>値の変更有無</returns>
        protected Boolean RaisePropertyChangedIfSet<TResult>(ref TResult source, TResult value)
        {
            //値が同じならなにもしない
            if (EqualityComparer<TResult>.Default.Equals(source, value))
            {
                return false;
            }

            source = value;

            return true;
        }

        #endregion

        #region IDisposable

        private Boolean isDisposed = false;

        // リソース開放処理
        public void Dispose()
        {
            this.Dispose(true);

            // Disposeが明示的に呼ばれたときはデストラクタでDisposeさせない
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// リソース開放処理(派生クラスオーバーライド用)
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(Boolean disposing)
        {
            if (isDisposed)
            {
                return;
            }

            isDisposed = true;

            if (disposing)
            {
                // マネージドリソースの解放処理
            }

            // アンマネージドリソースの解放処理
        }

        #endregion
    }
}
