using Entity.Example.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Entity.Example
{
    public class ItemAmountView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Image image;
        [SerializeField] Text amountText;

        ItemAmount itemAmount;

        public void SetData(ItemAmount itemAmount)
        {
            this.itemAmount = itemAmount;

            image.sprite = itemAmount.Entity.Sprite;
            image.type = itemAmount.Entity.ImageType;

            amountText.gameObject.SetActive(itemAmount.Entity.Stackable);

            if (amountText.gameObject.activeSelf)
                amountText.text = itemAmount.Amount.ToString();
        }

        public void OnPointerEnter(PointerEventData eventData) => TooltipController.Instance.Show(itemAmount.Entity);
        public void OnPointerExit(PointerEventData eventData) => TooltipController.Instance.Hide();
    }
}