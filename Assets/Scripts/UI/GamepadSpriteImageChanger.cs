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
        //image.sprite = GamepadSprite;
    }
    protected override void Refresh()
    {
        
    }
}
