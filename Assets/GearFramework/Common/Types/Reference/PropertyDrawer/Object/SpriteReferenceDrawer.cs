#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace GearFramework.Common.EditorOnly
{
    [CustomPropertyDrawer(typeof(Reference<Sprite>))]
    public class SpriteReferenceDrawer : ObjectReferenceDrawer<Sprite> { }
}
#endif
