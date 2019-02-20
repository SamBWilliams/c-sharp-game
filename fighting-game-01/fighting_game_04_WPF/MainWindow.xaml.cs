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
        Enemy enemy = new Enemy();
        Game game = new Game();
        MediaPlayer mediaPlayer = new MediaPlayer();

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

            blueAP.Content = player.AttackPower;
            redAP.Content = enemy.AttackPower;


        }

        public void AttackBtn_Click(object sender, RoutedEventArgs e)
        {

            player.CurrentMove = 0;
            bluePlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\blueAttack.png", UriKind.Absolute));
            if (game.checkAttack(player.CurrentMove, enemy.aiMoveSelector()) == 0)
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redAttack.png", UriKind.Absolute));
                player.Health -= enemy.AttackPower;
                enemy.Health -= player.AttackPower;
                blueHealth.Value -= enemy.AttackPower;
                redHealth.Value -= player.AttackPower;
                redMove.Content = "Attacks";
            }
            else if (game.checkAttack(player.CurrentMove, enemy.aiMoveSelector()) == 1)
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redBlock.png", UriKind.Absolute));
                player.AttackPower -= 5;
                blueAP.Content = player.AttackPower;
                redMove.Content = "Blocks";
            }
            else if (game.checkAttack(player.CurrentMove, enemy.aiMoveSelector()) == 2)
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redCharge.png", UriKind.Absolute));
                redHealth.Value -= player.AttackPower;
                enemy.AttackPower += 10;
                redAP.Content = enemy.AttackPower;
                redMove.Content = "Attack charged";
                //red ap goes up
            }
            else
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redIdel.png", UriKind.Absolute));
                redHealth.Value -= player.AttackPower;
                //enemy.heal();
                redHealth.Value += 20;
                redMove.Content = "Heals";
            }

            winCondition();
            constraints();

            mediaPlayer.Open(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\sounds\steelsword.mp3", UriKind.Absolute));
            mediaPlayer.Play();


        }
    
        private void BlockBtn_Click(object sender, RoutedEventArgs e)
        {
            player.CurrentMove = 1;
            bluePlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\blueBlock.png", UriKind.Absolute));
            if (game.checkBlock(player.CurrentMove, enemy.aiMoveSelector()) == 4)
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redAttack.png", UriKind.Absolute));
                enemy.AttackPower -= 5;
                //red ap goes down
                redAP.Content = enemy.AttackPower;
                redMove.Content = "Attack";
            }
            else if (game.checkBlock(player.CurrentMove, enemy.aiMoveSelector()) == 5)
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redBlock.png", UriKind.Absolute));
                redMove.Content = "Blocks";

            }
            else if (game.checkBlock(player.CurrentMove, enemy.aiMoveSelector()) == 6)
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redCharge.png", UriKind.Absolute));
                enemy.AttackPower += 10;
                redAP.Content = enemy.AttackPower;
                redMove.Content = "Attack charged";
                //red ap goes up
            }
            else
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redIdel.png", UriKind.Absolute));
                //enemy.heal();
                redHealth.Value += 20;
                redMove.Content = "Heals";
            }

            winCondition();
            constraints();
        }

        private void ChargeBtn_Click(object sender, RoutedEventArgs e)
        {
            player.CurrentMove = 2;
            player.AttackPower += 10;
            blueAP.Content = player.AttackPower;
            bluePlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\blueCharge.png", UriKind.Absolute));
            if (game.checkCharge(player.CurrentMove, enemy.aiMoveSelector()) == 8)
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redAttack.png", UriKind.Absolute));
                player.Health -= enemy.AttackPower;
                redMove.Content = "Attacks";
            }
            else if (game.checkCharge(player.CurrentMove, enemy.aiMoveSelector()) == 9)
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redBlock.png", UriKind.Absolute));
                redMove.Content = "Blocks";
            }
            else if (game.checkCharge(player.CurrentMove, enemy.aiMoveSelector()) == 10)
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redCharge.png", UriKind.Absolute));
                enemy.AttackPower += 10;
                redAP.Content = enemy.AttackPower;
                redMove.Content = "Attack charged";
                //red ap goes up
            }
            else
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redIdel.png", UriKind.Absolute));
               // enemy.heal();
                redHealth.Value +=20;
                redMove.Content = "Heals";
            }

            winCondition();
            constraints();
        }

        private void HealBtn_Click(object sender, RoutedEventArgs e)
        {
            player.CurrentMove = 3;
            //player.heal();
            blueHealth.Value += 20;
           
            bluePlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\blueIdel.png", UriKind.Absolute));
            if (game.checkHeal(player.CurrentMove, enemy.aiMoveSelector()) == 12)
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redAttack.png", UriKind.Absolute));
                player.Health -= enemy.AttackPower;
                redMove.Content = "Attacks";
            }
            else if (game.checkHeal(player.CurrentMove, enemy.aiMoveSelector()) == 13)
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redBlock.png", UriKind.Absolute));
                redMove.Content = "Blocks";
            }
            else if (game.checkHeal(player.CurrentMove, enemy.aiMoveSelector()) == 14)
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redCharge.png", UriKind.Absolute));
                enemy.AttackPower += 10;
                redAP.Content = enemy.AttackPower;
                redMove.Content = "Attack charged";
                //red ap goes up
            }
            else
            {
                redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redIdel.png", UriKind.Absolute));
                //enemy.heal();
                redHealth.Value += 20;
                redMove.Content = "Heals";
            }

            winCondition();
            constraints();

        }

        public void collapseElements()
        {
            attackBtn.Visibility = Visibility.Collapsed;
            blockBtn.Visibility = Visibility.Collapsed;
            chargeBtn.Visibility = Visibility.Collapsed;
            healBtn.Visibility = Visibility.Collapsed;
            blueAPlab.Visibility = Visibility.Collapsed;
            redAPlab.Visibility = Visibility.Collapsed;
            blueAP.Visibility = Visibility.Collapsed;
            redAP.Visibility = Visibility.Collapsed;
            redMove.Visibility = Visibility.Collapsed;
            refreshBtn.Visibility = Visibility.Visible;
        }

        public void winCondition()
        {
            if (blueHealth.Value <= 0)
            {
                conditionLabel.Visibility = Visibility.Visible;
                conditionLabel.Content = "DEFEAT";
                collapseElements();
            }
            else if (redHealth.Value <= 0)
            {
                conditionLabel.Visibility = Visibility.Visible;
                conditionLabel.Content = "WINNER";
                collapseElements();
                
            }
            else if (blueHealth.Value <=0 && redHealth.Value <= 0)
            {
                conditionLabel.Visibility = Visibility.Visible;
                conditionLabel.Content = "DRAW";
                collapseElements();
            }
        }

        public void constraints()
        {
            if (player.Health > 100) { player.Health = 100; }
            if (enemy.Health > 100) { enemy.Health = 100; }
            if (player.AttackPower < 5) { player.AttackPower = 5; }
            if (enemy.AttackPower < 5) { enemy.AttackPower = 5; }
        }

        void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            //InitializeComponent();
        }
    }

    class Fighter
    {
        
        int health;
        int attackPower;
        int currentMove;

        public int Health { get; set; } = 100;
        public int AttackPower { get; set; } = 20;
        public int CurrentMove { get; set; }

        public Fighter() { }


        //Player action methods
       
    }

    class Enemy : Fighter
    {
        
        //public int Health { get; set; } = 100;
        //public int AttackPower { get; set; } = 20;
        //public int CurrentMove { get; set; }

        public Enemy() { }

        
        

        public int aiMoveSelector()
        {
            Random rnd = new Random();
            int randomMoveVal = rnd.Next(0, 4);
            return randomMoveVal;
        }
    }

    class Game
    {
        int fightCominations;

        public int FightCominations { get; set; }

        public Game() { }

        public Game(int fightCominations)
        {
            this.FightCominations = fightCominations;
        }


        public int checkAttack(int pMove, int eMove)
        {
            

            if (pMove == eMove) { this.FightCominations = 0; }
            if (pMove + 1 == eMove) { this.FightCominations = 1; }
            if (pMove + 2 == eMove) { this.FightCominations = 2; }
            if (pMove + 3 == eMove) { this.FightCominations = 3; }

            return FightCominations;
        }

        public int checkBlock(int pMove, int eMove)
        {
            if (pMove -1 == eMove) { this.FightCominations = 4; }
            else if (pMove == eMove) { this.FightCominations = 5; }
            else if (pMove + 1 == eMove) { this.FightCominations = 6; }
            else { this.FightCominations = 7; }

            return FightCominations;
        }

        public int checkCharge(int pMove, int eMove)
        {
            if (pMove -2 == eMove) { this.FightCominations = 8; }
            else if (pMove - 1 == eMove) { this.FightCominations = 9; }
            else if (pMove == eMove) { this.FightCominations = 10; }
            else { this.FightCominations = 11; }

            return FightCominations;
        }

        public int checkHeal(int pMove, int eMove)
        {
            if (pMove - 3 == eMove) { this.FightCominations = 12; }
            else if (pMove - 2 == eMove) { this.FightCominations = 13; }
            else if (pMove - 1 == eMove) { this.FightCominations = 14; }
            else { this.FightCominations = 15; }

            return FightCominations;
        }

        

    }
        

}

    