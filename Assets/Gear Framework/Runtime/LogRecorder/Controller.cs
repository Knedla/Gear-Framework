using UnityEngine;

namespace GearFramework.LogRecorder
{
    public class Controller : MonoBehaviour
    {
        static Controller instance;

        Manager manager;

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

        public void SetData(Manager manager) => this.manager = manager;

        void OnApplicationFocus(bool hasFocus)
        {
            if (manager != null && manager.Settings.LogOnFocusLost && !hasFocus)
                manager.Log();
        }

        void OnApplicationPause(bool pauseStatus)
        {
            if (manager != null && manager.Settings.LogOnPause && pauseStatus)
                manager.Log();
        }
    }
}
