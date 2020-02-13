using WarriorWars.Enum;
using WarriorWars.Equipment;
using System;

namespace WarriorWars
{
    class Warrior
    {
        private const int GOOD_GUY_STARTING_HEALTH = 100;
        private const int BAD_GUY_STARTING_HEALTH = 100;

        private readonly Faction FACTION;

        private string name;
        private int health;
        private bool isAlive;

        public bool IsAlive
        {
            get
            {
                return isAlive;
            }
        }

        private Weapon weapon;
        private Armor armor;

        public Warrior(string name, Faction faction)
        {
            this.name = name;
            this.FACTION = faction;
            isAlive = true;

            switch (faction)
            {
                case Faction.goodGuy:
                    weapon = new Weapon(faction);
                    armor = new Armor(faction);
                    health = GOOD_GUY_STARTING_HEALTH;
                    break;
                case Faction.badGuy:
                    weapon = new Weapon(faction);
                    armor = new Armor(faction);
                    health = BAD_GUY_STARTING_HEALTH;
                    break;
                default:
                    break;
            }

        }

        public void attack(Warrior enemy)
        {
            int damage =  enemy.armor.ArmorPoints/weapon.Damage;
            enemy.health -= damage;

            if(enemy.health <= 0)
            {
                enemy.isAlive = false;
                Tools.ColorfulWriteLine($" {enemy.name} is dead", ConsoleColor.Red);
                Tools.ColorfulWriteLine($"{name} is victorious!", ConsoleColor.Green);
            }
            else
            {
                Console.WriteLine($" {name} attcked {enemy.name} and {damage} inflicted to {enemy.name}, remaining health is {enemy.health}");
            }
        }

    }
}
