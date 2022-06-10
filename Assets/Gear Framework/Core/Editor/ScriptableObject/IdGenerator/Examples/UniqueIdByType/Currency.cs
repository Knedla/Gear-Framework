#if UNITY_EDITOR

using UnityEngine;

namespace Gear.Examples.UniqueIdByTypeExample
{
    [CreateAssetMenu(fileName = "Currency", menuName = "Gear Framework/Examples/IdGenerator/UniqueIdByType/Currency")]
    public class Currency : ScriptableObject
    {
        [NonEditable]
        [UniqueIdByType]
        [SerializeField] private int id;
        public int Id => id;
    }
}
#endif
