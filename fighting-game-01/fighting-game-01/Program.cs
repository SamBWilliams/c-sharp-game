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
            Console.WriteLine("Stats");

            //createPlayer(playerName);

            startGame(playerName);

            
            




        }

        class Fighter : IMoveSet 
        {
            string name;
            int health;
            int attackPower;

            public string Name { get; set; }
            public int Health { get; set; }
            public int AttackPower { get; set; }

            public Fighter() {}

            public Fighter(string name, int health, int attackPower)
            {
                this.Name = name;
                this.Health = health;
                this.AttackPower = attackPower;
            }


            //Player action methods

            public void attack()
            {
                Console.WriteLine("\nYou attacked");
            }

            public void block()
            {
                Console.WriteLine("\nYou blocked");
            }

            public void chargeAttack()
            {
                Console.WriteLine("\nYou charged your attack power");
                this.AttackPower += 10;
            }

            public void heal()
            {
                Console.WriteLine("\nYou healed");
                this.Health += 20;
            }
        }

        //Computer class
        class Computer : Fighter
        {

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
            bool gameRunning = true;

            Console.WriteLine($"Name: {player.Name}\nHealth: {player.Health}\nAttack power: {player.AttackPower}");


            Console.WriteLine("\n\n Choose your move\n");
            Console.WriteLine("a)Attack\nb)Block\nc)Charge attack power\nh)Heal");

           // char userInput = Console.ReadKey().KeyChar;


            while(player.Health > 0)
            {
                char userInput = Console.ReadKey().KeyChar;
                switch (userInput)
                {
                    case 'a':
                        player.attack();
                        //Call to computer actions functions here
                        //Update player status afterwards
                        break;

                    case 'b':
                        player.block();
                        break;

                    case 'c':
                        player.chargeAttack();
                        Console.WriteLine($"Your attack power is: {player.AttackPower}");
                        break;

                    case 'h':
                        player.heal();
                        Console.WriteLine($"Your health is: {player.Health}");
                        break;
                }
            }
            

            
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
