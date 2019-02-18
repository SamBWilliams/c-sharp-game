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
            //
            Console.WriteLine("Enter your name: ");

            string playerName = Console.ReadLine();
            Console.WriteLine("Stats\n");

            //createPlayer(playerName);

            startGame(playerName);

            
            




        }

        class Fighter : IMoveSet 
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

            public virtual void attack()
            {
                this.CurrentMove = 0;
                //Console.WriteLine($"\nYou inflicted {this.AttackPower} damage");
                Console.WriteLine("\nYou attacked");
                //Testing current move value
                //Console.WriteLine(currentMove);
                
            }

            public virtual void block()
            {
                this.CurrentMove = 1;
                Console.WriteLine("\nYou blocked");
            }

            public virtual void chargeAttack()
            {
                this.CurrentMove = 2;
                Console.WriteLine("\nYou charged your attack power");
                this.AttackPower += 10;
            }

            public virtual void heal()
            {
                this.CurrentMove = 3;
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

            public override void attack()
            {
                Console.WriteLine("Enemy attacked");
            }

            public override void block()
            {
                Console.WriteLine("Enemy blocked");
            }

            public override void chargeAttack()
            {
                Console.WriteLine("Enemy charged attack");
                this.AttackPower += 20;
            }

            public override void heal()
            {
                Console.WriteLine("Enemy healed");
                this.Health += 20;
            }






        }

        interface IMoveSet
        {
            void attack();
            void block();
            void chargeAttack();
            void heal();
        }




        //Initialiser
        //public static void createPlayer(string n)
        //{
        //    var player = new Fighter(n, 100, 20);

        //    Console.WriteLine($"Name: {player.Name}\nHealth: {player.Health}\nAttack power: {player.AttackPower}");
        //}


        public static void startGame(string n)
        {
            var player = new Fighter(n, 100, 20);
            var enemy = new Enemy("Enemy", 100, 20);
            bool gameRunning = true;
            
            Console.WriteLine($"Name: {player.Name}\nHealth: {player.Health}\nAttack power: {player.AttackPower}");
            Console.WriteLine("\nEnemy stats");
            Console.WriteLine($"\nName: {enemy.Name}\nHealth: {enemy.Health}\nAttack power: {enemy.AttackPower}\n");


            Console.WriteLine("\n\n Choose your move\n");
            Console.WriteLine("a)Attack\nb)Block\nc)Charge attack power\nh)Heal");

           // char userInput = Console.ReadKey().KeyChar;


            //while(player.Health > 0)
            while(gameRunning)
            {
                char userInput = Console.ReadKey().KeyChar;
                switch (userInput)
                {
                    case 'a':
                        player.attack();
                        

                        //Both attack
                        if (player.CurrentMove == aiMoveSelector())
                        {
                            enemy.attack();
                            player.Health = player.Health - enemy.AttackPower;
                            enemy.Health = enemy.Health - player.AttackPower;
                            Console.WriteLine($"Your health: {player.Health}");
                            Console.WriteLine($"Enemy health: {enemy.Health}");
                        }
                        //Player attacks, AI blocks
                        else if (aiMoveSelector() == player.CurrentMove + 1)
                        {
                            enemy.block();
                            player.AttackPower -= 5;
                            Console.WriteLine($"Your attack power went down: {player.AttackPower}");

                        }
                        //Player attacks, AI charges attack
                        else if(aiMoveSelector() == player.CurrentMove + 2)
                        {
                            enemy.chargeAttack();
                            enemy.Health = enemy.Health - player.AttackPower;
                            Console.WriteLine($"Your health: {player.Health}");
                            Console.WriteLine($"Enemy health: {enemy.Health}");
                        }
                        //Player attacks, AI heals
                        else
                        {
                            enemy.heal();
                            enemy.Health = enemy.Health - player.AttackPower;
                            Console.WriteLine($"Your health: {player.Health}");
                            Console.WriteLine($"Enemy health: {enemy.Health}");
                        }
                        
                        break;

                    case 'b':
                        player.block();

                        //Both block
                        if (player.CurrentMove == aiMoveSelector())
                        {
                            enemy.block();
                            
                            Console.WriteLine($"Your health: {player.Health}");
                            Console.WriteLine($"Enemy health: {enemy.Health}");
                        }
                        //Player blocks, AI attacks
                        else if (aiMoveSelector() == player.CurrentMove - 1)
                        {
                            enemy.attack();
                            enemy.AttackPower -= 5;
                            Console.WriteLine($"Enemies attack power went down: {enemy.AttackPower}");

                        }
                        //Player blocks, AI charges attack
                        else if (aiMoveSelector() == player.CurrentMove + 1)
                        {
                            enemy.chargeAttack();
                            Console.WriteLine($"Enemy attack power went up: {enemy.AttackPower}");
                            Console.WriteLine($"Your health: {player.Health}");
                            Console.WriteLine($"Enemy health: {enemy.Health}");
                        }
                        //Player blocks, AI heals
                        else
                        {
                            enemy.heal();
                            
                            Console.WriteLine($"Your health: {player.Health}");
                            Console.WriteLine($"Enemy health: {enemy.Health}");
                        }
                        break;

                    case 'c':
                        player.chargeAttack();
                        // Console.WriteLine($"Your attack power is: {player.AttackPower}");

                        //Both charge attack
                        if (player.CurrentMove == aiMoveSelector())
                        {
                            enemy.chargeAttack();

                            Console.WriteLine($"Enemy attack power went up: {enemy.AttackPower}");
                            Console.WriteLine($"Your attack power went up: {player.AttackPower}");
                            Console.WriteLine($"Your health: {player.Health}");
                            Console.WriteLine($"Enemy health: {enemy.Health}");
                        }
                        //Player charges, AI attacks
                        else if (aiMoveSelector() == player.CurrentMove - 2)
                        {
                            enemy.attack();
                            player.Health -= enemy.AttackPower;
                            Console.WriteLine($"Your attack attack power went up: {player.AttackPower}");
                            Console.WriteLine($"Your health: {player.Health}");
                            Console.WriteLine($"Enemy health: {enemy.Health}");

                        }
                        //Player charges, AI blocks
                        else if (aiMoveSelector() == player.CurrentMove + 1)
                        {
                            enemy.block();

                            Console.WriteLine($"Your attack attack power went up: {player.AttackPower}");
                            Console.WriteLine($"Your health: {player.Health}");
                            Console.WriteLine($"Enemy health: {enemy.Health}");
                        }
                        //Player charges, AI heals
                        else
                        {
                            enemy.heal();
                            Console.WriteLine($"Your attack attack power went up: {player.AttackPower}");
                            Console.WriteLine($"Your health: {player.Health}");
                            Console.WriteLine($"Enemy health: {enemy.Health}");
                        }
                        break;

                    case 'h':
                        player.heal();
                        //Console.WriteLine($"Your health is: {player.Health}");

                        //Both heal
                        if (player.CurrentMove == aiMoveSelector())
                        {
                            enemy.heal();

                            
                            Console.WriteLine($"Your health: {player.Health}");
                            Console.WriteLine($"Enemy health: {enemy.Health}");
                        }
                        //Player heal, AI attacks
                        else if (aiMoveSelector() == player.CurrentMove - 3)
                        {
                            enemy.attack();
                            player.Health -= enemy.AttackPower;

                            Console.WriteLine($"Your health: {player.Health}");
                            Console.WriteLine($"Enemy health: {enemy.Health}");

                        }
                        //Player charges, AI blocks
                        else if (aiMoveSelector() == player.CurrentMove + 1)
                        {
                            enemy.block();

                            Console.WriteLine($"Your attack attack power went up: {player.AttackPower}");
                            Console.WriteLine($"Your health: {player.Health}");
                            Console.WriteLine($"Enemy health: {enemy.Health}");
                        }
                        //Player charges, AI heals
                        else
                        {
                            enemy.heal();
                            Console.WriteLine($"Your attack attack power went up: {player.AttackPower}");
                            Console.WriteLine($"Your health: {player.Health}");
                            Console.WriteLine($"Enemy health: {enemy.Health}");
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

        public static int aiMoveSelector()
        {
            //Random function to decide move
            //Number chosen from random is decides on move function

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
