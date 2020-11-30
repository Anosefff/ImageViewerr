using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

using ImageViewer.Common;

namespace ImageViewer.Model
{
    class ImageHandler : NotificationObject
    {
        private DispatcherTimer slideshowTimer = new DispatcherTimer();
        private Int32 slideshowCount = 0;

        private List<String> animationImages = new List<String>();

        #region Singleton

        private static ImageHandler instance = null;
        public static ImageHandler GetInstance()
        {
            if (instance == null)
            {
                instance = new ImageHandler();
            }

            return instance;
        }

        public ImageHandler()
        {
            this.DisplayImage(ImageAttribute.sampleImageFilePath, true);
        }

        #endregion

        #region Event

        private void SlideShowTimer(Object sender, EventArgs e)
        {
            if (this.animationImages.Any())
            {
                this.DisplayImage(this.animationImages.ElementAtOrDefault(slideshowCount));

                if (this.slideshowCount == this.animationImages.Count - 1)
                {
                    this.slideshowCount = 0;
                }
                else
                {
                    this.slideshowCount++;
                }
            }
        }

        #endregion

        #region Method

        private void DisplayImage(String imageFilePath, Boolean isSample = false)
        {
            if (isSample)
            {
                this.MainImage = new BitmapImage(new Uri(imageFilePath, UriKind.Relative));
            }
            else
            {
                this.MainImage = new BitmapImage(new Uri(imageFilePath, UriKind.Absolute));
            }

            this.Thumbnail = this.MainImage;
        }

        public void DisplayImage()
        {
            if (this.CurrentImageFilePath == null)
            {
                return;
            }

            if (String.IsNullOrEmpty(this.CurrentImageFilePath.FullPath))
            {
                return;
            }

            if (!File.GetAttributes(this.CurrentImageFilePath.FullPath).HasFlag(FileAttributes.Directory))
            {
                // ディレクトリではなくファイルが選択されたら該当する画像を表示
                this.DisplayImage(this.CurrentImageFilePath.FullPath);
            }
        }

        public void DisplayImageTreeView()
        {
            if (String.IsNullOrEmpty(this.ImageDirectoryPath))
            {
                return;
            }

            this.IsDisplayedImageTreeView = true;
            this.CurrentImageFilePath = null;
            this.ImageFilePaths.Clear();

            // 権限がなくパスが参照できない可能性があるので例外処理
            try
            {
                this.ImageFilePaths.Add(new ImageFilePathInfo(this.ImageDirectoryPath, ImageAttribute.directoryIconPath));
            }
            catch
            {
            }
        }

