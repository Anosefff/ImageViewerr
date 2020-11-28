using System;
using System.Windows;
using System.Windows.Forms;

namespace ImageViewer.Common
{
    public class FolderBrowserDialogAction : DispatcherTriggerAction
    {
        private static readonly FolderBrowserDialogConfirmation NullObject = new FolderBrowserDialogConfirmation();

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register(
                nameof(Description),
                typeof(String),
                typeof(FolderBrowserDialogAction),
                new PropertyMetadata(null));

        public String Description
        {
            get { return (String)this.GetValue(DescriptionProperty); }
            set { this.SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty SelectedPathProperty =
            DependencyProperty.Register(
                nameof(SelectedPath),
                typeof(String),
                typeof(FolderBrowserDialogAction),
                new PropertyMetadata(null));

        public String SelectedPath
        {
            get { return (String)this.GetValue(SelectedPathProperty); }
            set { this.SetValue(SelectedPathProperty, value); }
        }

        public static readonly DependencyProperty ShowNewFolderButtonProperty =
            DependencyProperty.Register(
                nameof(ShowNewFolderButton),
                typeof(Boolean?),
                typeof(FolderBrowserDialogAction),
                new PropertyMetadata(null));

        public Boolean? ShowNewFolderButton
        {
            get { return (Boolean?)this.GetValue(ShowNewFolderButtonProperty); }
            set { this.SetValue(ShowNewFolderButtonProperty, value); }
        }

        protected override void InvokeAction(InteractionRequestedEventArgs e)
        {
            var dlgConf = e.Context as FolderBrowserDialogConfirmation ?? NullObject;
            var dlg = new FolderBrowserDialog();

            this.ApplyMessagePropertyeValues(dlgConf, dlg);

            var conf = e.Context as Confirmation;
            var result = dlg.ShowDialog();

            if (conf != null)
            {
                // 同意したときにtrueにする
                conf.Confirmed = result == DialogResult.OK || result == DialogResult.Yes;
            }

            this.ApplyDialogPropertyValues(dlgConf, dlg);

            e.Callback();
        }

        private void ApplyMessagePropertyeValues(FolderBrowserDialogConfirmation dlgConf, FolderBrowserDialog dlg)
        {
            // FolderBrowserDialogプロパティのセット
            dlg.Description = dlgConf.Description ?? this.Description ?? dlg.Description;
            dlg.SelectedPath = dlgConf.SelectedPath ?? this.SelectedPath ?? dlg.SelectedPath;
            dlg.ShowNewFolderButton = dlgConf.ShowNewFolderButton ?? this.ShowNewFolderButton ?? dlg.ShowNewFolderButton;
        }

        private void ApplyDialogPropertyValues(FolderBrowserDialogConfirmation dlgConf, FolderBrowserDialog dlg)
        {
            // FolderBrowserDialogプロパティからFolderBrowserDialogConfirmationプロパティへコピー

            if (dlgConf != NullObject)
            {
                dlgConf.Description = dlg.Description;
            }

            this.Description = dlg.Description;

            if (dlgConf != NullObject)
            {
                dlgConf.SelectedPath = dlg.SelectedPath;
            }

            this.SelectedPath = dlg.SelectedPath;

            if (dlgConf != NullObject)
            {
                dlgConf.ShowNewFolderButton = dlg.ShowNewFolderButton;
            }

            this.ShowNewFolderButton = dlg.ShowNewFolderButton;
        }
    }
}
