using UnityEngine;

public abstract class ButtonVisualization : MonoBehaviour
{
    protected bool GamepadMode = false;
    private bool _Dirty = true;

    private void Update()
    {
        bool newGamepadMode = IsGamepadPresent();
        if (newGamepadMode != GamepadMode)
        {
            _Dirty = true;
        }

        GamepadMode = newGamepadMode;

        if (_Dirty)
        {
            Refresh();
            _Dirty = false;
        }
    }

    protected abstract void Refresh();

    private bool IsGamepadPresent()
    {
        string[] names = Input.GetJoystickNames();
        return names.Length > 0 && !string.IsNullOrEmpty(names[0]);
    }
}
