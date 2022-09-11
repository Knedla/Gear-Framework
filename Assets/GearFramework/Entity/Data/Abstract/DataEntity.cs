using System;
using UnityEngine;
using UnityEngine.UI;

namespace GearFramework.Entity.Data
{
    public abstract class DataEntity : IDataEntity
    {
        protected abstract Definition.DataEntity Definition { get; }

        public string Name => Definition.Name;
        public string Description => Definition.Description;

        public Sprite Sprite => Definition.Sprite;
        public Image.Type ImageType => Definition.ImageType;

        public Type DefinitionType => Definition.Type;

    }
}
