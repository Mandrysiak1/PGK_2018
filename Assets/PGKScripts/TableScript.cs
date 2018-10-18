using Assets.PGKScripts;
using UnityEngine;

public class TableScript : MonoBehaviour
{

    private Player myPlayer;
    public Table MyTable { get; set; }
    private bool hasPlayer = false;

    void Start()
    {

        MyTable = new Table();

        Messenger.AddListener("Table show", DebugID);
        var x = FindObjectOfType(typeof(MainScript));
        ((MainScript)x).SendMessage("AddFreeTable", MyTable);


        myPlayer = ((MainScript)x).GetPlayer();
    }

    // Musiało zostac zmienione, bo OnTriggerStay nie działa z GetKeyDown.
    // Kiedy to było używane wszyskie piwa kładły sie naraz, bo GetKeyDown
    // wywoływał się kilkanaście razy. GetKeyDown, zgodnie z dokumentacją powinien być 
    // używany tylko w update i fixeUpdate. Pisze tak jakby się ktoś pytał :)

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && hasPlayer == true)

        {
            if (MyTable != null)
            {

                if (MyTable.IsThereOrder())
                {

                    if (myPlayer.getBOP() > 0)
                    {
                        myPlayer.removeBeer();
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
