#if UNITY_EDITOR

namespace UnityEditor
{
    using UnityEngine;

    [CustomPropertyDrawer(typeof(Reference<Sprite>))]
    public class SpriteReferenceDrawer : ObjectReferenceDrawer<Sprite> { }
}
#endif