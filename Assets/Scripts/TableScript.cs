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

        MyTable = new Table();

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

                    if (LevelScene.Player.GetBeersOnPlateQuantity() > 0)
                    {
                        LevelScene.Player.RemoveBeer();
                        MyTable.putBeer();
                        int x = (int)MyTable.CurrOrder.getOrderSize() - MyTable.getBOT();
                        Debug.Log("Połozono piwo, potrzeba jeszcze: " + x);

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
            Debug.Log("Jesteś przy stoliku");
            hasPlayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Opuszczasz stolik");
            hasPlayer = false;
        }
    }

}
