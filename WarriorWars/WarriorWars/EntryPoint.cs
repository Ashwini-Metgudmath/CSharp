using System;
using System.Threading;
using WarriorWars.Enum;

namespace WarriorWars
{
    class EntryPoint
    {
        static Random rng = new Random();
        static void Main()
        {
            try
            {
                Warrior goodGuy = new Warrior("Bob", Faction.goodGuy);
                Warrior badGuy = new Warrior("Joe", Faction.badGuy);

                while (goodGuy.IsAlive && badGuy.IsAlive)
                {
                    if (rng.Next(0, 10) < 5)
                    {
                        goodGuy.attack(badGuy);
                    }
                    else
                    {
                        badGuy.attack(goodGuy);
                    }

                    Thread.Sleep(50);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Console.WriteLine("Press enter to close...");
                Console.ReadLine();
            }

        }
    }
}
