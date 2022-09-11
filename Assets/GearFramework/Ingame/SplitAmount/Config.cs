using UnityEngine;

namespace GearFramework.Ingame.SplitAmount
{
    public partial class Config
    {
        public const KeyCode RequestToOpenKey_1 = KeyCode.LeftShift; // thoughts: should this be added to the keybinding menu? I can't remember it being an option anywhere
        public const KeyCode RequestToOpenKey_2 = KeyCode.RightShift;

        public const int HorizontalEdgeAlignmentOffset = 5; // thoughts: different contexts may need different offsets, potentially...
        public const int VerticalEdgeAlignmentOffset = 5;
    }
}
