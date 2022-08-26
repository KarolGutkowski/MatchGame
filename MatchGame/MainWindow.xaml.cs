using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatchGame
{
    using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TextBlock lastClicked;
        bool findingMatch = false;
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed=0;
        int matched = 0;
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            Test.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matched == 8)
            {
                timer.Stop();
                Test.Text = Test.Text + " - Play again?";
            }
        }

        private void SetUpGame()
        {
            
            List<string> Emojis = new List<string>()
            {
                "🍇", "🍇",
                "🍉", "🍉",
                "🍋", "🍋",
                "🍍", "🍍",
                "🍌", "🍌",
                "🥑", "🥑",
                "🍆", "🍆",
                "🥦", "🥦",
                "🎶", "🎶",
                "🤡", "🤡",
                "💩", "💩",
                "👽", "👽",
                "👻", "👻",
                "🙈", "🙈",
                "🦒", "🦒",
                "🐭", "🐭",
                "🐼", "🐼",
                "🐴", "🐴",
                "🐽", "🐽",
                "🐾", "🐾",
                "🐒", "🐒",
                "👌" ,"👌",
                "🤌", "🤌",
                "🎖" ,"🎖"
            };
            
            Random random = new Random();
            int n = mainGrid.Children.OfType<TextBlock>().Where(x => x.Name != "Test").Count();
            /*
            for(int i=0;i<n;i++)
            {
                TextBlock textBlock = mainGrid.Children.OfType<TextBlock>().Where(x => x.Name != "Test").ElementAt(i);
                textBlock.Visibility = Visibility.Visible;
                textBlock.Text = "T";
            }
            */
            List<string> EmojisCopy = new List<string>(Emojis);
            while (EmojisCopy.Count>n)
            {
                int i = random.Next(EmojisCopy.Count);
                EmojisCopy.RemoveAt(i);
                if (i % 2 == 0)
                {
                    EmojisCopy.RemoveAt(i);
                }
                else
                {
                    EmojisCopy.RemoveAt(i-1);
                } 
            }
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                
                if (textBlock.Name != "Test")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(EmojisCopy.Count);
                    string nextEmoji = EmojisCopy[index];
                    textBlock.Text = nextEmoji;
                    EmojisCopy.RemoveAt(index);
                }
                
                
                //textBlock.Text = Emojis.Count.ToString();
            }
            //mainGrid.Children.OfType<TextBlock>().Where(x => x.Name != "Test").First().Text = "gay";
            tenthsOfSecondsElapsed = 0;
            matched= 0;
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(matched ==0) timer.Start();
            TextBlock textBlock = sender as TextBlock;
            string emoji = textBlock.Text;
            if (!findingMatch)
            {
                lastClicked = textBlock;
                findingMatch = true;
                textBlock.Visibility = Visibility.Hidden;
            }
            else if (textBlock.Text == lastClicked.Text)
            {
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
                matched += 1;
            }
            else
            {
                lastClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }     
        }

        
        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matched == 8)
            {
                SetUpGame();
            }
        }
    }
}
