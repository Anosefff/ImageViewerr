using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        private Setting setting = new Setting();

        public MainWindowViewModel()
        {
            // ViewModel => Model へのプロパティ変更通知監視
            base.PropertyChanged += this.OnThisPropertyChanged;

            // Model => ViewModel への弱いイベントハンドラ・リスナを登録
            this.listener.WeakPropertyChanged += this.OnWeakListenerPropertyChanged;

            base.AddListener(this.imageHandler, this.listener);
            this.imageHandler.Setting(ref this.setting);
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
                case nameof(this.IsThumbnail):
                    this.imageHandler.IsThumbnail = this.IsThumbnail;
                    break;
                case nameof(this.IsSlideshow):
                    this.imageHandler.IsSlideshow = this.IsSlideshow;
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
                case nameof(this.imageHandler.FileHeader):
                    base.RaisePropertyChanged(nameof(this.FileHeader));
                    break;
                case nameof(this.imageHandler.OpenHeader):
                    base.RaisePropertyChanged(nameof(this.OpenHeader));
                    break;
                case nameof(this.imageHandler.DirectoryDescription):
                    base.RaisePropertyChanged(nameof(this.DirectoryDescription));
                    break;
                case nameof(this.imageHandler.CloseHeader):
                    base.RaisePropertyChanged(nameof(this.CloseHeader));
                    break;
                case nameof(this.imageHandler.ViewHeader):
                    base.RaisePropertyChanged(nameof(this.ViewHeader));
                    break;
                case nameof(this.imageHandler.ThumbnailHeader):
                    base.RaisePropertyChanged(nameof(this.ThumbnailHeader));
                    break;
                case nameof(this.imageHandler.SlideshowHeader):
                    base.RaisePropertyChanged(nameof(this.SlideshowHeader));
                    break;
                case nameof(this.imageHandler.OptionHeader):
                    base.RaisePropertyChanged(nameof(this.OptionHeader));
                    break;
                case nameof(this.imageHandler.SettingHeader):
                    base.RaisePropertyChanged(nameof(this.SettingHeader));
                    break;
                case nameof(this.imageHandler.SettingTitle):
                    base.RaisePropertyChanged(nameof(this.SettingTitle));
                    break;
                case nameof(this.imageHandler.LanguageCaption):
                    base.RaisePropertyChanged(nameof(this.LanguageCaption));
                    break;
                case nameof(this.imageHandler.CurrentLanguage):
                    base.RaisePropertyChanged(nameof(this.CurrentLanguage));
                    break;
                case nameof(this.imageHandler.ViewportColorCaption):
                    base.RaisePropertyChanged(nameof(this.ViewportColorCaption));
                    break;
                case nameof(this.imageHandler.CurrentViewportColor):
                    base.RaisePropertyChanged(nameof(this.CurrentViewportColor));
                    break;
                case nameof(this.imageHandler.SlideshowIntervalCaption):
                    base.RaisePropertyChanged(nameof(this.SlideshowIntervalCaption));
                    break;
                case nameof(this.imageHandler.CurrentSlideShowInterval):
                    base.RaisePropertyChanged(nameof(this.CurrentSlideShowInterval));
                    break;
                case nameof(this.imageHandler.OKCaption):
                    base.RaisePropertyChanged(nameof(this.OKCaption));
                    break;
                case nameof(this.imageHandler.CancelCaption):
                    base.RaisePropertyChanged(nameof(this.CancelCaption));
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Method

        // オブジェクト破棄処理
        protected override void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                // イベントハンドラ登録解除
                base.PropertyChanged -= OnThisPropertyChanged;
                this.listener.WeakPropertyChanged -= OnWeakListenerPropertyChanged;

                // タイマー停止
                this.imageHandler.StopSlideshow();
            }

            base.Dispose(disposing);
        }

        #endregion

        #region Command

        private RelayCommand imageTreeViewDisplayCommand;
        public RelayCommand ImageTreeViewDisplayCommand
        {
            get
            {
                return this.imageTreeViewDisplayCommand = this.imageTreeViewDisplayCommand ?? new RelayCommand(this.imageHandler.DisplayImageTreeView);
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

        private RelayCommand slideshowCommand;
        public RelayCommand SlideshowCommand
        {
            get
            {
                return this.slideshowCommand = this.slideshowCommand ?? new RelayCommand(this.imageHandler.Slideshow);
            }
        }

        private RelayCommand openDialogCommand;
        public RelayCommand OpenDialogCommand
        {
            get
            {
                return this.openDialogCommand = this.openDialogCommand ?? new RelayCommand(this.OpenDialog);
            }
        }

        private void OpenDialog()
        {
            // 設定変更前にスライドショーを止める
            this.imageHandler.StopSlideshow();
            this.IsSlideshow = false;

            this.SettingViewModel = new SettingViewModel(this.setting);
        }

        private RelayCommand<SettingViewModel> closeDialogCommand;
        public RelayCommand<SettingViewModel> CloseDialogCommand
        {
            get
            {
                return this.closeDialogCommand = this.closeDialogCommand ?? new RelayCommand<SettingViewModel>(this.CloseDialog);
            }
        }

        private void CloseDialog(SettingViewModel parameter)
        {
            // 設定ダイアログから値を読み込み
            if (parameter.IsOK)
            {
                this.setting.Language = parameter.CurrentLanguage;
                this.setting.ViewportColor = parameter.CurrentViewportColor;
                this.setting.SlideShowInterval = parameter.CurrentSlideShowInterval;
                this.imageHandler.Setting(ref this.setting);
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

        public Boolean IsDisplayedImageTreeView
        {
            get { return this.imageHandler.IsDisplayedImageTreeView; }
        }

        public ObservableCollection<ImageFilePathInfo> ImageFilePaths
        {
            get { return this.imageHandler.ImageFilePaths; }
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

        private Boolean isThumbnail = true;
        public Boolean IsThumbnail
        {
            get { return this.isThumbnail; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.isThumbnail, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

        public BitmapImage Thumbnail
        {
            get { return this.imageHandler.Thumbnail; }
        }

        public BitmapImage MainImage
        {
            get { return this.imageHandler.MainImage; }
        }

        private Boolean isSlideshow;
        public Boolean IsSlideshow
        {
            get { return this.isSlideshow; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.isSlideshow, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

        #region Language

        public String FileHeader
        {
            get { return this.imageHandler.FileHeader; }
        }

        public String OpenHeader
        {
            get { return this.imageHandler.OpenHeader; }
        }

        public String DirectoryDescription
        {
            get { return this.imageHandler.DirectoryDescription; }
        }

        public String CloseHeader
        {
            get { return this.imageHandler.CloseHeader; }
        }

        public String ViewHeader
        {
            get { return this.imageHandler.ViewHeader; }
        }

        public String ThumbnailHeader
        {
            get { return this.imageHandler.ThumbnailHeader; }
        }

        public String SlideshowHeader
        {
            get { return this.imageHandler.SlideshowHeader; }
        }

        public String OptionHeader
        {
            get { return this.imageHandler.OptionHeader; }
        }

        public String SettingHeader
        {
            get { return this.imageHandler.SettingHeader; }
        }

        #endregion

        #region Setting

        private SettingViewModel settingViewModel;
        public SettingViewModel SettingViewModel
        {
            get { return this.settingViewModel; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.settingViewModel, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

        public String SettingTitle
        {
            get { return this.imageHandler.SettingTitle; }
        }

        public String LanguageCaption
        {
            get { return this.imageHandler.LanguageCaption; }
        }

        public String CurrentLanguage
        {
            get { return this.imageHandler.CurrentLanguage; }
        }

        public String ViewportColorCaption
        {
            get { return this.imageHandler.ViewportColorCaption; }
        }

        public ViewportColor CurrentViewportColor
        {
            get { return this.imageHandler.CurrentViewportColor; }
        }

        public String SlideshowIntervalCaption
        {
            get { return this.imageHandler.SlideshowIntervalCaption; }
        }

        public Int32 CurrentSlideShowInterval
        {
            get { return this.imageHandler.CurrentSlideShowInterval; }
        }

        public String OKCaption
        {
            get { return this.imageHandler.OKCaption; }
        }

        public String CancelCaption
        {
            get { return this.imageHandler.CancelCaption; }
        }

        #endregion

        #endregion
    }
}
