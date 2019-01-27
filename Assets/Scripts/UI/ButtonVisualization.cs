using UnityEngine;

public abstract class ButtonVisualization : MonoBehaviour
{
    protected bool GamepadMode = false;
    private bool _Dirty = true;

    private void Update()
    {
        bool newGamepadMode = GamepadHelper.IsGamepadPresent();
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
}
