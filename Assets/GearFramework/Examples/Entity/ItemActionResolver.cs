using GearFramework.Examples.Entity.Data;
using System;

namespace GearFramework.Examples.Entity
{
    public class ItemActionResolver
    {
        Action<ICommodity> commodityAction;
        Action<IAmmo> ammoAction;
        Action<IArmor> armorAction;
        Action<IWeapon> weaponAction;

        public ItemActionResolver(Action<ICommodity> commodityAction, Action<IAmmo> ammoAction, Action<IArmor> armorAction, Action<IWeapon> weaponAction)
        {
            this.commodityAction = commodityAction;
            this.ammoAction = ammoAction;
            this.armorAction = armorAction;
            this.weaponAction = weaponAction;
        }

        public void Invoke(IItem item)
        {
            if (Invoke(item, commodityAction))
                return;
            if (Invoke(item, ammoAction))
                return;
            if (Invoke(item, armorAction))
                return;
            if (Invoke(item, weaponAction))
                return;
        }

        bool Invoke<T>(IItem item, Action<T> action) where T : class, IItem
        {
            T entity = item as T;

            if (entity != null)
            {
                action.Invoke(entity);
                return true;
            }

            return false;
        }
    }
}