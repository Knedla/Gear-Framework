#if UNITY_EDITOR

namespace GearFramework.Common.EditorOnly
{
    public enum PathModifyByNamespace
    {
        NaN,
        AdjustDirectoryPathToRootOfTypeNamespace,   // script path: c:/Value1/Value2/Value3        c# namespace: Value1.Value2.Value3   result path: c:/Value1
        AdjustDirectoryPathWithTypeNamespace        // script path: c:/Value1/Custom/Destination   C# namespace: Value1.Value2.Value3   result path: c:/Value1/Value2/Value3
    }
}
#endif
