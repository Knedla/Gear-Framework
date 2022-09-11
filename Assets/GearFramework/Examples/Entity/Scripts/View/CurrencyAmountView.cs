using GearFramework.Entity.Data;
using UnityEngine;
using UnityEngine.UI;

namespace GearFramework.Examples.Entity
{
    public class CurrencyAmountView : MonoBehaviour
    {
        [SerializeField] Image image;
        [SerializeField] Text amountText;

        public void SetData(CurrencyAmount currencyAmount)
        {
            image.sprite = currencyAmount.Entity.Sprite;
            image.type = currencyAmount.Entity.ImageType;
            amountText.text = currencyAmount.Amount.ToString();
        }
    }
}