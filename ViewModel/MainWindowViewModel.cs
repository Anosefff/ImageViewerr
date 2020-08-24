using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using ImageViewer.Common;
using ImageViewer.Model;

namespace ImageViewer.ViewModel
{
    class MainWindowViewModel : WeakEventViewModelBase
    {
        private readonly ImageEditor editor = ImageEditor.GetInstance();
        private readonly PropertyChangedWeakEventListener listener = new PropertyChangedWeakEventListener();

        public MainWindowViewModel()
        {
            // ViewModel => Model へのプロパティ変更通知監視
            base.PropertyChanged += this.OnThisPropertyChanged;

            // Model => ViewModel への弱いイベントハンドラ・リスナを登録
            this.listener.WeakPropertyChanged += this.OnWeakListenerPropertyChanged;

            base.AddListener(this.editor, this.listener);
        }

        #region Event

        // ViewModel => Model へのプロパティ変更通知同期
        private void OnThisPropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                default:
                    break;
            }
        }

        /// <summary>
        /// Model => ViewModel へのプロパティ変更通知同期
        /// Model は ViewModel に比べ長期にわたって保持される
        /// ViewModel 側が破棄されても Model 側の参照が残ることで
        /// メモリリークしないように、弱いイベントパターンでリッスン
        /// </summary>
        private void OnWeakListenerPropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.editor.Thumbnail):
                    base.RaisePropertyChanged(nameof(this.Thumbnail));
                    break;
                case nameof(this.editor.MainImage):
                    base.RaisePropertyChanged(nameof(this.MainImage));
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Method

        #endregion

        #region Command

        private RelayCommand imageOpenCommand;
        public RelayCommand ImageOpenCommand
        {
            get
            {
                return this.imageOpenCommand = this.imageOpenCommand ?? new RelayCommand(this.editor.OpenImage);
            }
        }

        #endregion

        #region Property

        private Boolean isDisplayedViewport = true;
        public Boolean IsDisplayedViewport
        {
            get { return this.isDisplayedViewport; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.isDisplayedViewport, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

        private ViewportColor currentViewportColor = ViewportColor.Red;
        public ViewportColor CurrentViewportColor
        {
            get { return this.currentViewportColor; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.currentViewportColor, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

        public BitmapImage Thumbnail
        {
            get { return this.editor.Thumbnail; }
        }

        public BitmapImage MainImage
        {
            get { return this.editor.MainImage; }
        }

        #endregion
    }
}
