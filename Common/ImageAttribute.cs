using System;
using System.Collections.Generic;

namespace ImageViewer.Common
{
    public class ImageAttribute
    {
        public static readonly String sampleImageFilePath = @"..\Resource\Sample.jpg";
        public static readonly String directoryIconPath = @"..\Resource\DirectoryIcon.jpg";
        public static readonly String imageIconPath = @"..\Resource\ImageIcon.jpg";

        #region Languages

        public static readonly List<String> FileHeaders = new List<String>()
        {
             "File", "ファイル"
        };

        public static readonly List<String> OpenHeaders = new List<String>()
        {
            "Open", "開く"
        };


        public static readonly List<String> DirectoryDescriptions = new List<String>()
        {
            "Select Image File Directory", "画像フォルダ選択"
        };

        public static readonly List<String> CloseHeaders = new List<String>()
        {
            "Close", "終了"
        };

        public static readonly List<String> ViewHeaders = new List<String>()
        {
            "View", "表示"
        };

        public static readonly List<String> ThumbnailHeaders = new List<String>()
        {
            "Thumbnail", "サムネイル"
        };

        public static readonly List<String> SlideshowHeaders = new List<String>()
        {
            "Slideshow", "スライドショー"
        };

        public static readonly List<String> OptionHeaders = new List<String>()
        {
            "Option", "オプション"
        };

        public static readonly List<String> SettingHeaders = new List<String>()
        {
            "Setting", "設定"
        };

        public static readonly List<String> SettingTitles = new List<String>()
        {
            "Setting", "設定"
        };

        public static readonly List<String> LanguageCaptions = new List<String>()
        {
            "Language", "言語"
        };

        public static readonly List<String> ViewportColorCaptions = new List<String>()
        {
            "Viewport Color", "ビューポートの色"
        };

        public static readonly List<String> SlideshowIntervalCaptions = new List<String>()
        {
            "Slideshow Interval", "スライドショーの間隔"
        };

        public static readonly List<String> OKCaptions = new List<String>()
        {
            "OK", "OK"
        };

        public static readonly List<String> CancelCaptions = new List<String>()
        {
            "Cancel", "キャンセル"
        };

        #endregion

        #region Setting

        public static readonly List<String> Languages = new List<String>()
        {
            "English", "日本語",
        };

        public static readonly List<Int32> SlideshowIntervals = new List<Int32>()
        {
            1, 5, 10, 20, 30
        };

        #endregion
    }

    public enum LanguageKind
    {
        English,
        Japanese
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
