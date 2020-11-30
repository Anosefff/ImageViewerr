using System;
using System.Collections.ObjectModel;

using ImageViewer.Common;
using ImageViewer.Model;

namespace ImageViewer.ViewModel
{
    class SettingViewModel : NotificationObject
    {
        public SettingViewModel(Setting setting)
        {
            this.LanguageCaption = setting.LanguageCaption;
            this.ViewportColorCaption = setting.ViewportColorCaption;
            this.SlideshowIntervalCaption = setting.SlideshowIntervalCaption;
            this.OKCaption = setting.OKCaption;
            this.CancelCaption = setting.CancelCaption;

            foreach (var language in ImageAttribute.Languages)
            {
                this.Languages.Add(language);
            }

            foreach (var interval in ImageAttribute.SlideshowIntervals)
            {
                this.SlideshowIntervals.Add(interval);
            }

            this.CurrentLanguage = setting.Language;
            this.CurrentViewportColor = setting.ViewportColor;
            this.CurrentSlideShowInterval = setting.SlideShowInterval;

        }

        #region Command

        private RelayCommand okCommand;
        public RelayCommand OKCommand
        {
            get
            {
                return this.okCommand = this.okCommand ?? new RelayCommand(this.OK);
            }
        }

        private void OK()
        {
            // OKボタンでクローズしたことを確認
            this.IsOK = true;

            // ダイアログを閉じる
            this.IsClosedWindow = true;
        }

        private RelayCommand cancelCommand;
        public RelayCommand CancelCommand
        {
            get
            {
                return this.cancelCommand = this.cancelCommand ?? new RelayCommand(this.Cancel);
            }
        }

        private void Cancel()
        {
            // ダイアログを閉じる
            this.IsClosedWindow = true;
        }

        #endregion

        #region Property

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

        private ObservableCollection<String> languages = new ObservableCollection<String>();
        public ObservableCollection<String> Languages
        {
            get { return this.languages; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.languages, value))
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

        private ObservableCollection<Int32> slideshowIntervals = new ObservableCollection<Int32>();
        public ObservableCollection<Int32> SlideshowIntervals
        {
            get { return this.slideshowIntervals; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.slideshowIntervals, value))
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

        private Boolean isOK;
        public Boolean IsOK
        {
            get { return this.isOK; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.isOK, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

        private Boolean isClosedWindow;
        public Boolean IsClosedWindow
        {
            get { return this.isClosedWindow; }
            set
            {
                if (base.RaisePropertyChangedIfSet(ref this.isClosedWindow, value))
                {
                    base.RaisePropertyChanged();
                }
            }
        }

        #endregion
    }
}
