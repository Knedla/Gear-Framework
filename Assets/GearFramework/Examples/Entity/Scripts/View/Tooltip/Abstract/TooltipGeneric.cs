using GearFramework.Examples.Entity.Data;

namespace GearFramework.Examples.Entity
{
    public abstract class Tooltip<T> : Tooltip where T : IItem
    {
        public virtual void SetData(T entity)
        {
            image.sprite = entity.Sprite;
            image.type = entity.ImageType;

            nameText.text = entity.Name;
            descriptionText.text = entity.Description;

            Show();
        }
    }
}
