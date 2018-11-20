using QTE;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField]
    private QTEController QTE;

    [SerializeField]
    private MainScript Main;

    [SerializeField]
    private PlayerPlate Plate;

    public delegate void CollideWithItem();
    public event CollideWithItem OnCollisionI;

    public delegate void CollideWithClient();
    public event CollideWithClient OnCollisionC;

    private bool HasQteChance
    {
        get
        {
            var player = Main.GetPlayer();
            return !player.Vulnerable;
        }
    }


    public void ItemCollision(GameObject obj)
    {
        if (QTE.IsRunning)
            return;

        if (HasQteChance)
        {
            QTE.TryRunCollisionQte(allItems: false);
            if(OnCollisionI != null)
            OnCollisionI();
        }
        else
        {
            Plate.RemoveRandomItem();
            if (OnCollisionI != null)
                OnCollisionI();
        }
    }

    public void CustomerCollision(WaypointWandering wandering)
    {
        if(HasQteChance)
        {
            QTE.TryRunCollisionQte(allItems: true);
            if (OnCollisionC != null)
                OnCollisionC();
        }
        else
        {
            Plate.RemoveAll();
            if (OnCollisionC != null)
                OnCollisionC();
        }
    }
}
