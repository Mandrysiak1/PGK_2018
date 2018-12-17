using UnityEngine;

public class BarScriptTutorial : MonoBehaviour
{
    [SerializeField]
    private GameContext Context;

    public Canvas barCanvasPickup;
    public Canvas barCanvasHelp;
    public bool hasPlayer = false;
    public OrderItem orderToPick;
    public OrderSource orderSource;
    public PlayerPlate playerPlate;

    void Start()
    {
        GameContext.FindIfNull(ref Context);
        barCanvasPickup.enabled = false;
        barCanvasHelp.enabled = false;
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