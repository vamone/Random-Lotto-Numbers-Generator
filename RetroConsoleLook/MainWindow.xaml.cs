using LottonRandomNumberGeneratorV2;
using System.ComponentModel;
using System.Runtime.ExceptionServices;
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
        readonly LottoEngine _lottoEngine;

        readonly TextBox _readLine;

        public MainWindow()
        {
            InitializeComponent();

            var games = Constants.GetGameConfigs();
            var algorithms = Constants.GetAlgorithms();

            this._lottoEngine = new LottoEngine(games.Values, algorithms.Values);

            this.Background = Brushes.Black;
            this.Foreground = Brushes.White;
            this.FontFamily = new FontFamily("Consolas");
            this.FontSize = 14;

            this.PreviewKeyDown += MainWindow_PreviewKeyDown;

            this.textBlockHeadline.Text = "Lotto winning numbers generator";

            var page1 = new ConsolePage("Games", games.Values.Select(x => x.Name).ToList());

            var page2 = new ConsolePage("Algorithms", algorithms.Values.Select(x => x.Type.ToString()).ToList());

            var numberOfGames = new List<string>();

            for (int i = 0; i < 101; i++)
            {
                numberOfGames.Add(i.ToString());
            }

            var page3 = new ConsolePage("Number of games", numberOfGames);

            var page4 = new ConsolePage("Enter value", new List<string>());

            this.Pages.Add(page1);
            this.Pages.Add(page2);
            this.Pages.Add(page3);
            this.Pages.Add(page4);

            foreach (var page in this.Pages)
            {
                page.PropertyChanged += MenuChange_PropertyChanged;
            }

            this._readLine = new TextBox { Width = 200, Background = Brushes.Black, Foreground = Brushes.White, BorderThickness = new Thickness(0), Focusable = true };
        }

        public List<ConsolePage> Pages { get; set; } = new List<ConsolePage>();

        public int ActivePageIndex { get; set; } = 0;

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Print();
        }

        void MenuChange_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            this.Print();
        }

        void Print()
        {
            this.stackPanelConsoleOutput.Children.Clear();

            var titles = new List<string>();

            int j = 0;
            foreach (var page in this.Pages)
            {
                if (page.IsIndexSelected)
                {
                    titles.Add($"{page.Title}:");
                    titles.Add(page.Items[page.Index]);
                }
                else
                {
                    if (j == this.ActivePageIndex)
                    {
                        titles.Add($"{page.Title}:");
                    }
                }

                j++;
            }

            this.AddTextBlock(string.Join(" > ", titles));

            this.AddTextBlock("---");

            if (this.Pages.Count() > this.ActivePageIndex)
            {
                int i = 0;
                foreach (var item in this.Pages[this.ActivePageIndex].Items)
                {
                    string textWithIndex = $"{i + 1}. {item}";
                    string text = i == this.Pages[this.ActivePageIndex].Index ? $"> {textWithIndex}" : $"  {textWithIndex}";

                    if (item == "Custom")
                    {
                        var sp = new StackPanel {  Orientation = Orientation.Horizontal };

                        sp.Children.Add(new TextBlock { Text = i == this.Pages[this.ActivePageIndex].Index ? $"> {i + 1}. " : $"  {i + 1}. " });

                        //sp.Children.Add(this._readLine);

                        this.stackPanelConsoleOutput.Children.Add(sp);
                    }
                    else
                    {
                        this.AddTextBlock(text);
                    }

                    i++;
                }
            }
        }

        void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                if (this.Pages[this.ActivePageIndex].Index != 0)
                {
                    this.Pages[this.ActivePageIndex].Index--;
                    this.Pages[this.ActivePageIndex].IsIndexSelected = false;
                }
            }

            if (e.Key == Key.Down)
            {
                if (this.Pages[this.ActivePageIndex].Index < (this.Pages[this.ActivePageIndex].Items.Count() - 1))
                {
                    this.Pages[this.ActivePageIndex].Index++;
                    this.Pages[this.ActivePageIndex].IsIndexSelected = false;
                }
            }

            if (e.Key == Key.Left)
            {
                if (this.ActivePageIndex >= 1)
                {
                    this.ActivePageIndex--;
                    this.Pages[this.ActivePageIndex].IsIndexSelected = false;
                }

                this.Print();
            }

            if (e.Key == Key.Right)
            {
                if (this.Pages[this.ActivePageIndex].Title == "Custom")
                {
                    //this._readLine.Focus();
                }

                //this.Print();
            }

            if (e.Key == Key.Enter)
            {
                this.Pages[this.ActivePageIndex].IsIndexSelected = true;
                this.ActivePageIndex++;

                this.Print();
            }
        }

        void AddTextBlock(string text)
        {
            var textBlock1 = new TextBlock { Text = text };
            this.stackPanelConsoleOutput.Children.Add(textBlock1);
        }
    }
}