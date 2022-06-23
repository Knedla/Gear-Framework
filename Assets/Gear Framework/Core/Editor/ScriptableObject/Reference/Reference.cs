using System;

namespace UnityEngine
{
    [Serializable]
    public class Reference<T>
    {
        [SerializeField] private bool useConstant = true;
        [SerializeField] private T constantValue;
        [SerializeField] private Variable<T> variable;
        public T Value => useConstant ? constantValue : variable.Value;
    }
}
