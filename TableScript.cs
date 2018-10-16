using UnityEngine;

public class TableScript : MonoBehaviour {

    private Table myTable;
	// Use this for initialization
	void Start () {
        myTable = new Table();
        Messenger.AddListener("show id", DebugID);
	}
	
	// Update is called once per frame
	void Update () {
		//PERHAPS WE SHOULD UPDATE OUR MOOD HERE? :D
	}

    public void DebugID()
    {
        Debug.Log(myTable.ID);
    }

    private void OnTriggerStay(Collider other)
    {
        //PLAYER HAS TO HAVE TAG PLAYER
        if (other.CompareTag("player"))
        {
            
            if(Input.GetKeyDown(KeyCode.E))
            {
                if (myTable != null)
                {
                    if(myTable.IsThereOrder())
                    {
                        //SEND OBJECT HERE
                        //PICK UP ORDER
                        Debug.Log("Object picked, E clicked :D");
                        myTable.TableAwaiting = true;
                    } else
                    {
                        if (myTable.TableAwaiting == true)
                        {
                            //GIVE BEERS :D BUT FIRST CHECK IF I HAVE IT, OFCOURSE
                            Debug.Log("Gave tha beer :D");
                            myTable.TableAwaiting = false;
                            //GOTTA HAVE TO CHANGE THIS TABLE AWAITING FOR THE AMOUNT OF BEERS AFTER
                            // PROTOTYPING OF COURSE
                        }
                    }
                    
                }
            }
        }
    }
}
