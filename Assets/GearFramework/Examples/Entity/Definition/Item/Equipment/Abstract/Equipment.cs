namespace GearFramework.Examples.Entity.Definition
{
    public abstract class Equipment : Item
    {
        public float Durability;

        public Equipment()
        {
            Stackable = false;
            BuyQuantityPerUnit = 1;

            Durability = 100;
        }
    }
}
