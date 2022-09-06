namespace Entity.Example.Definition
{
    public class Axe : Weapon
    {
        public float MinDamage;
        public float MaxDamage;

        public Axe()
        {
            MinDamage = 3;
            MaxDamage = 7;
        }
    }
}
