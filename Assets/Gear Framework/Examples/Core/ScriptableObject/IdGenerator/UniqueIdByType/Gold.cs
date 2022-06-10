#if UNITY_EDITOR

using UnityEngine;

namespace Gear.Examples
{
    [CreateAssetMenu(fileName = "Gold", menuName = "Gear Framework/Examples/IdGenerator/UniqueIdByType/Gold")]
    public class Gold : ScriptableObject
    {
        [NonEditable]
        [UniqueIdByCustomType(typeof(Currency))] // note: pay attention, this is UniqueIdByCustomType
        [SerializeField] private int id;
        public int Id => id;
    }
}
#endif
