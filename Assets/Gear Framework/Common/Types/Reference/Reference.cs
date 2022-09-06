using System;
using UnityEngine;

namespace GearFramework.Common
{
    [Serializable]
    public class Reference<T>
    {
        [SerializeField] bool useConstant = true;
        [SerializeField] T constantValue;
        [SerializeField] Variable<T> variable;
        public T Value => useConstant ? constantValue : variable.Value;
    }
}
