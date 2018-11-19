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
        }
        else
        {
            Plate.RemoveRandomItem();
        }
    }

    public void CustomerCollision(WaypointWandering wandering)
    {
        if(HasQteChance)
        {
            QTE.TryRunCollisionQte(allItems: true);
        }
        else
        {
            Plate.RemoveAll();
        }
    }
}
