using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fighting_game_01
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Enter your name to start the game: ");

            string playerName = Console.ReadLine();
            Console.WriteLine("Stats\n");
            startGame(playerName);

        }
        //Player class
        class Fighter //: IMoveSet 
        {
            string name;
            int health;
            int attackPower;
            int currentMove;

            public string Name { get; set; }
            public int Health { get; set; }
            public int AttackPower { get; set; }
            public int CurrentMove { get; set; }

            public Fighter() {}

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
                if(eMove == this.CurrentMove)
                {
                    this.Health -= eAttack;
                    
                }
                else if(eMove == this.CurrentMove + 1)
                {
                    this.AttackPower -= 5;
                }
                else
                {
                    eHealth -= this.AttackPower;
                    //eHealth -= pAttack;
                }

                Console.WriteLine("\nYou attacked");
                return eHealth;
            }

            public virtual void block(int eMove, int eAttack)
            {
                this.CurrentMove = 1;

                if(eMove == this.CurrentMove - 1)
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

        //Computer class
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
            //public override void block()
            //{
            //    Console.WriteLine("Enemy blocked");
            //}

            public override void block(int pMove, int pAttack)
            {
                this.CurrentMove = 1;

                if (pMove == this.CurrentMove - 1)
                {
                    pAttack -= 5;
                }
                Console.WriteLine("\nEnemy blocked");
            }

            //public override void chargeAttack()
            //{
            //    Console.WriteLine("Enemy charged attack");
            //    this.AttackPower += 20;
            //}

            //public override void heal()
            //{
            //    Console.WriteLine("Enemy healed");
            //    this.Health += 20;
            //}

            public override void chargeAttack(int pMove, int pAttack)
            {
                this.CurrentMove = 2;

                this.AttackPower += 10;

                if (pMove == this.CurrentMove - 2)
                {
                    this.Health -= pAttack;
                }
                Console.WriteLine("\nYou charged your attack power");
            }

            public override void heal(int pMove, int pAttack)
            {
                this.CurrentMove = 3;
                if (pMove == this.CurrentMove - 2)
                {
                    this.Health -= pAttack;
                }
                Console.WriteLine("\nYou healed");
                this.Health += 20;
            }



        }

        //interface IMoveSet
        //{
        //    void attack();
        //    void block();
        //    void chargeAttack();
        //    void heal();
        //}




        //Initialiser
        public static void startGame(string n)
        {
            var player = new Fighter(n, 100, 20);
            var enemy = new Enemy("Enemy", 100, 20);
            //var f = new Fight();
            //Fighter en = new Enemy("En2", 100, 20);
            //Console.WriteLine("\nEnemy stats");
            //Console.WriteLine($"\nName: {en.Name}\nHealth: {en.Health}\nAttack power: {en.AttackPower}\n");
            bool gameRunning = true;
            
            Console.WriteLine($"Name: {player.Name}\nHealth: {player.Health}\nAttack power: {player.AttackPower}");
            Console.WriteLine("\nEnemy stats");
            Console.WriteLine($"\nName: {enemy.Name}\nHealth: {enemy.Health}\nAttack power: {enemy.AttackPower}\n");
            Console.WriteLine("\n\n Choose your move\n");
            Console.WriteLine("a)Attack\nb)Block\nc)Charge attack power\nh)Heal");

            //var pCm = player.CurrentMove;

   
            while(gameRunning)
            {
                char userInput = Console.ReadKey().KeyChar;
                switch (userInput)
                {
                    case 'a':
                        //Both attack
                        //player.attack(player.AttackPower, aiMoveSelector(), enemy.AttackPower, enemy.Health);
                        if (player.CurrentMove == aiMoveSelector())
                        {
                            player.attack(player.AttackPower, aiMoveSelector(), enemy.AttackPower, enemy.Health);
                            enemy.attack(enemy.AttackPower, player.CurrentMove, player.AttackPower, player.Health);
                            Console.WriteLine($"Your health: {player.Health}");
                            Console.WriteLine($"Enemy health: {enemy.Health}");
                        }
                        //Player attacks, AI blocks
                        else if (aiMoveSelector() == player.CurrentMove + 1)
                        {
                            player.attack(player.AttackPower, aiMoveSelector(), enemy.AttackPower, enemy.Health);
                            enemy.block(player.CurrentMove, player.AttackPower);
                            Console.WriteLine($"Your attack power decresed to: {player.AttackPower}");
                            
                        }
                        //Player attacks, AI charges
                        else if (aiMoveSelector() == player.CurrentMove + 2)
                        {
                            player.attack(player.AttackPower, aiMoveSelector(), enemy.AttackPower, enemy.Health);
                            enemy.chargeAttack(player.CurrentMove, player.AttackPower);
                           // Console.WriteLine(enemy.Health);
                            Console.WriteLine($"Enemy attack power increase to: {enemy.AttackPower}");
                        }
                        //Player attacks, AI heals
                        else if (aiMoveSelector() == player.CurrentMove + 3)
                        {
                            player.attack(player.AttackPower, aiMoveSelector(), enemy.AttackPower, enemy.Health);
                            enemy.heal(player.CurrentMove, player.AttackPower);
                            Console.WriteLine($"Enemy health: {enemy.Health}");
                        }
                            break;

                    case 'b':
                        //Both block
                        if (player.CurrentMove == aiMoveSelector())
                        {
                            player.block(aiMoveSelector(), enemy.AttackPower);
                            enemy.block(player.CurrentMove, player.AttackPower);
                        }
                        //Player blocks, enemy attacks
                        else if (aiMoveSelector() == player.CurrentMove - 1)
                        {
                            player.block(aiMoveSelector(), enemy.AttackPower);
                            enemy.attack(enemy.AttackPower, player.CurrentMove, player.AttackPower, player.Health);
                            Console.WriteLine($"Enemy attack power decresed to: {enemy.AttackPower}");
                        }
                        //Player blocks, enemy charges
                        else if (aiMoveSelector() == player.CurrentMove + 1)
                        {
                            player.block(aiMoveSelector(), enemy.AttackPower);
                            enemy.chargeAttack(player.CurrentMove, player.AttackPower);
                            Console.WriteLine($"Enemy attack power increased to: {enemy.AttackPower}");
                        }
                        //Player blocks, enemy heals
                        else if (aiMoveSelector() == player.CurrentMove + 2)
                        {
                            player.block(aiMoveSelector(), enemy.AttackPower);
                            enemy.heal(player.CurrentMove, player.AttackPower);
                            Console.WriteLine($"Enemy health: {enemy.Health}");
                        }
                        break;

                    case 'c':

                        //Both charge
                        player.chargeAttack(enemy.CurrentMove, enemy.AttackPower);
                        Console.WriteLine($"Player attack increased to: {player.AttackPower}");
                        if (player.CurrentMove == aiMoveSelector())
                        {
                            enemy.chargeAttack(player.CurrentMove, player.AttackPower);
                            Console.WriteLine($"Enemy attack increased to: {enemy.AttackPower}");
                            //Console.WriteLine($"Your attack power increased to: {player.AttackPower}");

                        }
                        //Player charges, enemy attacks
                        else if(aiMoveSelector() == player.CurrentMove - 2)
                        {
                            enemy.attack(enemy.AttackPower, player.CurrentMove, player.AttackPower, player.Health);
                            //Console.WriteLine($"Your attack power increased to: {player.AttackPower}");
                            Console.WriteLine($"Your health: {player.Health}");
                        }
                        //Player charges, enemy blocks
                        else if (aiMoveSelector() == player.CurrentMove - 1)
                        {
                            enemy.block(player.CurrentMove, player.AttackPower);
                           // Console.WriteLine($"Your attack power increased to: {player.AttackPower}");
                        }
                        //Player charges, enemy heals
                        else if (aiMoveSelector() == player.CurrentMove + 1)
                        {
                            enemy.heal(enemy.CurrentMove, enemy.AttackPower);
                           // Console.WriteLine($"Enemy attack increased to: {enemy.AttackPower}");
                            Console.WriteLine($"Enemy health: {enemy.Health}");
                        }
                        break;

                    case 'h':

                        //Both heal
                        player.heal(enemy.CurrentMove, enemy.AttackPower);
                        Console.WriteLine($"Your health: {player.Health}");
                        if (aiMoveSelector() == player.CurrentMove)
                        {
                            enemy.heal(player.CurrentMove, player.AttackPower);
                            //Console.WriteLine($"Enemy health: {enemy.Health}");
                        }
                        //Player heals, enemy attacks
                        else if (aiMoveSelector() == player.CurrentMove - 3)
                        {
                            enemy.attack(enemy.AttackPower, player.CurrentMove, player.AttackPower, player.Health);
                            //Console.WriteLine($"Your attack power increased to: {player.AttackPower}");
                            Console.WriteLine($"Your health: {player.Health}");
                        }
                        //Player heals, enemy blocks
                        else if(aiMoveSelector() == player.CurrentMove - 2)
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
                }

                if(player.Health < 0)
                {
                    gameRunning = false;
                    Console.WriteLine("You lose");
                }
                else if(enemy.Health < 0)
                {
                    gameRunning = false;
                    Console.WriteLine("You win");
                }
            }
            

            
        }

        //public static void bothAttack(int pHealth, int pAttack, int eHealth, int eAttack)
        //{
        //    pHealth -= eAttack;
        //    eHealth -= pAttack;
        //    Console.WriteLine($"Your health: {pHealth}");
        //    Console.WriteLine($"Enemy health: {eHealth}");


        //}

        //public Fighter BothAttack(Fighter p)
        //{


        //    return p;
        //}





        public static void pAttackEblock(int pAttack)
        {

        }

        public static int aiMoveSelector()
        { 

            Random rnd = new Random();

            int randomMoveVal = rnd.Next(0, 4);

            return randomMoveVal;
        }




        //Test
        //public static Fighter createPlayer(string n)
        //{
        //    var player = new Fighter(n, 100, 20);

        //    Console.WriteLine($"Name: {player.Name}\nHealth: {player.Health}\nAttack power: {player.AttackPower}");

        //    return player;
        //}
    }
}
