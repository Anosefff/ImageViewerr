using Microsoft.Win32;
using System;
using System.Collections.Generic;

namespace ImageViewer.Common
{
    public class FolderBrowserDialogConfirmation : Confirmation
    {
        /// <summary>
        /// 上部に表示する説明テキスト
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// 最初に選択するフォルダ
        /// </summary>
        public String SelectedPath { get; set; }

        /// <summary>
        /// 新しいフォルダを作成できるか
        /// </summary>
        public Boolean? ShowNewFolderButton { get; set; }
    }
}
