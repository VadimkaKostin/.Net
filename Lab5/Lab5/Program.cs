using Lab5.Players;
using Lab5.Strategy;
using System;
using System.Text;

namespace Lab5
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.Write("Оберіть складність гри\n1 - легка\n2 - середня\n3 - висока\n\nСкладінсть: ");
            
            int hardLevel = int.Parse(Console.ReadLine());

            Console.Clear();

            RealPlayer realPlayer = new RealPlayer();

            ComputerPlayer computerPlayer = new ComputerPlayer();
            
            switch(hardLevel)
            {
                case 1:
                    computerPlayer.ComputerStrategy = new RandomStrategy();
                    break;
                case 2:
                    computerPlayer.ComputerStrategy = new HeuristicStrategy();
                    break;
                case 3:
                    computerPlayer.ComputerStrategy = new MinimaxStrategy();
                    break;
            }

            Game game = new Game(realPlayer, computerPlayer);

            await game.Run();
        }
    }
}