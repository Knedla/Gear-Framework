using UnityEngine;

namespace GearFramework.Runtime.PlayerInput.EscapeKeyStack
{
    sealed class Gear
    {
        public static IManager Manager { get; private set; }
        
        [RuntimeInitializeOnLoadMethod]
        static void Init() => Manager = new Manager();
    }
}
