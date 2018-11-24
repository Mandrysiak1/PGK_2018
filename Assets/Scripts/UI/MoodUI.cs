using UnityEngine;
using UnityEngine.UI;

public class MoodUI : MonoBehaviour
{
    public Sprite[] Sprites;
    [SerializeField]
    private Image Image;

    public float Mood
    {
        get { return _Mood; }
        set
        {
            _Mood = value;
            Dirty = true;
        }
    }

    private float _Mood = 1.0f;
    private bool Dirty = true;

    private void Update()
    {
        if (Dirty)
        {
            Dirty = false;

            Image.sprite = GetSpriteForMood(Mood);
        }
    }

    private Sprite GetSpriteForMood(float mood)
    {
        int spritesCount = Sprites.Length;
        float step = 1.0f / spritesCount;
        int index = (int)(mood / step);
        index = Mathf.Clamp(index, 0, spritesCount - 1);

        return Sprites[index];
    }
}
