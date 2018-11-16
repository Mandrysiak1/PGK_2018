using UnityEngine;
using UnityEngine.Experimental.UIElements;

[CreateAssetMenu(menuName = "Beerfest/OrderItem")]

public class OrderItem : ScriptableObject
{

    public string Name;

    public int MaximumSize;

    public float Frequency;

    public Image Image; // ?????

    public Sprite Sprite;

}
