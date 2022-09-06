using Entity.Example.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entity.Example
{
    public class EntityTestScene : MonoBehaviour
    {
        [SerializeField] CurrencyAmountView goldView;
        [SerializeField] CurrencyAmountView diamondView;

        [SerializeField] ItemAmountView itemAmountViewPrefab;
        [SerializeField] Transform itemsTransform;

        private void Start()
        {
            PopulateCurrencies(GetWallet());
            PopulateInventory(GetInventory());
        }

        Dictionary<Type, CurrencyAmount> GetWallet()
        {
            Dictionary<Type, CurrencyAmount> wallet = new Dictionary<Type, CurrencyAmount>();
            wallet.Add(typeof(Gold), new CurrencyAmount(new Gold(), 325));
            wallet.Add(typeof(Diamond), new CurrencyAmount(new Diamond(), 4));
            return wallet;
        }

        void PopulateCurrencies(Dictionary<Type, CurrencyAmount> wallet)
        {
            goldView.SetData(wallet[typeof(Gold)]);
            diamondView.SetData(wallet[typeof(Diamond)]);
        }

        List<ItemAmount> GetInventory()
        {
            return new List<ItemAmount>()
            {
                new ItemAmount(new Axe()),
                new ItemAmount(new Helm()),
                new ItemAmount(new Apple(), 20),
                new ItemAmount(new Apple(), 1),
                new ItemAmount(new Arrow(), 59),
            };
        }

        void PopulateInventory(List<ItemAmount> inventory)
        {
            foreach (ItemAmount item in inventory)
                Instantiate(itemAmountViewPrefab, itemsTransform).SetData(item);
        }
    }
}
