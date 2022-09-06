#if UNITY_EDITOR

using UnityEditor;
using UnityEngine.UIElements;

namespace GearFramework.Common.EditorOnly
{
    public class ProjectSettingsObject : SettingsObject<ProjectSettings, ProjectSettingsObject>
    {
        [SettingsProvider]
        static SettingsProvider RegisterSettingsProvider() => CreateSettingsProvider();
        protected override string SettingsAssetFilePath => PathUtil.GetCallerFilePath_ForSettingsAsset();

        protected override string Title => "Reserved Directory Names";
        protected override SettingsElement SettingsElement => new ExtendedSettingsElement(new PropertyDrawerElement<ProjectSettingsObject>(Instance), null, new HelpBox(helpText, HelpBoxMessageType.Info));

        static readonly string helpText =
            "Naming convention"
            + "\n\nMain directories:"
            + "\n - Common"
            + "\n - Runtime"
            + "\n - Instances - holds all instances of framework elements associated with one project"
            + "\n - Examples"

            + "\n\nSimple type directories:"
            + "\n - Class"
            + "\n - Interface"
            + "\n - MonoBehaviour"
            + "\n - ScriptableObject"
            + "\n - Static"

            + "\n\nSpecific type directories:"
            + "\n - Resources - contains all non c# and no assets files"
            + "\n - Prefab"
            + "\n - SettingsAsset - contains assets -> settings instances"

            + "\n\nComplex types:"
            + "\n - Abstract - subdirectory containing the abstract object"
            + "\n\n - EditorOnly directories - use to store files used only by the editor"
            + "\n\t- All files in the EditorOnly directory must have the namespace suffix .EditorOnly."
            + "\n\t- All lines of code or entire files that reference the EditorOnly namespace must be wrapped with #if UNITY_EDITOR"
            + "\n\t- The name EditorOnly is becouse the name Editor is used as a class name in the UnityEditor namespace"

            + "\n\n   Directory:       \t\tFile suffix:        \t\tnote:"
            + "\n    Extensions      \t\t Extension           \t\t - used for c# extensions"
            + "\n    Utils           \t\t Util              \t\t - provides a collection of helper functions, contains just static methods, it is stateless and cannot be instantiated"
            + "\n    Attribute       \t\t Attribute           \t\t - "
            //+ "\n    AssetBundles     \t AssetBundle         \t - "

            + "\n\n[EditorOnly exclusive subdirectories]"
            + "\n                    \t\t EditorText                \t - used to store editor messages, contains just const fields, it is stateless and cannot be instantiated"
            + "\n    Settings        \t\t Object                \t\t - "
            + "\n                    \t\t SettingsElement   \t - "
            + "\n    VisualElement   \t VisualElement       \t - "
            + "\n    PropertyDrawer  \t Drawer                \t\t - "
            + "\n    CustomEditor    \t Editor                \t\t - "


            + "\n\nEditorOnly namespace folders" +
            "\nEditorOnly" +
            "\nSettings" +
            "\nVisualElement" +
            "\nPropertyDrawer" +
            "\nCustomEditor" +
            "\nMenuItems"

            ;
    }
}
#endif
