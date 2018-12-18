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
        //image.sprite = GamepadMode ? GamepadSprite : keySprite;
        if (GamepadMode == true)
        {
            image.sprite = GamepadSprite;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        else
        {
            image.sprite = keySprite;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
