using UnityEngine;

public class TableTrigger : MonoBehaviour
{
    [SerializeField]
    private GameContext Context;
    [SerializeField]
    private OrderSource Source;

    private bool PlayerInRange = false;

    void Start()
    {
        GameContext.FindIfNull(ref Context);
    }

    void Update()
    {
        if (Input.GetButtonDown("Submit") && PlayerInRange && !Context.QTE.IsRunning)
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
