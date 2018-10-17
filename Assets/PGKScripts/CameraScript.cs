using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {

    public Transform player;

    public float distanceX = 0;
    public float distanceY = 6;
    public float distanceZ = -7;

    public float rotationX = 30;
    public float rotationY = 0;
    public float rotationZ = 0;

    private Vector3 scrolledVector = new Vector3(0,0,0);

	// Use this for initialization
	void Start () {
        transform.Rotate(rotationX, rotationY, rotationZ);
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = player.position + new Vector3(distanceX, distanceY, distanceZ);
    }
}
