using System;
using UnityEngine;
using UnityEngine.UI;

namespace Entity.Definition
{
    public abstract class DataEntity : ScriptableObject
    {
        [Header("- String -")]

        public string Name;
        public string Description;

        [Header("- Sprite -")]

        public Sprite Sprite;
        public Image.Type ImageType;

        Type type;
        public Type Type
        {
            get
            {
                if (type == null)
                    type = GetType();

                return type;
            }
        }

        public DataEntity()
        {
            Name = Config.NameResourcePrefix + Type.Name.ToLower();
            Description = Config.DescriptionResourcePrefix + Type.Name.ToLower();
        }
    }
}
