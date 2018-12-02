using UnityEngine;

public class BarScriptTutorial : MonoBehaviour
{
    [SerializeField]
    private GameContext Context;

    public Canvas barCanvas;

    private bool hasPlayer = false;
    public OrderItem orderToPick;

    void Start()
    {
        GameContext.FindIfNull(ref Context);
        barCanvas.enabled = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("ReturnItemOnBar") && hasPlayer && orderToPick != null)
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
            barCanvas.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasPlayer = false;
            barCanvas.enabled = false;
        }
    }
}