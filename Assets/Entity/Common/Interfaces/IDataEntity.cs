using UnityEngine;
using UnityEngine.UI;

namespace Entity.Data
{
    public interface IDataEntity
    {
        string Name { get; }
        string Description { get; }
        Sprite Sprite { get; }
        Image.Type ImageType { get; }
    }
}
