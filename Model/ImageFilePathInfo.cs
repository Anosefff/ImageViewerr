using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

using ImageViewer.Common;

namespace ImageViewer.Model
{
    public class ImageFilePathInfo
    {
        public String FullPath { get; set; }

        public String Name { get; set; }

        public BitmapImage Icon { get; set; }

        public ObservableCollection<ImageFilePathInfo> Children { get; set; } = new ObservableCollection<ImageFilePathInfo>();

        public ImageFilePathInfo(String fullPath, String iconPath)
        {
            this.FullPath = fullPath;
            this.Name = Path.GetFileName(fullPath);
            this.Icon = new BitmapImage(new Uri(iconPath, UriKind.Relative));

            // ドライブ名は上記で取得できないので再設定
            if (this.Name.Length.Equals(0))
            {
                this.Name = this.FullPath;
            }

            this.AddChild();
        }

        private void AddChild()
        {
            this.Children.Clear();

            // 権限がなくパスが参照できない可能性があるので例外処理
            try
            {
                if (Directory.Exists(this.FullPath))
                {
                    // 下位ディレクトリ一覧を取得
                    foreach (var path in Directory.EnumerateDirectories(this.FullPath, "*", SearchOption.TopDirectoryOnly))
                    {
                        this.Children.Add(new ImageFilePathInfo(path, ImageAttribute.directoryIconPath));
                    }

                    // 画像ファイルのみ抽出
                    // *.png
                    // *.gif
                    // *.jpeg
                    // *.tiff
                    // *.bmp
                    var imageFiles = Directory.EnumerateFiles(this.FullPath)
                        .Where(file => file.ToLower().EndsWith("png", StringComparison.OrdinalIgnoreCase) ||
                               file.ToLower().EndsWith("gif", StringComparison.OrdinalIgnoreCase) ||
                               file.ToLower().EndsWith("jpeg", StringComparison.OrdinalIgnoreCase) ||
                               file.ToLower().EndsWith("tiff", StringComparison.OrdinalIgnoreCase) ||
                               file.ToLower().EndsWith("bmp", StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    // 下位ファイル一覧を取得
                    foreach (var imageFile in imageFiles)
                    {
                        Children.Add(new ImageFilePathInfo(imageFile, ImageAttribute.imageIconPath));
                    }
                }
            }
            catch
            {
            }
        }
    }
}
