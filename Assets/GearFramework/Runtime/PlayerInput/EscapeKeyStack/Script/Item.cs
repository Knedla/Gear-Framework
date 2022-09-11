using UnityEngine;

namespace GearFramework.Runtime.PlayerInput.EscapeKeyStack
{
    public abstract class Item : MonoBehaviour, IItem
    {
        public virtual bool BlockEscapeKey => false;
        
        protected virtual void OnEnable() => Gear.Manager.Push(this);
        
        protected virtual void OnDisable() => Gear.Manager.Remove(this);
        
        public abstract void EscapeKeyPressed();
    }
}
