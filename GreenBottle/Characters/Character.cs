namespace GreenBottle.Characters
{
    public class Character
    {
        public string Name;
        public string Icon;

        public int X;
        public int Y;

        public int Health { get; set; }

        public int HealthMax { get; set; }

        public int Gold { get; set; }

        public int WeaponDamageLow { get; set; }
        public int WeaponDamageHigh { get; set; }
        public int WeaponDamage { get; set; }

        public string WeaponName { get; set; }

        // add inventory here ??
    }
}