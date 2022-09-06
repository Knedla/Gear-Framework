#if UNITY_EDITOR

using GearFramework.Common;
using UnityEngine;

namespace GearFramework.Examples
{
    [CreateAssetMenu(fileName = "Sword", menuName = "Gear Framework/Examples/IdGenerator/UniqueIdByCustomType/Sword")]
    public class Sword : ScriptableObject
    {
        [NonEditable]
        [UniqueIdByCustomType(typeof(Item))]
        [SerializeField] int id;
        public int Id => id;
    }
}
#endif
