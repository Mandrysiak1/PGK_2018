using UnityEngine;
using UnityEngine.Experimental.UIElements;

[CreateAssetMenu(menuName = "Beerfest/OrderItem")]

public class OrderItem : ScriptableObject
{

    public string Name;

    public int MaximumSize;

    public Sprite Sprite;

}
