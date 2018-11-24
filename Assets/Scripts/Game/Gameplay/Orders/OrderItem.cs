using UnityEngine;
using UnityEngine.Experimental.UIElements;

[CreateAssetMenu(menuName = "Beerfest/OrderItem")]
public class OrderItem : ScriptableObject
{
    public string Name;

    public int MaximumOrderSize;

    public Sprite Sprite;

}
