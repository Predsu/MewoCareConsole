using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MewoCareConsole
{
    class CoinFlip
    {
        private static string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static string appFolderPath = Path.Combine(appDataPath, "MewoCareDesktop");
        public static void Start()
        {
            Console.WriteLine("CoinFlip Game");
            Start:
            Console.WriteLine("Money amount you have: {0}", Game.money);
            Console.WriteLine("Place your bet (type 0 to exit): ");
            double bet = double.Parse(Console.ReadLine());
            if (bet == 0)
            {
                Game.saveDoubleData(Game.money, Path.Combine(appFolderPath, "money.mwcr"));
                Console.Clear();
                Game.Start(true);
            }
            Game.money -= bet;
            Console.WriteLine("Type 0 to tails, 1 to heads, any other to exit");
            if (bet <= Game.money) {
                int choice = int.Parse(Console.ReadLine());
                Random generator = new Random();
                int result = generator.Next(0, 2);
                if (choice != 0 && choice != 1)
                {
                    Game.saveDoubleData(Game.money, Path.Combine(appFolderPath, "money.mwcr"));
                    Console.Clear();
                    Game.Start(true);
                }
                else if (choice == result)
                {
                    Game.money += bet * 2;
                    Console.WriteLine("Won");
                    goto Start;
                } else if (choice != result)
                {
                    Console.WriteLine("Lost");
                    goto Start;
                }
            } else
            {
                Console.WriteLine("You don't have enough money");
            }
        }
    }
}
