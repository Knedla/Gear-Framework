using Entity.Example.Data;
using UnityEngine;

namespace Entity.Example
{
    public class TooltipController : MonoBehaviour
    {
        public static TooltipController Instance { get; private set; }

        [SerializeField] CommodityTooltip commodityTooltip;
        [SerializeField] AmmoTooltip ammoTooltip;
        [SerializeField] ArmorTooltip armorTooltip;
        [SerializeField] WeaponTooltip weaponTooltip;

        ItemActionResolver itemActionResolver;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            itemActionResolver = new ItemActionResolver(commodityTooltip.SetData, ammoTooltip.SetData, armorTooltip.SetData, weaponTooltip.SetData);
        }

        public void Show(IItem entity) => itemActionResolver.Invoke(entity);
        public void Hide() => Tooltip.Close();
    }
}
