#if UNITY_EDITOR

using UnityEngine;

namespace Gear.Examples.UniqueIdByTypeExample
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Gear Framework/Examples/IdGenerator/GUIDGenerator/Weapon")]
    public class Weapon : ScriptableObject
    {
        [NonEditable]
        [GUIDGenerator]
        [SerializeField] private string id;
        public string Id => id;
    }
}
#endif
