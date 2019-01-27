using UnityEngine;

public static class GamepadHelper
{
    public static bool IsGamepadPresent()
    {
        string[] names = Input.GetJoystickNames();
        foreach (string name in names)
            if (!string.IsNullOrEmpty(name))
                return true;
        return false;
    }
}
