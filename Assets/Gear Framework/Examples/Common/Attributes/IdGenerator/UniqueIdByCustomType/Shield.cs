#if UNITY_EDITOR

using GearFramework.Common;
using UnityEngine;

namespace GearFramework.Examples
{
    [CreateAssetMenu(fileName = "Shield", menuName = "Gear Framework/Examples/IdGenerator/UniqueIdByCustomType/Shield")]
    public class Shield : ScriptableObject
    {
        [NonEditable]
        [UniqueIdByCustomType(typeof(Item))]
        [SerializeField] int id;
        public int Id => id;
    }
}
#endif
