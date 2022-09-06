namespace Entity.Example.Definition
{
    public class Arrow : Ammo
    {
        public Arrow()
        {
            BuyQuantityPerUnit = 100;
            DamageModifier = 1.2f;
        }
    }
}
