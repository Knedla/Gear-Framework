#if UNITY_EDITOR

using GearFramework.Common;
using UnityEngine;

namespace GearFramework.Examples
{
    [CreateAssetMenu(fileName = "Gold", menuName = "Gear Framework/Examples/IdGenerator/UniqueIdByType/Gold")]
    public class Gold : ScriptableObject
    {
        [NonEditable]
        [UniqueIdByCustomType(typeof(Currency))] // note: pay attention, this is UniqueIdByCustomType
        [SerializeField] int id;
        public int Id => id;
    }
}
#endif
