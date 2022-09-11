#if UNITY_EDITOR

using GearFramework.Common;
using UnityEngine;

namespace GearFramework.Examples
{
    [CreateAssetMenu(fileName = "Currency", menuName = "Gear Framework/Examples/IdGenerator/UniqueIdByType/Currency")]
    public class Currency : ScriptableObject
    {
        [NonEditable]
        [UniqueIdByType]
        [SerializeField] int id;
        public int Id => id;
    }
}
#endif
