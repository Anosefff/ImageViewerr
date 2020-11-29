using System;

namespace ImageViewer.Common
{
    public class ImageAttribute
    {
        public static readonly String sampleImageFilePath = @"..\Resource\Sample.jpg";
        public static readonly String directoryIconPath = @"..\Resource\DirectoryIcon.jpg";
        public static readonly String imageIconPath = @"..\Resource\ImageIcon.jpg";
    }

    public enum ViewportColor
    {
        Red,
        Blue,
        Green,
        Yellow,
        Pink
    }
}
