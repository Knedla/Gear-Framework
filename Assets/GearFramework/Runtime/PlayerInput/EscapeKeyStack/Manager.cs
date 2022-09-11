using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GearFramework.Runtime.PlayerInput.EscapeKeyStack
{
    public class Manager : IManager
    {
        IListener listener;
        List<IItem> items;
        IItem currentItem;

        public Manager()
        {
            items = new List<IItem>();
            SetListener(new GameObject(typeof(Listener).Name).AddComponent<Listener>());
        }

        void SetListener(IListener listener)
        {
            this.listener = listener;
            this.listener.OnKeyPressedEvent += Listener_OnKeyPressedEvent;
            SetListenerState();
        }
        
        void Listener_OnKeyPressedEvent()
        {
            if (currentItem != null && !currentItem.BlockEscapeKey)
                currentItem.EscapeKeyPressed();
        }
        
        public void Push(IItem item)
        {
            items.Add(item);
            currentItem = item;
            SetListenerState();
        }
        
        public void Remove(IItem item)
        {
            if (!items.Remove(item) || currentItem != item)
                return;

            currentItem = items.Count == 0 ? null : items.Last();
            SetListenerState();
        }
        
        void SetListenerState()
        {
            if (currentItem != null && !currentItem.BlockEscapeKey)
            {
                if (!listener.IsActive)
                    listener.Enable();
            }
            else
            {
                if (listener.IsActive)
                    listener.Disable();
            }
        }
    }
}
