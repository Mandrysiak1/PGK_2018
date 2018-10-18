using UnityEngine;

public class TableScript : MonoBehaviour {
    public Table MyTable { get; set; }
    // Use this for initialization
    void Start () {
        MyTable = new Table();
        Messenger.AddListener("Table show", DebugID);
        var x = FindObjectOfType(typeof(MainScript));
        ((MainScript)x).SendMessage("AddFreeTable", MyTable);
	}

    
	
	// Update is called once per frame
	void Update () {
        if (MyTable.TableAwaiting == true) MyTable.Mood -= Time.deltaTime;
        Debug.Log(MyTable.TableAwaiting);
        Debug.Log("update poszet");
	}

    public void DebugID()
    {
        Debug.Log(MyTable.ID);
    }

    private void OnTriggerStay(Collider other)
    {
        //PLAYER HAS TO HAVE TAG PLAYER
        if (other.CompareTag("Player"))
        {
            // Messenger.Broadcast("Press E");
            Debug.Log("Press E to interact");
            if(Input.GetKeyDown(KeyCode.E))
            {
                if (MyTable != null)
                {
                    if(MyTable.IsThereOrder())
                    {
                        //SEND OBJECT HERE
                        //PICK UP ORDER
                        Debug.Log("Object picked, E clicked :D");
                        MyTable.TableAwaiting = true;
                    } else
                    {
                        if (MyTable.TableAwaiting == true)
                        {
                            //GIVE BEERS :D BUT FIRST CHECK IF I HAVE IT, OFCOURSE
                            Debug.Log("Gave tha beer :D");
                            MyTable.TableAwaiting = false;
                            //GOTTA HAVE TO CHANGE THIS TABLE AWAITING FOR THE AMOUNT OF BEERS AFTER
                            // PROTOTYPING OF COURSE
                        }
                    }
                    
                }
            }
        }
    }
}
