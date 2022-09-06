#if UNITY_EDITOR

using UnityEditor;
using UnityEngine.UIElements;

namespace GearFramework.Common.EditorOnly
{
    // TODO: when the IdGeneratorSettingsElement is active and a new generator instance is created, the IdGeneratorSettingsElement should be refreshed
    public class IdGeneratorSettingsObject : SettingsObject<IdGeneratorSettings, IdGeneratorSettingsObject>
    {
        [SettingsProvider]
        static SettingsProvider RegisterSettingsProvider() => CreateSettingsProvider();
        protected override string SettingsAssetFilePath => PathUtil.GetCallerFilePath_ForSettingsAsset();

        protected override string Title => "Id Generators";
        protected override SettingsElement SettingsElement => new ExtendedSettingsElement(new PropertyDrawerElement<IdGeneratorSettingsObject>(Instance), null, GetSuffixVisualElement());

        VisualElement GetSuffixVisualElement()
        {
            VisualElement visualElement = new VisualElement();

            visualElement.Add(new Label());
            visualElement.Add(new HelpBox("Expected scenario: int.MaxValue will never be used in any of the generators", HelpBoxMessageType.Info));
            visualElement.Add(new Label());

            foreach (UniqueIdGenerator item in ScriptableObjectUtil.GetAssetInstances<UniqueIdGenerator>())
            {
                visualElement.Add(new Label(item.name));

                PropertyDrawerElement<UniqueIdGenerator> vsualElement = new PropertyDrawerElement<UniqueIdGenerator>(item);
                vsualElement.Init();
                visualElement.Add(vsualElement);

                visualElement.Add(new Label());
            }

            visualElement.Add(new HelpBox(helpText, HelpBoxMessageType.Info));

            return visualElement;
        }

        static readonly string helpText = "Attributes:"
            + "\n- GUIDGeneratorAttribute"
            + "\nIf there is a need to generate assets across different computers, GUIDGeneratorAttribute is the right choice"
            + "\n\n- UniqueIdByCustomTypeAttribute"
            + "\n- UniqueIdByTypeAttribute"
            + "\nThey are good for projects where all assets are generated on the same computer because the last generated id is stored locally in the project where the asset was created"
            + "\n\nYou can find the generators in the Generators directory at the root of the IdGenerator directory"
            + "\nGenerator names are generated as Type.ToString() => [Type.Namespace].[Type.Name]"
            + "\n\nExamples are available in the Assets/Gear Framework/Examples/Core/ScriptableObject/IdGenerator directory"
            + "\nYou can create an example assets from the Assets > Create > Gear Framework > Examples > IdGenerator > menu"
            + "\n\nThoughts:"
            + "\n- in reality, I don't have any good arguments for using int instead of string for Id"
            + "\n- whan used in a Dictionary<TKey, TValue> as a key, an int is a bit faster because it doesn't have to generate a hash code for it"
            + "\n- if you have millions of operations with that dictionary in a short period of time (per frame) you might feel the difference, that's it"
            + "\n- int it a bit easier for a human to remember";
    }
}
#endif
