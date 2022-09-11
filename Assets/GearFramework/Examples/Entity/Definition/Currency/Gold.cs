namespace GearFramework.Examples.Entity.Definition
{
    public class Gold : GearFramework.Entity.Definition.Currency
    {
        public Gold() => MaxAmmount = int.MaxValue;
    }
}
