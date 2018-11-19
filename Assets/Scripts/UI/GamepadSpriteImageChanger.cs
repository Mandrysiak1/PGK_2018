using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class GamepadSpriteImageChanger : ButtonVisualization
{
    [SerializeField]
    private Sprite GamepadSprite;

    private Image image;
    private Sprite keySprite;

    void Start ()
    {
        image = GetComponent<Image>();
        keySprite = image.sprite;
    }
    protected override void Refresh()
    {
        image.sprite = GamepadMode ? GamepadSprite : keySprite;
    }
}
