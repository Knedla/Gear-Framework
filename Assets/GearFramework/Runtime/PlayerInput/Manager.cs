using UnityEngine;

namespace GearFramework.Runtime.PlayerInput
{
    public class Manager // alternative name: PlayerInputComponent
    {
        public static bool GetKeyDown(KeyCode keyCode_1, KeyCode keyCode_2) => Input.GetKeyDown(keyCode_1) || Input.GetKeyDown(keyCode_2);
        public static bool KeyPressed(KeyCode keyCode_1, KeyCode keyCode_2) => Input.GetKey(keyCode_1) || Input.GetKey(keyCode_2);
        public static bool GetKeyUp(KeyCode keyCode_1, KeyCode keyCode_2) => Input.GetKeyUp(keyCode_1) || Input.GetKeyUp(keyCode_2);
        public static bool AnyShiftKeyPressed { get { return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift); } }
        public static bool AnyControlKeyPressed { get { return Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl); } }
    }
}
