using UnityEngine;

public class BarScript : MonoBehaviour
{
    private bool hasPlayer = false;
    private LevelScene LevelScene;
    private MainScript MainScript;
    // public List<OrderItem> OrdersToPick = new List<OrderItem>();
    public OrderItem orderToPick;


    void Start()
    {
        MainScript = FindObjectOfType<MainScript>();
        LevelScene = FindObjectOfType<LevelScene>();
    }

    void Update ()
    {
        if(Input.GetButtonDown("Submit") && hasPlayer)
        {

            // LevelScene.Player.AddOrderItemOnPlate(OrdersToPick[0]);
            LevelScene.Player.AddOrderItemOnPlate(orderToPick);
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
