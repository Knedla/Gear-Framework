namespace GearFramework.Examples.Entity.Definition
{
    public abstract class Ammo : Item
    {
        public float DamageModifier;

        public Ammo() => Stackable = true;
    }
}
