using QTE;
using UnityEngine;

public class GameContext : MonoBehaviour
{
    public GameSettings Settings;
    public Player Player;
    public OrderController Orders;

    public static void FindIfNull(ref GameContext context)
    {
        if (context == null)
        {
            context = FindObjectOfType<GameContext>();
            if (context == null)
                Debug.LogWarning("Could not find GameContext!");
        }
    }
}
