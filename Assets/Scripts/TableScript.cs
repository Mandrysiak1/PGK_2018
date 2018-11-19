using Assets.PGKScripts;
using QTE;
using UnityEngine;

public class TableScript : MonoBehaviour
{
    [SerializeField]
    private QTEController QTE;
    [SerializeField]
    private LevelScene LevelScene;


    public Table MyTable { get; set; }
    private bool hasPlayer = false;


    void Start()
    {
        if (QTE == null)
            QTE = FindObjectOfType<QTEController>();
        if(LevelScene == null)
            LevelScene = FindObjectOfType<LevelScene>();

        MyTable = new Table(GetComponent<OrderSource>().possibleRequests);

        Messenger.AddListener("Table show", DebugID);

        var x = FindObjectOfType(typeof(MainScript));
        if(x != null)
            ((MainScript)x).SendMessage("AddFreeTable", MyTable);
    }

    void Update()
    {

        if (Input.GetButtonDown("Submit") && hasPlayer == true && !QTE.IsRunning)

        {
            if (MyTable != null)
            {

                if (MyTable.IsThereOrder())
                {

                    if (LevelScene.Player.GetItemOrderOnPlateQuantity(MyTable.currOrder.orderType) > 0)
                    {
                        LevelScene.Player.RemoveBeer(MyTable.currOrder.orderType);
                        MyTable.putBeer();
                        int remaining = (int)MyTable.CurrOrder.getOrderSize() - MyTable.getBOT();
                        Debug.Log("Połozono piwo, potrzeba jeszcze: " + remaining);
                        if (remaining == 0)
                        {
                            QTE.TryRunTipQte();
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
