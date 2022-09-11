using UnityEngine;

namespace GearFramework.Runtime.PlayerInput.EscapeKeyStack
{
    public class Listener : MonoBehaviour, IListener
    {
        static Listener instance;

        public event OnKeyPressed OnKeyPressedEvent;
        
        public bool IsActive { get { return gameObject.activeSelf; } }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void Enable() => gameObject.SetActive(true);
        
        public void Disable() => gameObject.SetActive(false);
        
        private void Update()
        {
            if (Input.GetKeyDown(Config.GlobalEscapeKey))
                OnKeyPressedEvent?.Invoke();
        }
    }
}
