#if UNITY_EDITOR

using UnityEngine;

namespace Gear.Examples
{
    [CreateAssetMenu(fileName = "Shield", menuName = "Gear Framework/Examples/IdGenerator/UniqueIdByCustomType/Shield")]
    public class Shield : ScriptableObject
    {
        [NonEditable]
        [UniqueIdByCustomType(typeof(Item))]
        [SerializeField] private int id;
        public int Id => id;
    }
}
#endif
