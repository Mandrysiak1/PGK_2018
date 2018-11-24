using UnityEngine;

public class TableScript : MonoBehaviour
{
    [SerializeField]
    private GameContext Context;

    public Table MyTable { get; set; }
    private bool hasPlayer = false;

    void Start()
    {
        GameContext.FindIfNull(ref Context);

        MyTable = new Table(GetComponent<OrderSource>().possibleRequests);

        Messenger.AddListener("Table show", DebugID);

        var x = FindObjectOfType(typeof(MainScript));
        if(x != null)
            ((MainScript)x).SendMessage("AddFreeTable", MyTable);
    }

    void Update()
    {

        if (Input.GetButtonDown("Submit") && hasPlayer == true && !Context.QTE.IsRunning)

        {
            Debug.Log(UpgradeClass.Tip+"<- tipy");
            if (MyTable != null)
            {

                if (MyTable.IsThereOrder())
                {

                    if (Context.Player.GetItemOrderOnPlateQuantity(MyTable.currOrder.orderType) > 0)
                    {
                        Context.Player.RemoveBeer(MyTable.currOrder.orderType);
                        MyTable.putBeer();
                        int remaining = (int)MyTable.CurrOrder.getOrderSize() - MyTable.getBOT();
                        Debug.Log("Połozono piwo, potrzeba jeszcze: " + remaining);
                        if (remaining == 0)
                        {
                            if(MyTable.CurrOrder.orderType.name == "WitchPotion") //sorry for hardcoding i will fix it next week, i promise, for now its too much work :C
                            {

                                UpgradeClass.Tip += 10;
                            }
                            Context.QTE.TryRunTipQte();
                        }

                    }
                    else
                        Debug.Log("Nie masz przy sobie piwa!");
                }
                else
                {
                    Debug.Log("Aktualnie nie ma zamówienia na tym stoliku!");
                }

            }
        }

    }

    public void DebugID()
    {
        Debug.Log(MyTable.ID);
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
        {;
            hasPlayer = false;
        }
    }

}
