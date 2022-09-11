namespace GearFramework.Examples.Entity.Definition
{
    public abstract class Commodity : Item
    {
        public Commodity()
        {
            Stackable = true;
            BuyQuantityPerUnit = 1; // example: for 1 gold you buy 3 apples, you cannot buy one apple at a time, only in bulk
        }
    }
}
