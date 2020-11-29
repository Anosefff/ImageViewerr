using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using ImageViewer.Common;
using ImageViewer.Model;

namespace ImageViewer.ViewModel
{
    class MainWindowViewModel : WeakEventViewModelBase
    {
        // 依存性を低くするためViewModel上でnewしない、Modelはシングルトンで実装
        private readonly ImageHandler imageHandler = ImageHandler.GetInstance();
        private readonly PropertyChangedWeakEventListener listener = new PropertyChangedWeakEventListener();

        public MainWindowViewModel()
        {
            // ViewModel => Model へのプロパティ変更通知監視
            base.PropertyChanged += this.OnThisPropertyChanged;

            // Model => ViewModel への弱いイベントハンドラ・リスナを登録
            this.listener.WeakPropertyChanged += this.OnWeakListenerPropertyChanged;

            base.AddListener(this.imageHandler, this.listener);
        }

        #region Event

        // ViewModel => Model へのプロパティ変更通知同期
        private void OnThisPropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this.ImageDirectoryPath):
                    this.imageHandler.ImageDirectoryPath = this.ImageDirectoryPath;
                    break;
                case nameof(this.CurrentImageFilePath):
                    this.imageHandler.CurrentImageFilePath = this.CurrentImageFilePath;
                    break;
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
                case nameof(this.imageHandler.IsDisplayedImageTreeView):
                    base.RaisePropertyChanged(nameof(this.IsDisplayedImageTreeView));
                    break;
                case nameof(this.imageHandler.ImageFilePaths):
                    base.RaisePropertyChanged(nameof(this.ImageFilePaths));
                    break;
                case nameof(this.imageHandler.Thumbnail):
                    base.RaisePropertyChanged(nameof(this.Thumbnail));
                    break;
                case nameof(this.imageHandler.MainImage):
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

        private RelayCommand imageTreeViewDisplayCommand;
        public RelayCommand ImageTreeViewDisplayCommand
        {
            get
            {
                return this.imageTreeViewDisplayCommand = this.imageTreeViewDisplayCommand ?? new RelayCommand(this.imageHandler.AddImageFilePath);
            }
        }

        private RelayCommand imageDisplayCommand;
        public RelayCommand ImageDisplayCommand
        {
            get
            {
                return this.imageDisplayCommand = this.imageDisplayCommand ?? new RelayCommand(this.imageHandler.DisplayImage);
            }
        }

        #endregion

        #region Property

        private String imageDirectoryPath = "";
        public String ImageDirectoryPath
        {
            get { return this.imageDirectoryPath; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.imageDirectoryPath, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

        private ImageFilePathInfo currentImageFilePath;
        public ImageFilePathInfo CurrentImageFilePath
        {
            get { return this.currentImageFilePath; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.currentImageFilePath, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

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

        public Boolean IsDisplayedImageTreeView
        {
            get { return this.imageHandler.IsDisplayedImageTreeView; }
        }

        public ObservableCollection<ImageFilePathInfo> ImageFilePaths
        {
            get { return this.imageHandler.ImageFilePaths; }
        }

        public BitmapImage Thumbnail
        {
            get { return this.imageHandler.Thumbnail; }
        }

        public BitmapImage MainImage
        {
            get { return this.imageHandler.MainImage; }
        }

        #endregion
    }
}
