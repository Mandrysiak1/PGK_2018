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

    public bool PlayerInRange = false;

    void Start()
    {
        GameContext.FindIfNull(ref Context);
    }

    void Update()
    {
        bool willUpdate = Context.Orders.CanFillOrder(Source);

        if (Source.CurrentOrder != null && PlayerInRange == false && willUpdate)
            DeliverHere.enabled = true;
        else DeliverHere.enabled = false;
        if (Source.CurrentOrder != null && PlayerInRange == true && willUpdate)
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
