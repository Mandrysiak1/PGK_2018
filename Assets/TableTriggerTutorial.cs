using UnityEngine;

public class TableTriggerTutorial : MonoBehaviour
{
    [SerializeField]
    private GameContext Context;
    [SerializeField]
    private OrderSource Source;
    public PlayerPlate playerPlate;
    public Canvas DeliverHere;
    public Canvas PressE;
    public OrderItem orderToPick;

    private bool PlayerInRange = false;

    void Start()
    {
        GameContext.FindIfNull(ref Context);
    }

    void Update()
    {
        if (Source.CurrentOrder != null && PlayerInRange == false && playerPlate.GetItemQuantityOnPlate(orderToPick) >= (Source.CurrentOrder.Size - Source.CurrentOrder.FilledSize))
            DeliverHere.enabled = true;
        else DeliverHere.enabled = false;
        if (Source.CurrentOrder != null && PlayerInRange == true && playerPlate.GetItemQuantityOnPlate(orderToPick) >= (Source.CurrentOrder.Size - Source.CurrentOrder.FilledSize))
        {
            PressE.enabled = true;
        }
        else PressE.enabled = false;
        if (Input.GetButtonDown("Submit") && PlayerInRange)
        {
            Context.Orders.TryUpdateOrderSource(Source);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = false;
        }
    }

}
