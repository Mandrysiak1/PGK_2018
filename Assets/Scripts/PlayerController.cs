using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour {

    public class PlayerCollision : UnityEvent { }

    public readonly PlayerCollision OnPlayerCollide = new PlayerCollision();

    private int points = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("WanderingCustomer"))
        {
            OnPlayerCollide.Invoke();
        }
    }
}
