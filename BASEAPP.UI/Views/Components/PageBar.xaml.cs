using Prism.Commands;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BASEAPP.UI.Views.Components
{
    /// <summary>
    /// Interaction logic for PageBar.xaml
    /// </summary>
    public partial class PageBar : UserControl
    {
        public static readonly DependencyProperty PageCountProperty =
            DependencyProperty.Register("PageCount", typeof(int), typeof(PageBar),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsRender)
                {
                    DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    PropertyChangedCallback = OnPageCountChanged
                });


        public int PageCount
        {
            get { return (int)GetValue(PageCountProperty); }
            set { SetValue(PageCountProperty, value); }
        }

        public static readonly DependencyProperty PageIndexProperty =
            DependencyProperty.Register("PageIndex", typeof(int), typeof(PageBar),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsRender)
                {
                    DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                });


        public int PageIndex
        {
            get { return (int)GetValue(PageIndexProperty); }
            set { SetValue(PageIndexProperty, value); }
        }


        public static readonly DependencyProperty PageCLickCommandProperty =
            DependencyProperty.Register("PageCLickCommand", typeof(DelegateCommand), typeof(PageBar), new PropertyMetadata(null));

        public DelegateCommand PageCLickCommand
        {
            get { return (DelegateCommand)GetValue(PageCLickCommandProperty); }
            set { SetValue(PageCLickCommandProperty, value); }
        }

        public PageBar()
        {
            InitializeComponent();
        }

        private static void OnPageCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PageBar pageBar && e.NewValue is int newPageCount)
            {
                pageBar.UpdateButtons(newPageCount);
            }
        }

        private void UpdateButtons(int newPageCount)
        {
            ButtonsPanel.Children.Clear();

            Style buttonStyle = (Style)this.FindResource("ClickButton");

            for (int i = 1; i <= newPageCount; i++)
            {
                Button button = new Button();
                button.Content = i;
                button.Margin = new Thickness(5);
                button.Width = 40;
                button.Height = 40;
                button.CommandParameter = i;
                button.Command = PageCLickCommand;
                button.Style = buttonStyle;
                button.Click += Button_Click; 
                ButtonsPanel.Children.Add(button);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                PageIndex = (int)button.Content;
            }
        }

    }
}
