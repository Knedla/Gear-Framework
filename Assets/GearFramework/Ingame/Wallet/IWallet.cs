using GearFramework.Entity.Data;

namespace GearFramework.Ingame.Wallet
{
    public interface IWallet
    {
        int GetAmount(int currencyId);
        int GetAmount(Currency currency);

        void Add(int currencyId, int amount);
        bool Remove(int currencyId, int amount);

        void Add(Currency currency, int amount);
        bool Remove(Currency currency, int amount);

        void Add(CurrencyAmount currencyAmount);
        void Remove(CurrencyAmount currencyAmount);

        bool HasEnough(int currencyId, int amount);
        bool HasEnough(Currency currency, int amount);
    }
}
