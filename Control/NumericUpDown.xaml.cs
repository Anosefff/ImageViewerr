using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ImageViewer.Control
{
    /// <summary>
    /// NumericUpDown.xaml の相互作用ロジック
    /// </summary>
    public partial class NumericUpDown : UserControl
    {
        public NumericUpDown()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(int),
                typeof(NumericUpDown),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private void UpButton_Click(Object sender, RoutedEventArgs e)
        {
            this.Value++;
        }

        private void DownButton_Click(Object sender, RoutedEventArgs e)
        {
            this.Value--;
        }

        private void TextBox_MouseWheel(Object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                this.Value++;
            }
            else
            {
                this.Value--;
            }
        }
    }
}
