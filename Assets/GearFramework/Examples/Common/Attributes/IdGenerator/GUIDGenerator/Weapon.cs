#if UNITY_EDITOR

using GearFramework.Common;
using UnityEngine;

namespace GearFramework.Examples
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Gear Framework/Examples/IdGenerator/GUIDGenerator/Weapon")]
    public class Weapon : ScriptableObject
    {
        [NonEditable]
        [GUIDGenerator]
        [SerializeField] string id;
        public string Id => id;
    }
}
#endif
