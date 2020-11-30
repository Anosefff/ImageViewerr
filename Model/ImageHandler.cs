using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Animation;
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
            this.slideshowTimer.Interval = new TimeSpan(0, 0, 1);
            this.slideshowTimer.Tick -= this.SlideShowTimer;
            this.slideshowTimer.Tick += this.SlideShowTimer;
            this.slideshowTimer.Start();
        }

        public void StopSlideshow()
        {
            this.slideshowTimer.Stop();
            this.slideshowCount = 0;
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

        #endregion
    }
}
