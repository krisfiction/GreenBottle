namespace GreenBottle.Characters
{
    public class Character
    {
        public string Name;
        public string Icon;

        public int X;
        public int Y;

        private int _hp;
        private int _hpMax;

        public int HP
        {
            get
            {
                return _hp;
            }
            set
            {
                _hp = value;
            }
        }

        public int HPMax
        {
            get
            {
                return _hpMax;
            }
            set
            {
                _hpMax = value;
            }
        }

        public int HPPercentage
        {
            get
            {
                return _hp / _hpMax * 100;
            }
        }

        // old data below


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