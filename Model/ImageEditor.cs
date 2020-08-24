using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

using Microsoft.Win32;

using ImageViewer.Common;

namespace ImageViewer.Model
{
    class ImageEditor : NotificationObject
    {
        #region Singleton

        private static ImageEditor instance = null;
        public static ImageEditor GetInstance()
        {
            if (instance == null)
            {
                instance = new ImageEditor();
            }

            return instance;
        }

        public ImageEditor()
        {
            this.MainImage = new BitmapImage(new Uri(@"..\Resource\Sample.jpg", UriKind.Relative));
            this.Thumbnail = this.MainImage;
        }

        #endregion

        #region Method

        public void OpenImage()
        {
            var openFileDialog = new OpenFileDialog();

            openFileDialog.Filter =
                "Image File(*.bmp,*.jpg,*.png,*.tif)|*.bmp;*.jpg;*.png;*.tif|bitmap(*.bmp)|*.bmp|JPEG(*.jpg)|*.jpg|PNG(*.png)|*.png|TIFF(*.tif)|*.tif";

            if (openFileDialog.ShowDialog() == true)
            {
                this.MainImage = new BitmapImage(new Uri(openFileDialog.FileName));
                this.Thumbnail = MainImage;
            }
        }

        #endregion

        #region Property

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

        #endregion
    }
}