        public void Slideshow()
        {
            if (this.IsSlideshow)
            {
                if (this.CurrentImageFilePath == null)
                {
                    return;
                }

                if (String.IsNullOrEmpty(this.CurrentImageFilePath.CurrentDirectory))
                {
                    return;
                }

                var slideshowDirectory = "";

                // 選択項目によって設定するディレクトリを変更
                if (File.GetAttributes(this.CurrentImageFilePath.FullPath).HasFlag(FileAttributes.Directory))
                {
                    slideshowDirectory = this.CurrentImageFilePath.FullPath;
                }
                else
                {
                    slideshowDirectory = Path.GetDirectoryName(this.CurrentImageFilePath.FullPath);
                }

                // 画像ファイルのみ抽出
                // *.png
                // *.gif
                // *.jpeg
                // *.tiff
                // *.bmp
                var imageFiles = Directory.EnumerateFiles(slideshowDirectory, "*", SearchOption.AllDirectories)
                    .Where(file => file.ToLower().EndsWith("png", StringComparison.OrdinalIgnoreCase) ||
                           file.ToLower().EndsWith("gif", StringComparison.OrdinalIgnoreCase) ||
                           file.ToLower().EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                           file.ToLower().EndsWith("tiff", StringComparison.OrdinalIgnoreCase) ||
                           file.ToLower().EndsWith("bmp", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                this.animationImages.Clear();

                foreach (var imageFile in imageFiles)
                {
                    this.animationImages.Add(imageFile);
                }

                this.StartSlideshow();
            }
            else
            {
                this.StopSlideshow();
            }
        }

        private void StartSlideshow()
        {
            this.slideshowTimer.Interval = new TimeSpan(0, 0, this.CurrentSlideShowInterval);
            this.slideshowTimer.Tick -= this.SlideShowTimer;
            this.slideshowTimer.Tick += this.SlideShowTimer;
            this.slideshowTimer.Start();
        }

        public void StopSlideshow()
        {
            this.slideshowTimer.Stop();
            this.slideshowCount = 0;
        }

        public void Setting(ref Setting setting)
        {
            // Language

            var languageId = 0;

            if (setting.Language == ImageAttribute.Languages.ElementAtOrDefault((Int32)LanguageKind.English))
            {
                languageId = (Int32)LanguageKind.English;
            }
            else if (setting.Language == ImageAttribute.Languages.ElementAtOrDefault((Int32)LanguageKind.Japanese))
            {
                languageId = (Int32)LanguageKind.Japanese;
            }
            else
            {
            }

            this.FileHeader = ImageAttribute.FileHeaders.ElementAtOrDefault(languageId);
            this.OpenHeader = ImageAttribute.OpenHeaders.ElementAtOrDefault(languageId);
            this.DirectoryDescription = ImageAttribute.DirectoryDescriptions.ElementAtOrDefault(languageId);
            this.CloseHeader = ImageAttribute.CloseHeaders.ElementAtOrDefault(languageId);
            this.ViewHeader = ImageAttribute.ViewHeaders.ElementAtOrDefault(languageId);
            this.ThumbnailHeader = ImageAttribute.ThumbnailHeaders.ElementAtOrDefault(languageId);
            this.SlideshowHeader = ImageAttribute.SlideshowHeaders.ElementAtOrDefault(languageId);
            this.OptionHeader = ImageAttribute.OptionHeaders.ElementAtOrDefault(languageId);
            this.SettingHeader = ImageAttribute.SettingHeaders.ElementAtOrDefault(languageId);

            // Setting
            this.SettingTitle = setting.SettingTitle = ImageAttribute.SettingTitles.ElementAtOrDefault(languageId);
            this.LanguageCaption = setting.LanguageCaption = ImageAttribute.LanguageCaptions.ElementAtOrDefault(languageId);
            this.CurrentLanguage = setting.Language;
            this.ViewportColorCaption = setting.ViewportColorCaption = ImageAttribute.ViewportColorCaptions.ElementAtOrDefault(languageId);
            this.CurrentViewportColor = setting.ViewportColor;
            this.SlideshowIntervalCaption = setting.SlideshowIntervalCaption = ImageAttribute.SlideshowIntervalCaptions.ElementAtOrDefault(languageId);
            this.CurrentSlideShowInterval = setting.SlideShowInterval;
            this.OKCaption = setting.OKCaption =  ImageAttribute.OKCaptions.ElementAtOrDefault(languageId);
            this.CancelCaption = setting.CancelCaption = ImageAttribute.CancelCaptions.ElementAtOrDefault(languageId);
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

        private Boolean isDisplayedImageTreeView;
        public Boolean IsDisplayedImageTreeView
        {
            get { return this.isDisplayedImageTreeView; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.isDisplayedImageTreeView, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

        private ObservableCollection<ImageFilePathInfo> imageFilePaths = new ObservableCollection<ImageFilePathInfo>();
        public ObservableCollection<ImageFilePathInfo> ImageFilePaths
        {
            get { return this.imageFilePaths; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.imageFilePaths, value))
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

        private Boolean isThumbnail;
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

        private BitmapImage thumbnail;
        public BitmapImage Thumbnail
        {
            get { return this.thumbnail; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.thumbnail, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

        private BitmapImage mainImage;
        public BitmapImage MainImage
        {
            get { return this.mainImage; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.mainImage, value))
                {
                    base.RaisePropertyChanged();
                }
            }
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

        private String fileHeader;
        public String FileHeader
        {
            get { return this.fileHeader; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.fileHeader, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

        private String openHeader;
        public String OpenHeader
        {
            get { return this.openHeader; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.openHeader, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

        private String directoryDescription;
        public String DirectoryDescription
        {
            get { return this.directoryDescription; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.directoryDescription, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

        private String closeHeader;
        public String CloseHeader
        {
            get { return this.closeHeader; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.closeHeader, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

        private String viewHeader;
        public String ViewHeader
        {
            get { return this.viewHeader; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.viewHeader, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

        private String thumbnailHeader;
        public String ThumbnailHeader
        {
            get { return this.thumbnailHeader; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.thumbnailHeader, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

        private String slideshowHeader;
        public String SlideshowHeader
        {
            get { return this.slideshowHeader; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.slideshowHeader, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

        private String optionHeader;
        public String OptionHeader
        {
            get { return this.optionHeader; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.optionHeader, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

        private String settingHeader;
        public String SettingHeader
        {
            get { return this.settingHeader; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.settingHeader, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #region Setting

        private String settingTitle;
        public String SettingTitle
        {
            get { return this.settingTitle; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.settingTitle, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

        private String languageCaption;
        public String LanguageCaption
        {
            get { return this.languageCaption; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.languageCaption, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

        private String currentLanguage;
        public String CurrentLanguage
        {
            get { return this.currentLanguage; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.currentLanguage, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

        private String viewportColorCaption;
        public String ViewportColorCaption
        {
            get { return this.viewportColorCaption; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.viewportColorCaption, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

        private ViewportColor currentViewportColor;
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

        private String slideshowIntervalCaption;
        public String SlideshowIntervalCaption
        {
            get { return this.slideshowIntervalCaption; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.slideshowIntervalCaption, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

        private Int32 currentSlideShowInterval;
        public Int32 CurrentSlideShowInterval
        {
            get { return this.currentSlideShowInterval; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.currentSlideShowInterval, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

        private String okCaption;
        public String OKCaption
        {
            get { return this.okCaption; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.okCaption, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

        private String cancelCaption;
        public String CancelCaption
        {
            get { return this.cancelCaption; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.cancelCaption, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

        #endregion

        #endregion
    }
}
