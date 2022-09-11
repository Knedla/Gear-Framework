using UnityEngine;
using UnityEngine.UI;

namespace GearFramework.Examples.Entity
{
    public abstract class Tooltip : MonoBehaviour
    {
        static Tooltip active;

        public static void Close()
        {
            if (active != null)
                active.Hide();
        }

        [SerializeField] protected Image image;
        [SerializeField] protected Text nameText;
        [SerializeField] protected Text descriptionText;

        private void Awake() => Hide();

        protected void Show()
        {
            if (active != null)
                active.Hide();

            gameObject.SetActive(true);
            active = this;
        }

        void Hide()
        {
            gameObject.SetActive(false);
            active = null;
        }
    }
}
