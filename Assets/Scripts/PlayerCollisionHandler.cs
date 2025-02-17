using System;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField]
    private MainScript Main;

    [SerializeField]
    private PlayerPlate Plate;

    // TODO: Change to UnityEvent!
    public delegate void CollideWithItem();
    public event CollideWithItem OnCollisionI;

    public delegate void CollideWithClient();
    public event CollideWithClient OnCollisionC;

    private bool IsVulnerable
    {
        get
        {
            var player = Main.GetPlayer();
            return player.Vulnerable;
        }
    }


    public void ItemCollision(GameObject obj)
    {
        if(IsVulnerable)
        {
            if (!Plate.Empty)
            {
                Plate.RemoveRandomItem();
                if (OnCollisionI != null)
                    OnCollisionI();
            }
        }
    }

    public void CustomerCollision(WaypointWandering wandering)
    {
        if(IsVulnerable)
        {
            if(!Plate.Empty)
            {
                Plate.RemoveAll();
                if (OnCollisionC != null)
                    OnCollisionC();
            }
        }
    }

    internal void CustomerCollision(WanderAndChase wanderandchase)
    {
        if (IsVulnerable)
        {
            if (!Plate.Empty)
            {
                Plate.RemoveAll();
                if (OnCollisionC != null)
                    OnCollisionC();
            }
        }
    }
}
