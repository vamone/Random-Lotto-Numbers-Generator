using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RetroConsoleLook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Background = Brushes.Black;
            this.Foreground = Brushes.White;
            this.FontFamily = new FontFamily("Consolas");
            this.FontSize = 14;

            this.PreviewKeyDown += MainWindow_PreviewKeyDown;

            this.textBlockHeadline.Text = "Lotto winning numbers generator";

            var page1 = new ConsolePage("Games", new List<string>
            {
                "Euromilions",
                "Setforlive",
                "Lotto",
                "Thunderball"
            });

            var page2 = new ConsolePage("Algorithms", new List<string>
            {
                "Random",
                "Combination",
                "Index"
            });

            this.Pages.Add(page1);
            this.Pages.Add(page2);

            this.ActivePage = this.Pages[0];
            this.ActivePage.PropertyChanged += MenuChange_PropertyChanged;
        }

        public List<ConsolePage> Pages { get; set; } = new List<ConsolePage>();

        public ConsolePage ActivePage { get; set; }


        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.PrintMenu();
        }

        void MenuChange_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            this.PrintMenu();
        }

        void PrintMenu()
        {
            this.stackPanelConsoleOutput.Children.Clear();

            if (this.Pages[0].Title != this.ActivePage.Title)
            {
                this.AddTextBlock($"{this.Pages[0].Title} > {this.ActivePage.Title}:");
            }
            else
            {
                this.AddTextBlock($"{this.ActivePage.Title}:");
            }

            this.AddTextBlock("---");

            int i = 0;
            foreach (var item in this.ActivePage.Items)
            {
                string textWithIndex = $"{i + 1}. {item}";
                string text = i == this.ActivePage.Index ? $"> {textWithIndex}" : $"  {textWithIndex}";
                this.AddTextBlock(text);

                i++;
            }
        }

        void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                if (this.ActivePage.Index != 0)
                {
                    this.ActivePage.Index--;
                }
            }

            if (e.Key == Key.Down)
            {
                if (this.ActivePage.Index < (this.ActivePage.Items.Count() - 1))
                {
                    this.ActivePage.Index++;
                }
            }

            if (e.Key == Key.Left)
            {
                this.ActivePage = this.Pages[0];
                this.PrintMenu();
            }

            if (e.Key == Key.Right)
            {
                this.ActivePage = this.Pages[1];
                this.PrintMenu();
            }

            if (e.Key == Key.Enter)
            {
                this.ActivePage = this.Pages[1];
                this.PrintMenu();
            }
        }

        void AddTextBlock(string text)
        {
            var textBlock1 = new TextBlock { Text = text };
            this.stackPanelConsoleOutput.Children.Add(textBlock1);
        }
    }
}