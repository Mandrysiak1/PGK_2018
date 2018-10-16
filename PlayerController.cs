using UnityEngine;

public class PlayerController : MonoBehaviour {

    private int points = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("some")) //CURRENTLY OBJECTS TO INTERACT WITH, HAVE TAG SOME
        {
            Messenger.Broadcast("show id"); //TESTS
        }

        //other.gameObject.SetActive(false);
    }
}
