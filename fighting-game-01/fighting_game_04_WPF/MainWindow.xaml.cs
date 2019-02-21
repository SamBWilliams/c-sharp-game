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
            bluePlayer.Visibility = Visibility.Collapsed;
            redPlayer.Visibility = Visibility.Collapsed;
            bothFighters.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\bothCharge.png", UriKind.Absolute));
            bothFighters.Visibility = Visibility.Visible;

            blueName.Content = nameText.Text;
            blueAP.Content = player.AttackPower;
            redAP.Content = enemy.AttackPower;


        }

        public void AttackBtn_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Open(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\sounds\steelsword.mp3", UriKind.Absolute));
            mediaPlayer.Play();
            player.CurrentMove = 0;
            
           
            if (game.checkAttack(player.CurrentMove, enemy.aiMoveSelector()) == 0)
            {
                bothFighters.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\bothAtk.png", UriKind.Absolute));
               
                player.Health -= enemy.AttackPower1;
                enemy.Health1 -= player.AttackPower;
                blueHealth.Value -= enemy.AttackPower;
                redHealth.Value -= player.AttackPower;
                redMove.Content = "Attacks";
            }
            if (game.checkAttack(player.CurrentMove, enemy.aiMoveSelector()) == 1)
            {
                
                bothFighters.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\bAeB.png", UriKind.Absolute));
                player.AttackPower -= 5;
                if(player.AttackPower < 5) { player.AttackPower = 5; }
                blueAP.Content = player.AttackPower;
                redMove.Content = "Blocks";
            }
            else if (game.checkAttack(player.CurrentMove, enemy.aiMoveSelector()) == 2)
            {
                bothFighters.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\bAeC.png", UriKind.Absolute));
                
                redHealth.Value -= player.AttackPower;
                enemy.Health1 -= player.AttackPower;
                enemy.AttackPower += 10;
                redAP.Content = enemy.AttackPower;
                redMove.Content = "Attack charged";
                
            }
            else if (game.checkAttack(player.CurrentMove, enemy.aiMoveSelector()) == 3)
            {
                bothFighters.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\bAeH.png", UriKind.Absolute));              
                redHealth.Value -= player.AttackPower;
                enemy.Health1 -= player.AttackPower;
                redHealth.Value += 20;
                redMove.Content = "Heals";
            }
            
            winCondition();          
           
        }
    
        private void BlockBtn_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Open(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\sounds\swordecho.mp3", UriKind.Absolute));
            mediaPlayer.Play();
            player.CurrentMove = 1;
            //bluePlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\blueBlock.png", UriKind.Absolute));
            if (game.checkBlock(player.CurrentMove, enemy.aiMoveSelector()) == 4)
            {
                bothFighters.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\bBeA.png", UriKind.Absolute));
                enemy.AttackPower1 -= 5;
                redAP.Content = enemy.AttackPower1;
                redMove.Content = "Attack";
            }
            else if (game.checkBlock(player.CurrentMove, enemy.aiMoveSelector()) == 5)
            {
                bothFighters.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\bothBlock.png", UriKind.Absolute));
                //redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redBlock.png", UriKind.Absolute));
                redMove.Content = "Blocks";

            }
            else if (game.checkBlock(player.CurrentMove, enemy.aiMoveSelector()) == 6)
            {
                bothFighters.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\bBeC.png", UriKind.Absolute));
                //redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redCharge.png", UriKind.Absolute));
                enemy.AttackPower1 += 10;
                redAP.Content = enemy.AttackPower1;
                redMove.Content = "Attack charged";
                
            }
            else if (game.checkBlock(player.CurrentMove, enemy.aiMoveSelector()) == 7)
            {
                bothFighters.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\bBeH.png", UriKind.Absolute));
                //redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redIdel.png", UriKind.Absolute));
                redHealth.Value += 20;
                redMove.Content = "Heals";
            }

            winCondition();
        }

        private void ChargeBtn_Click(object sender, RoutedEventArgs e)
        {
            player.CurrentMove = 2;
            player.AttackPower += 10;
            blueAP.Content = player.AttackPower;
            //bluePlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\blueCharge.png", UriKind.Absolute));
            if (game.checkCharge(player.CurrentMove, enemy.aiMoveSelector()) == 8)
            {
                bothFighters.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\bCeA.png", UriKind.Absolute));
                //redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redAttack.png", UriKind.Absolute));
                player.Health -= enemy.AttackPower;
                blueHealth.Value -= enemy.AttackPower;
                redMove.Content = "Attacks";
            }
            else if (game.checkCharge(player.CurrentMove, enemy.aiMoveSelector()) == 9)
            {
                bothFighters.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\bCeB.png", UriKind.Absolute));
                //redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redBlock.png", UriKind.Absolute));
                redMove.Content = "Blocks";
            }
            else if (game.checkCharge(player.CurrentMove, enemy.aiMoveSelector()) == 10)
            {
                bothFighters.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\bothCharge.png", UriKind.Absolute));
                //redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redCharge.png", UriKind.Absolute));
                enemy.AttackPower1 += 10;
                redAP.Content = enemy.AttackPower1;
                redMove.Content = "Attack charged";
                
            }
            else if (game.checkCharge(player.CurrentMove, enemy.aiMoveSelector()) == 11)
            {
                bothFighters.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\bCeH.png", UriKind.Absolute));
                //redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redIdel.png", UriKind.Absolute));
                redHealth.Value +=20;
                redMove.Content = "Heals";
            }

            winCondition();
            //constraints();
        }

        private void HealBtn_Click(object sender, RoutedEventArgs e)
        {
            player.CurrentMove = 3;
            blueHealth.Value += 20;
           
            //bluePlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\blueIdel.png", UriKind.Absolute));
            if (game.checkHeal(player.CurrentMove, enemy.aiMoveSelector()) == 12)
            {
                bothFighters.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\bHeA.png", UriKind.Absolute));
                player.Health -= enemy.AttackPower1;
                blueHealth.Value -= enemy.AttackPower;
                redMove.Content = "Attacks";
            }
            else if (game.checkHeal(player.CurrentMove, enemy.aiMoveSelector()) == 13)
            {
                bothFighters.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\bHeB.png", UriKind.Absolute));
                //redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redBlock.png", UriKind.Absolute));
                redMove.Content = "Blocks";
            }
            else if (game.checkHeal(player.CurrentMove, enemy.aiMoveSelector()) == 14)
            {
                bothFighters.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\bHeC.png", UriKind.Absolute));
                //redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redCharge.png", UriKind.Absolute));
                enemy.AttackPower1 += 10;
                redAP.Content = enemy.AttackPower1;
                redMove.Content = "Attack charged";
                
            }
            else if (game.checkHeal(player.CurrentMove, enemy.aiMoveSelector()) == 15)
            {
                bothFighters.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\bothHeal.png", UriKind.Absolute));
                //redPlayer.Source = new BitmapImage(new Uri(@"C:\c-sharp-game\fighting-game-01\fighting_game_04_WPF\Images\redIdel.png", UriKind.Absolute));
                redHealth.Value += 20;
                redMove.Content = "Heals";
            }
            winCondition();
            //constraints();
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
            //refreshBtn.Visibility = Visibility.Visible;
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
    }

    class Enemy : Fighter
    {

        public int Health1 { get; set; } = 100;
        public int AttackPower1 { get; set; } = 20;
        public int CurrentMove1 { get; set; }

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

    