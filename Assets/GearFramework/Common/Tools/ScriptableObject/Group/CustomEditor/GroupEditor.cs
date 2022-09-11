#if UNITY_EDITOR

using UnityEditor;

namespace GearFramework.Common.EditorOnly
{
    [CustomEditor(typeof(Group))]
    public abstract class GroupEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.HelpBox(helpText, MessageType.Info, true);
        }

        static readonly string helpText = "The file name serves as a unique id."
            + "\nThe Group serves as the bearer of the name."
            + "\nWhen a Group starts to be used, the file name must not be changed again.";
    }
}
#endif
