using System;
using System.Linq;
using ImageViewer.Common;

namespace ImageViewer.Model
{
    public class Setting
    {
        #region Language

        public String FileHeader { get; set; } = ImageAttribute.FileHeaders.ElementAtOrDefault((Int32)LanguageKind.English);

        public String OpenHeader { get; set; } = ImageAttribute.OpenHeaders.ElementAtOrDefault((Int32)LanguageKind.English);

        public String DirectoryDescription { get; set; } = ImageAttribute.DirectoryDescriptions.ElementAtOrDefault((Int32)LanguageKind.English);

        public String CloseHeader { get; set; } = ImageAttribute.CloseHeaders.ElementAtOrDefault((Int32)LanguageKind.English);

        public String ViewHeader { get; set; } = ImageAttribute.ViewHeaders.ElementAtOrDefault((Int32)LanguageKind.English);

        public String ThumbnailHeader { get; set; } = ImageAttribute.ThumbnailHeaders.ElementAtOrDefault((Int32)LanguageKind.English);

        public String SlideshowHeader { get; set; } = ImageAttribute.SlideshowHeaders.ElementAtOrDefault((Int32)LanguageKind.English);

        public String OptionHeader { get; set; } = ImageAttribute.OptionHeaders.ElementAtOrDefault((Int32)LanguageKind.English);

        public String SettingHeader { get; set; } = ImageAttribute.SettingHeaders.ElementAtOrDefault((Int32)LanguageKind.English);

        #endregion

        #region Setting

        public String SettingTitle { get; set; } = ImageAttribute.SettingTitles.ElementAtOrDefault((Int32)LanguageKind.English);

        public String LanguageCaption { get; set; } = ImageAttribute.LanguageCaptions.ElementAtOrDefault((Int32)LanguageKind.English);

        public String Language { get; set; } = ImageAttribute.Languages.ElementAtOrDefault((Int32)LanguageKind.English);

        public String ViewportColorCaption { get; set; } = ImageAttribute.ViewportColorCaptions.ElementAtOrDefault((Int32)LanguageKind.English);

        public ViewportColor ViewportColor { get; set; } = ViewportColor.Red;

        public String SlideshowIntervalCaption { get; set; } = ImageAttribute.SlideshowIntervalCaptions.ElementAtOrDefault((Int32)LanguageKind.English);

        public Int32 SlideShowInterval { get; set; } = ImageAttribute.SlideshowIntervals.FirstOrDefault();

        public String OKCaption { get; set; } = ImageAttribute.OKCaptions.ElementAtOrDefault((Int32)LanguageKind.English);

        public String CancelCaption { get; set; } = ImageAttribute.CancelCaptions.ElementAtOrDefault((Int32)LanguageKind.English);

        #endregion
    }
}
