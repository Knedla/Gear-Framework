namespace Entity.Example.Definition
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
