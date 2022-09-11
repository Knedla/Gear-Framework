#if UNITY_EDITOR
using GearFramework.Common.EditorOnly;
#endif
using System.Collections.Generic;
using UnityEngine;

namespace GearFramework.Entity
{
    public class DbContext : MonoBehaviour
    {
#if UNITY_EDITOR
        public static string GetPrefabFilePath() => System.IO.Path.ChangeExtension(PathUtil.GetCallerFilePath_ForInstances(), "prefab");
#endif
        public List<ScriptableObject> Context;
    }
}
