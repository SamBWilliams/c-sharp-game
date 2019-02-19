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

namespace fighting_game_04_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Fighter player = new Fighter();


        public MainWindow()
        {
            InitializeComponent();

        }



        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            startBtn.Visibility = Visibility.Collapsed;
            nameText.Visibility = Visibility.Collapsed;
            title.Visibility = Visibility.Collapsed;
            blueName.Visibility = Visibility.Visible;
            redName.Visibility = Visibility.Visible;
            attackBtn.Visibility = Visibility.Visible;
            blockBtn.Visibility = Visibility.Visible;
            chargeBtn.Visibility = Visibility.Visible;
            healBtn.Visibility = Visibility.Visible;


            blueName.Content = nameText.Text;




        }


        public void AttackBtn_Click(object sender, RoutedEventArgs e)
        {
            //Fighter f = new Fighter();
            //testLab.Visibility = Visibility.Visible;
            attack();
            


        }

        private void BlockBtn_Click(object sender, RoutedEventArgs e)
        {
            block();
            
        }

        private void ChargeBtn_Click(object sender, RoutedEventArgs e)
        {
            chargeAttack();
        }

        private void HealBtn_Click(object sender, RoutedEventArgs e)
        {
            heal();
        }


        public void attack()
        {
            
            bluePlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\blueAttack.png", UriKind.Absolute));

            if (aiMoveSelector() == 0)
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redAttack.png", UriKind.Absolute));
                redHealth.Value -= 20;
            }
            else if (aiMoveSelector() == 1)
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redBlock.png", UriKind.Absolute));
                

            }
            else if (aiMoveSelector() == 2)
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redCharge.png", UriKind.Absolute));
                redHealth.Value -= 20;
            }
            else
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redIdel.png", UriKind.Absolute));
               // redHealth.Value += 20;
            }
            
        }

        public void block()
        {
            bluePlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\blueBlock.png", UriKind.Absolute));
            if (aiMoveSelector() == 0)
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redAttack.png", UriKind.Absolute));
                
            }
            else if (aiMoveSelector() == 1)
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redBlock.png", UriKind.Absolute));


            }
            else if (aiMoveSelector() == 2)
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redCharge.png", UriKind.Absolute));
                
            }
            else
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redIdel.png", UriKind.Absolute));
                
            }
        }

        public void chargeAttack()
        {
            bluePlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\blueCharge.png", UriKind.Absolute));
            if (aiMoveSelector() == 0)
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redAttack.png", UriKind.Absolute));

            }
            else if (aiMoveSelector() == 1)
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redBlock.png", UriKind.Absolute));


            }
            else if (aiMoveSelector() == 2)
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redCharge.png", UriKind.Absolute));

            }
            else
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redIdel.png", UriKind.Absolute));

            }

        }

        public void heal()
        {
            bluePlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\blueIdel.png", UriKind.Absolute));
            if (aiMoveSelector() == 0)
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redAttack.png", UriKind.Absolute));

            }
            else if (aiMoveSelector() == 1)
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redBlock.png", UriKind.Absolute));


            }
            else if (aiMoveSelector() == 2)
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redCharge.png", UriKind.Absolute));

            }
            else
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redIdel.png", UriKind.Absolute));

            }
        }

        public static int aiMoveSelector()
        {
            Random rnd = new Random();
            int randomMoveVal = rnd.Next(0, 4);
            return randomMoveVal;
        }

    }

    public class Fighter
    {
        //string name;
        //int health;
        int attackPower;
        //int currentMove;

        //public string Name { get; set; }
        //public int Health { get; set; }
        public int AttackPower { get; set; } = 20;
        //public int CurrentMove { get; set; }

        //public void doThis() { }
    }

}

    