using QTE;
using UnityEngine;

public class GameContext : MonoBehaviour
{
    // TODO: Remove MainScript entirely
    public MainScript Main;
    public Player Player;
    public OrderController Orders;

    public static void FindIfNull(ref GameContext context)
    {
        if (context == null)
        {
            context = FindObjectOfType<GameContext>();
            if (context == null)
                Debug.LogError("Could not find GameContext!");
        }
    }
}
