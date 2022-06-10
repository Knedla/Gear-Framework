#if UNITY_EDITOR

using UnityEngine;

namespace Gear.Examples.UniqueIdByCustomTypeExample
{
    [CreateAssetMenu(fileName = "Sword", menuName = "Gear Framework/Examples/IdGenerator/UniqueIdByCustomType/Sword")]
    public class Sword : ScriptableObject
    {
        [NonEditable]
        [UniqueIdByCustomType(typeof(Item))]
        [SerializeField] private int id;
        public int Id => id;
    }
}
#endif
