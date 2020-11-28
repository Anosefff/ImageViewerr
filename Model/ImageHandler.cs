using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

using Microsoft.Win32;

using ImageViewer.Common;

namespace ImageViewer.Model
{
    class ImageHandler : NotificationObject
    {
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
            var imageFilePath = @"..\Resource\Sample.jpg";

            this.DisplayImage(imageFilePath);
        }

        #endregion

        #region Event

        #endregion

        #region Method

        private void DisplayImage(String imageFilePath)
        {
            this.MainImage = new BitmapImage(new Uri(imageFilePath, UriKind.Relative));
            this.Thumbnail = this.MainImage;
        }

        private TreeViewItem CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryHeader = this.CreateDirectoryHeader(directoryInfo);
            var directoryNode = new TreeViewItem { Header = directoryHeader };

            foreach (var directory in directoryInfo.GetDirectories())
            {
                directoryNode.Items.Add(this.CreateDirectoryNode(directory));
            }

            foreach (var fileInfo in directoryInfo.GetFiles())
            {
                var imageHeader = this.CreateImageHeader(fileInfo);

                directoryNode.Items.Add(new TreeViewItem { Header = imageHeader });
            }

            return directoryNode;
        }

        private StackPanel CreateDirectoryHeader(DirectoryInfo directoryInfo)
        {
            var sp = new StackPanel() { Orientation = Orientation.Horizontal };

            sp.Children.Add(new Image()
            {
                Source = new BitmapImage(new Uri(@"..\Resource\DirectoryIcon.jpg", UriKind.Relative)),
                Width = 16,
                Height = 16,
            });

            sp.Children.Add(new TextBlock() { Text = directoryInfo.Name });

            return sp;
        }

        private StackPanel CreateImageHeader(FileInfo fileInfo)
        {
            var sp = new StackPanel() { Orientation = Orientation.Horizontal };

            sp.Children.Add(new Image()
            {
                Source = new BitmapImage(new Uri(@"..\Resource\ImageIcon.jpg", UriKind.Relative)),
                Width = 16,
                Height = 16,
            });

            sp.Children.Add(new TextBlock() { Text = fileInfo.Name });

            return sp;
        }

        public void DisplayImageTreeView()
        {
            this.CurrentImageTreeViewItem = null;
            this.ImageTreeViewItems.Clear();

            var imageDirectoryInfo = new DirectoryInfo(this.ImageDirectoryPath);

            this.ImageTreeViewItems.Add(this.CreateDirectoryNode(imageDirectoryInfo));
        }

        public void Command()
        {
            if (this.CurrentImageTreeViewItem == null)
            {
                return;
            }

            var header = (StackPanel)this.CurrentImageTreeViewItem.Header;
            var name = header.Children();

            Console.WriteLine("aaa");
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

        private ObservableCollection<TreeViewItem> imageTreeViewItems = new ObservableCollection<TreeViewItem>();
        public ObservableCollection<TreeViewItem> ImageTreeViewItems
        {
            get { return this.imageTreeViewItems; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.imageTreeViewItems, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

        private TreeViewItem currentImageTreeViewItem;
        public TreeViewItem CurrentImageTreeViewItem
        {
            get { return this.currentImageTreeViewItem; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.currentImageTreeViewItem, value))
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

        #endregion
    }
}
