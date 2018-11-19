using TMPro;
using UnityEngine;

public class ButtonTextLabel : ButtonVisualization
{
    public string KeyboardLabel = "";
    public string GamepadLabel = "";
    public Color GamepadColor = Color.white;

    [SerializeField]
    private TextMeshProUGUI Text;

    protected override void Refresh()
    {
        Text.text = GamepadMode ? GamepadLabel : KeyboardLabel;
        Text.color = GamepadMode ? GamepadColor : Color.white;
    }
}
