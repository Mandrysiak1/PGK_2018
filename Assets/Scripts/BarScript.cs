using UnityEngine;

public class BarScript : MonoBehaviour
{
    [SerializeField]
    private GameContext Context;

    private bool hasPlayer = false;
    public OrderItem orderToPick;

    void Start()
    {
        GameContext.FindIfNull(ref Context);
    }

    void Update()
    {
        if(Input.GetButtonDown("ReturnItemOnBar") && hasPlayer && orderToPick != null)
        {
            Context.Player.RemoveBeer(orderToPick);
        }
        if (Input.GetButtonDown("Submit") && hasPlayer && orderToPick != null)
        {
            Context.Player.AddOrderItemOnPlate(orderToPick);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasPlayer = false;
        }
    }
}