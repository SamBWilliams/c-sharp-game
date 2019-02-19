using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fighting_game_03_refactored
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Enter your name to start the game: ");
            //string playerName = Console.ReadLine();
            //startGame(playerName);

            var game = new Game();
            game.start();
        }
        //Player class
        class Fighter
        {
            string name;
            int health;
            int attackPower;
            int currentMove;

            public string Name { get; set; }
            public int Health { get; set; }
            public int AttackPower { get; set; }
            public int CurrentMove { get; set; }

            public Fighter() { }

            public Fighter(string name, int health, int attackPower)
            {
                this.Name = name;
                this.Health = health;
                this.AttackPower = attackPower;
            }

            //Player action methods
            public virtual int attack(int pAttack, int eMove, int eAttack, int eHealth)
            {
                this.CurrentMove = 0;
                if (eMove == this.CurrentMove)
                {
                    this.Health -= eAttack;

                }
                else if (eMove == this.CurrentMove + 1)
                {
                    this.AttackPower -= 5;
                }
                else
                {
                    eHealth -= this.AttackPower;
                }
                Console.WriteLine("\nYou attacked");
                return eHealth;
            }

            public virtual void block(int eMove, int eAttack)
            {
                this.CurrentMove = 1;

                if (eMove == this.CurrentMove - 1)
                {
                    eAttack -= 5;
                }
                Console.WriteLine("\nYou blocked");
            }

            public virtual void chargeAttack(int eMove, int eAttack)
            {
                this.CurrentMove = 2;
                this.AttackPower += 10;

                if (eMove == this.CurrentMove - 2)
                {
                    this.Health -= eAttack;
                }
                Console.WriteLine("\nYou charged your attack power");
            }

            public virtual void heal(int eMove, int eAttack)
            {
                this.CurrentMove = 3;
                if (eMove == this.CurrentMove - 2)
                {
                    this.Health -= eAttack;
                }
                Console.WriteLine("\nYou healed");
                this.Health += 20;
            }
        }

        //Enemy class
        class Enemy : Fighter
        {
            public string Name { get; set; }
            public int Health { get; set; }
            public int AttackPower { get; set; }
            public int CurrentMove { get; set; }

            public Enemy() { }

            public Enemy(string name, int health, int attackPower)
            {
                this.Name = name;
                this.Health = health;
                this.AttackPower = attackPower;
            }


            public override int attack(int eAttack, int pMove, int pAttack, int pHealth)
            {

                this.CurrentMove = 0;
                if (pMove == this.CurrentMove)
                {
                    this.Health -= pAttack;

                }
                else if (pMove == this.CurrentMove + 1)
                {
                    this.AttackPower -= 5;
                }
                else
                {
                    pHealth -= this.AttackPower;
                }

                Console.WriteLine("Enemy attacked");
                return pHealth;
            }


            public override void block(int pMove, int pAttack)
            {
                this.CurrentMove = 1;

                if (pMove == this.CurrentMove - 1)
                {
                    pAttack -= 5;
                }
                Console.WriteLine("\nEnemy blocked");
            }


            public override void chargeAttack(int pMove, int pAttack)
            {
                this.CurrentMove = 2;

                this.AttackPower += 10;

                if (pMove == this.CurrentMove - 2)
                {
                    this.Health -= pAttack;
                }
                Console.WriteLine("\nEnemy charged their attack power");
            }

            public override void heal(int pMove, int pAttack)
            {
                this.CurrentMove = 3;
                if (pMove == this.CurrentMove - 3)
                {
                    this.Health -= pAttack;
                }
                Console.WriteLine("Enemy healed");
                this.Health += 20;
            }

            public static int aiMoveSelector()
            {
                Random rnd = new Random();
                int randomMoveVal = rnd.Next(0, 4);
                return randomMoveVal;
            }
        }


        //Game class
        class Game
        {
            //Fighter player = new Fighter(n, 100, 20);
            Enemy enemy = new Enemy("Enemy", 100, 20);
            bool gameRunning = true;

            public void start()
            {
                Console.WriteLine("***Welcome to Champions!***");
                Console.WriteLine("Enter your name to start the game");
                string name = Console.ReadLine();
                startFight(name);
            }

            public void startFight(string n)
            {
                var player = new Fighter(n, 100, 20);
                displayStats(player.Name, player.Health, player.AttackPower, enemy.Name, enemy.Health, enemy.AttackPower);
                Console.WriteLine("\nThe fight begins!");
                Console.WriteLine("\n\n Choose your move\n");

                while (gameRunning)
                {
                    Console.WriteLine("\na)Attack\nb)Block\nc)Charge attack power\nh)Heal\ns)Display stats\n");
                    char userInput = Console.ReadKey().KeyChar;

                    switch (userInput)
                    {
                        case 'a':
                            //Both attack
                            player.attack(player.AttackPower, Enemy.aiMoveSelector(), enemy.AttackPower, enemy.Health);
                            if (player.CurrentMove == Enemy.aiMoveSelector())
                            {
                                enemy.attack(enemy.AttackPower, player.CurrentMove, player.AttackPower, player.Health);
                                Console.WriteLine($"Your health: {player.Health}");
                                Console.WriteLine($"Enemy health: {enemy.Health}");
                            }
                            //Player attacks, AI blocks
                            else if (Enemy.aiMoveSelector() == player.CurrentMove + 1)
                            {
                                enemy.block(player.CurrentMove, player.AttackPower);
                                Console.WriteLine($"Your attack power decresed to: {player.AttackPower}");
                                Console.WriteLine($"Enemy health: {enemy.Health}");
                            }
                            //Player attacks, AI charges
                            else if (Enemy.aiMoveSelector() == player.CurrentMove + 2)
                            {
                                enemy.chargeAttack(player.CurrentMove, player.AttackPower);
                                Console.WriteLine($"Enemy attack power increase to: {enemy.AttackPower}");
                                Console.WriteLine($"Enemy health: {enemy.Health}");
                            }
                            //Player attacks, AI heals
                            else if (Enemy.aiMoveSelector() == player.CurrentMove + 3)
                            {
                                enemy.heal(player.CurrentMove, player.AttackPower);
                                Console.WriteLine($"Enemy health: {enemy.Health}");
                            }
                            break;

                        case 'b':
                            //Both block
                            player.block(Enemy.aiMoveSelector(), enemy.AttackPower);
                            if (player.CurrentMove == Enemy.aiMoveSelector())
                            {
                                enemy.block(player.CurrentMove, player.AttackPower);
                            }
                            //Player blocks, enemy attacks
                            else if (Enemy.aiMoveSelector() == player.CurrentMove - 1)
                            {
                                enemy.attack(enemy.AttackPower, player.CurrentMove, player.AttackPower, player.Health);
                                Console.WriteLine($"Enemy attack power decresed to: {enemy.AttackPower}");
                            }
                            //Player blocks, enemy charges
                            else if (Enemy.aiMoveSelector() == player.CurrentMove + 1)
                            {
                                enemy.chargeAttack(player.CurrentMove, player.AttackPower);
                                Console.WriteLine($"Enemy attack power increased to: {enemy.AttackPower}");
                            }
                            //Player blocks, enemy heals
                            else if (Enemy.aiMoveSelector() == player.CurrentMove + 2)
                            {
                                enemy.heal(player.CurrentMove, player.AttackPower);
                                Console.WriteLine($"Enemy health: {enemy.Health}");
                            }
                            break;

                        case 'c':

                            //Both charge
                            player.chargeAttack(enemy.CurrentMove, enemy.AttackPower);
                            Console.WriteLine($"Player attack increased to: {player.AttackPower}");
                            if (player.CurrentMove == Enemy.aiMoveSelector())
                            {
                                enemy.chargeAttack(player.CurrentMove, player.AttackPower);
                                Console.WriteLine($"Enemy attack increased to: {enemy.AttackPower}");
                            }
                            //Player charges, enemy attacks
                            else if (Enemy.aiMoveSelector() == player.CurrentMove - 2)
                            {
                                enemy.attack(enemy.AttackPower, player.CurrentMove, player.AttackPower, player.Health);
                                Console.WriteLine($"Your health: {player.Health}");
                            }
                            //Player charges, enemy blocks
                            else if (Enemy.aiMoveSelector() == player.CurrentMove - 1)
                            {
                                enemy.block(player.CurrentMove, player.AttackPower);
                            }
                            //Player charges, enemy heals
                            else if (Enemy.aiMoveSelector() == player.CurrentMove + 1)
                            {
                                enemy.heal(enemy.CurrentMove, enemy.AttackPower);
                                Console.WriteLine($"Enemy health: {enemy.Health}");
                            }
                            break;

                        case 'h':

                            //Both heal
                            player.heal(enemy.CurrentMove, enemy.AttackPower);
                            Console.WriteLine($"Your health: {player.Health}");
                            if (Enemy.aiMoveSelector() == player.CurrentMove)
                            {
                                enemy.heal(player.CurrentMove, player.AttackPower);
                                Console.WriteLine($"Enemy health: {enemy.Health}");
                            }
                            //Player heals, enemy attacks
                            else if (Enemy.aiMoveSelector() == player.CurrentMove - 3)
                            {
                                enemy.attack(enemy.AttackPower, player.CurrentMove, player.AttackPower, player.Health);
                                Console.WriteLine($"Your health: {player.Health}");
                            }
                            //Player heals, enemy blocks
                            else if (Enemy.aiMoveSelector() == player.CurrentMove - 2)
                            {
                                Console.WriteLine("Enemy blocks");
                            }
                            //Player heals, enemy charges
                            else
                            {
                                enemy.chargeAttack(player.CurrentMove, player.AttackPower);
                                Console.WriteLine($"Enemy attack increased to: {enemy.AttackPower}");
                            }
                            break;
                        case 's':
                            displayStats(player.Name, player.Health, player.AttackPower, enemy.Name, enemy.Health, enemy.AttackPower);
                            break;

                        default:
                            Console.WriteLine("\nIncorrect input");
                            break;
                    }

                    //Win/Lose condition
                    if (player.Health <= 0)
                    {
                        gameRunning = false;
                        Console.WriteLine("\n\nYou lose");
                    }
                    else if (enemy.Health <= 0)
                    {
                        gameRunning = false;
                        Console.WriteLine("\n\nYou win");
                    }
                }
            }

            public void displayStats(string pName, int pHealth, int pAttack, string eName, int eHealth, int eAttack)
            {
                Console.WriteLine("Your stats\n");
                Console.WriteLine($"Name: {pName}\nHealth: {pHealth}\nAttack power: {pAttack}");
                Console.WriteLine("\nEnemy stats");
                Console.WriteLine($"\nName: {eName}\nHealth: {eHealth}\nAttack power: {eAttack}\n");
            }




        }
        //Initialiser
        

        //Select enemy move value
        //public static int aiMoveSelector()
        //{
        //    Random rnd = new Random();
        //    int randomMoveVal = rnd.Next(0, 4);
        //    return randomMoveVal;
        //}

        //Shows stats
        //public static void displayStats(string pName, int pHealth, int pAttack, string eName, int eHealth, int eAttack)
        //{
        //    Console.WriteLine("Your stats\n");
        //    Console.WriteLine($"Name: {pName}\nHealth: {pHealth}\nAttack power: {pAttack}");
        //    Console.WriteLine("\nEnemy stats");
        //    Console.WriteLine($"\nName: {eName}\nHealth: {eHealth}\nAttack power: {eAttack}\n");
        //}
    }
}

